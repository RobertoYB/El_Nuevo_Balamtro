using El_Nuevo_Balamtro.Clases;
using System;
namespace El_Nuevo_Balamtro.Uno
{
    public class UnoJuego : Juego
    {
        private Stack<UnoCarta> mazo = new Stack<UnoCarta>();
        private Stack<UnoCarta> descarte = new Stack<UnoCarta>();
        private List<JugadorBase> jugadores = new List<JugadorBase>();
        private int direccion = 1;
        public UnoJuego() : base("UNO") { }
        public override void Iniciar()
        {
            MostrarTitulo();
            CrearMazo();
            Barajar();
            jugadores.Add(new JugadorAleatorio("Jugador 1"));
            jugadores.Add(new JugadorCalculador("Jugador 2"));
            jugadores.Add(new JugadorAleatorio("Jugador 3"));
            for (int i = 0; i < 4; i++)
                foreach (var j in jugadores)
                    j.Mano.Add(mazo.Pop());
            UnoCarta primera;
            do
            {
                primera = mazo.Pop();
            } while (primera.Tipo == TipoCarta.MasCuatro);
            descarte.Push(primera);
            UnoCarta cartaActual = descarte.Peek();
            El_Nuevo_Balamtro.Core.LogHelper.Info($"Comienza el juego con: {cartaActual}");
            int turno = 0;
            while (true)
            {
                var jugador = jugadores[turno];
                El_Nuevo_Balamtro.Core.LogHelper.Info($"\n--- Turno de {jugador.Nombre} ---");
                var cartaJugada = jugador.JugarTurno(cartaActual, mazo, descarte, jugadores, turno);
                if (cartaJugada != cartaActual)
                {
                    descarte.Push(cartaJugada);
                    cartaActual = cartaJugada;
                    AplicarEfecto(cartaJugada, ref turno);
                }
                if (jugador.Mano.Count == 1)
                    El_Nuevo_Balamtro.Core.LogHelper.Advertencia($"{jugador.Nombre} grita ¡UNO!");
                else if (jugador.Mano.Count == 0)
                {
                    El_Nuevo_Balamtro.Core.LogHelper.Exito($"{jugador.Nombre} ha ganado el juego de UNO!");
                    break;
                }
                turno = (turno + direccion + jugadores.Count) % jugadores.Count;
                if (mazo.Count == 0)
                {
                    var cartaSuperior = descarte.Pop();
                    var cartasParaReutilizar = descarte.ToList();
                    descarte.Clear();
                    descarte.Push(cartaSuperior);
                    var rnd = new Random();
                    foreach (var c in cartasParaReutilizar.OrderBy(x => rnd.Next()))
                        mazo.Push(c);
                    El_Nuevo_Balamtro.Core.LogHelper.Info("♻️  El mazo se ha rebarajado.");
                }
            }
        }
        private void CrearMazo()
        {
            string[] colores = { "Rojo", "Azul", "Verde", "Amarillo" };
            foreach (var color in colores)
            {
                mazo.Push(new UnoCarta(color, TipoCarta.Numero, 0));
                for (int i = 1; i <= 9; i++)
                {
                    mazo.Push(new UnoCarta(color, TipoCarta.Numero, i));
                    mazo.Push(new UnoCarta(color, TipoCarta.Numero, i));
                }
                for (int i = 0; i < 2; i++)
                {
                    mazo.Push(new UnoCarta(color, TipoCarta.Bloqueo));
                    mazo.Push(new UnoCarta(color, TipoCarta.Reversa));
                    mazo.Push(new UnoCarta(color, TipoCarta.MasDos));
                }
            }
            for (int i = 0; i < 4; i++)
            {
                mazo.Push(new UnoCarta("Ninguno", TipoCarta.CambioColor));
                mazo.Push(new UnoCarta("Ninguno", TipoCarta.MasCuatro));
            }
        }
        private void Barajar()
        {
            var cartas = mazo.ToList();
            mazo.Clear();
            var rnd = new Random();
            foreach (var c in cartas.OrderBy(x => rnd.Next()))
                mazo.Push(c);
        }
        private void AplicarEfecto(UnoCarta carta, ref int turno)
        {
            switch (carta.Tipo)
            {
                case TipoCarta.Reversa:
                    direccion *= -1;
                    El_Nuevo_Balamtro.Core.LogHelper.Info("🔁 Se cambia el sentido de juego");
                    break;
                case TipoCarta.Bloqueo:
                    turno = (turno + direccion + jugadores.Count) % jugadores.Count;
                    El_Nuevo_Balamtro.Core.LogHelper.Info("🚫 Siguiente jugador pierde turno");
                    break;
                case TipoCarta.MasDos:
                    var siguiente = jugadores[(turno + direccion + jugadores.Count) % jugadores.Count];
                    for (int i = 0; i < 2; i++)
                        siguiente.Mano.Add(mazo.Pop());
                    El_Nuevo_Balamtro.Core.LogHelper.Info($"{siguiente.Nombre} toma +2 cartas");
                    break;
                case TipoCarta.MasCuatro:
                    var siguiente4 = jugadores[(turno + direccion + jugadores.Count) % jugadores.Count];
                    for (int i = 0; i < 4; i++)
                        siguiente4.Mano.Add(mazo.Pop());
                    El_Nuevo_Balamtro.Core.LogHelper.Info($"{siguiente4.Nombre} toma +4 cartas y se cambia color");
                    break;
            }
        }
    }
}