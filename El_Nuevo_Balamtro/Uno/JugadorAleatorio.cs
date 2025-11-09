using System;
namespace JuegosDeCartas.Uno
{
    public class JugadorAleatorio : JugadorBase
    {
        private static Random random = new Random();
        public JugadorAleatorio(string nombre) : base(nombre) { }
        public override UnoCarta? JugarTurno(UnoCarta cartaActual, Stack<UnoCarta> mazo, Stack<UnoCarta> descarte, List<JugadorBase> jugadores, int indiceJugador)
        {
            var jugables = Mano.Where(c => c.PuedeJugarSobre(cartaActual)).ToList();
            if (jugables.Count > 0)
            {
                var carta = jugables[random.Next(jugables.Count)];
                Mano.Remove(carta);
                JuegosDeCartas.Core.LogHelper.Accion($"{Nombre} juega {carta}");
                return carta;
            }
            if (mazo.Count > 0)
            {
                var robada = mazo.Pop();
                JuegosDeCartas.Core.LogHelper.Info($"{Nombre} roba una carta ({robada})");
                if (robada.PuedeJugarSobre(cartaActual))
                {
                    JuegosDeCartas.Core.LogHelper.Accion($"{Nombre} juega inmediatamente {robada}");
                    return robada;
                }
                else
                {
                    Mano.Add(robada);
                }
            }
            else
            {
                JuegosDeCartas.Core.LogHelper.Info($"{Nombre} no puede jugar y el mazo está vacío.");
            }
            return cartaActual;
        }
    }
}