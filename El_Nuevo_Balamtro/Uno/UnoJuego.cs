using System;
using El_Nuevo_Balamtro.Modelos;
namespace El_Nuevo_Balamtro.Uno
{
    public class UnoJuego : Juego
    {
        private Stack<UnoCarta> mazo = new Stack<UnoCarta>();
        private Stack<UnoCarta> descarte = new Stack<UnoCarta>();
        private List<JugadorUno> jugadores = new List<JugadorUno>();
        private int direccion = 1;
        public UnoJuego() : base("Uno") { }
        public override void Iniciar()
        {
            MostrarTitulo();
            CrearMazo();
            Barajar();
            jugadores.Add(new JugadorAleatorio("Raúl"));
            jugadores.Add(new JugadorAleatorio("Ángel"));
            jugadores.Add(new JugadorAleatorio("Santiago"));
            for (int i = 0; i < 4; i++)
                foreach (var j in jugadores)
                    j.Mano.Add(SacarCartaDelMazo());
            UnoCarta cartaActual;
            do { cartaActual = SacarCartaDelMazo(); } while (cartaActual.Tipo == TipoCarta.MasCuatro);
            descarte.Push(cartaActual);
            Console.WriteLine($"Comienza con: {cartaActual}");
            int turno = 0;
            while (true)
            {
                var jugador = jugadores[turno];
                Console.WriteLine($"\nTurno de {jugador.Nombre}");
                var cartaJugada = jugador.JugarTurno(cartaActual, mazo, descarte, jugadores, turno);
                if (cartaJugada != cartaActual)
                {
                    descarte.Push(cartaJugada);
                    cartaActual = cartaJugada;
                    AplicarEfecto(cartaJugada, ref turno);
                }
                if (jugador.Mano.Count == 1)
                    Console.WriteLine($"- {jugador.Nombre} grita ¡Uno! -");
                else if (jugador.Mano.Count == 0)
                {
                    Console.WriteLine($"- ¡{jugador.Nombre} ha ganado la partida de Uno! -");
                    break;
                }
                turno = (turno + direccion + jugadores.Count) % jugadores.Count;
            }
        }
        private UnoCarta SacarCartaDelMazo()
        {
            if (mazo.Count == 0)
                RebarajarDescarte();
            return mazo.Pop();
        }
        private void RebarajarDescarte()
        {
            if (descarte.Count <= 1)
                throw new InvalidOperationException("- No hay cartas suficientes para rebarajar el mazo -");
            Console.WriteLine("\n- El mazo se ha agotado, rebarajando el descarte -");
            UnoCarta cartaSuperior = descarte.Pop();
            var cartasParaReusar = descarte.ToList();
            descarte.Clear();
            var rnd = new Random();
            foreach (var c in cartasParaReusar.OrderBy(x => rnd.Next()))
                mazo.Push(c);
            descarte.Push(cartaSuperior);
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
                    Console.WriteLine("- Se cambia el sentido de juego -");
                    break;
                case TipoCarta.Bloqueo:
                    turno = (turno + direccion + jugadores.Count) % jugadores.Count;
                    Console.WriteLine("- Siguiente jugador pierde turno -");
                    break;
                case TipoCarta.MasDos:
                    var siguiente = jugadores[(turno + direccion + jugadores.Count) % jugadores.Count];
                    for (int i = 0; i < 2; i++)
                        siguiente.Mano.Add(SacarCartaDelMazo());
                    Console.WriteLine($"- {siguiente.Nombre} toma +2 cartas -");
                    break;
                case TipoCarta.MasCuatro:
                    var siguiente4 = jugadores[(turno + direccion + jugadores.Count) % jugadores.Count];
                    for (int i = 0; i < 4; i++)
                        siguiente4.Mano.Add(SacarCartaDelMazo());
                    Console.WriteLine($"- {siguiente4.Nombre} toma +4 cartas y cambia el color -");
                    break;
            }
        }
    }
}