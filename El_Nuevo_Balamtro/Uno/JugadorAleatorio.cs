using System;
namespace El_Nuevo_Balamtro.Uno
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
                El_Nuevo_Balamtro.Core.LogHelper.Accion($"{Nombre} juega {carta}");
                return carta;
            }
            if (mazo.Count > 0)
            {
                var robada = mazo.Pop();
                El_Nuevo_Balamtro.Core.LogHelper.Info($"{Nombre} roba una carta ({robada})");
                if (robada.PuedeJugarSobre(cartaActual))
                {
                    El_Nuevo_Balamtro.Core.LogHelper.Accion($"{Nombre} juega inmediatamente {robada}");
                    return robada;
                }
                else
                {
                    Mano.Add(robada);
                }
            }
            else
            {
                El_Nuevo_Balamtro.Core.LogHelper.Info($"{Nombre} no puede jugar y el mazo está vacío.");
            }
            return cartaActual;
        }
    }
}