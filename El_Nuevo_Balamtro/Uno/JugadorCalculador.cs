using System;
namespace El_Nuevo_Balamtro.Uno
{
    public class JugadorCalculador : JugadorUno
    {
        private static Random random = new Random();
        public JugadorCalculador(string nombre) : base(nombre) { }
        public override UnoCarta JugarTurno(
            UnoCarta cartaActual,
            Stack<UnoCarta> mazo,
            Stack<UnoCarta> descarte,
            List<JugadorUno> jugadores,
            int indice)
        {
            int siguienteIndice = (indice + 1) % jugadores.Count;
            var siguienteJugador = jugadores[siguienteIndice];
            if (siguienteJugador.Mano.Count == 1)
            {
                var especiales = Mano.Where(c => c.PuedeJugarSobre(cartaActual) &&
                    (c.Tipo == TipoCarta.MasCuatro || c.Tipo == TipoCarta.MasDos ||
                     c.Tipo == TipoCarta.Bloqueo)).ToList();
                if (especiales.Count > 0)
                {
                    var carta = especiales.OrderBy(c => PrioridadEspecial(c)).First();
                    Mano.Remove(carta);
                    Console.WriteLine($"- {Nombre} bloquea a {siguienteJugador.Nombre} jugando {carta} -");
                    return carta;
                }
                if (mazo.Count == 0)
                    RebarajarDescarte(mazo, descarte);
                if (mazo.Count > 0)
                {
                    var robada = mazo.Pop();
                    Console.WriteLine($"- {Nombre} roba con esperanza de detener a {siguienteJugador.Nombre}: {robada} -");
                    if (robada.PuedeJugarSobre(cartaActual))
                    {
                        Console.WriteLine($"{Nombre} juega inmediatamente {robada}");
                        return robada;
                    }
                    else Mano.Add(robada);
                }
                return cartaActual;
            }
            var normales = Mano.Where(c => c.PuedeJugarSobre(cartaActual) && c.Tipo == TipoCarta.Numero).ToList();
            if (normales.Count > 0)
            {
                var carta = normales[random.Next(normales.Count)];
                Mano.Remove(carta);
                Console.WriteLine($"{Nombre} juega {carta}");
                return carta;
            }
            var jugables = Mano.Where(c => c.PuedeJugarSobre(cartaActual)).ToList();
            if (jugables.Count > 0)
            {
                var carta = jugables[random.Next(jugables.Count)];
                Mano.Remove(carta);
                Console.WriteLine($"{Nombre} juega {carta}");
                return carta;
            }
            if (mazo.Count == 0)
                RebarajarDescarte(mazo, descarte);
            if (mazo.Count > 0)
            {
                var robada = mazo.Pop();
                Console.WriteLine($"{Nombre} roba {robada}");
                if (robada.PuedeJugarSobre(cartaActual))
                {
                    Console.WriteLine($"{Nombre} juega inmediatamente {robada}");
                    return robada;
                }
                else Mano.Add(robada);
            }
            return cartaActual;
        }
        private int PrioridadEspecial(UnoCarta carta)
        {
            return carta.Tipo switch
            {
                TipoCarta.MasCuatro => 1,
                TipoCarta.MasDos => 2,
                TipoCarta.Bloqueo => 3,
                _ => 4
            };
        }
        private void RebarajarDescarte(Stack<UnoCarta> mazo, Stack<UnoCarta> descarte)
        {
            if (descarte.Count <= 1)
            {
                Console.WriteLine("- No hay suficientes cartas para rebarajar. Se continúa con el mazo actual -");
                return;
            }
            Console.WriteLine("\n- El mazo se ha agotado, rebarajando el descarte -");
            UnoCarta cartaSuperior = descarte.Pop();
            var cartasParaReusar = descarte.ToList();
            descarte.Clear();
            var rnd = new Random();
            foreach (var c in cartasParaReusar.OrderBy(x => rnd.Next()))
                mazo.Push(c);
            descarte.Push(cartaSuperior);
        }
    }
}