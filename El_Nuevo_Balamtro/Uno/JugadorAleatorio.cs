using System;
namespace El_Nuevo_Balamtro.Uno
{
    public class JugadorAleatorio : JugadorUno
    {
        private static Random random = new Random();
        public JugadorAleatorio(string nombre) : base(nombre) { }
        public override UnoCarta JugarTurno(
            UnoCarta cartaActual,
            Stack<UnoCarta> mazo,
            Stack<UnoCarta> descarte,
            List<JugadorUno> jugadores,
            int indice)
        {
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
        protected void RebarajarDescarte(Stack<UnoCarta> mazo, Stack<UnoCarta> descarte)
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