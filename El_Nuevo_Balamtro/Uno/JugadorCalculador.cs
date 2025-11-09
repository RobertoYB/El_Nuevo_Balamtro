using System;
namespace JuegosDeCartas.Uno
{
    public class JugadorCalculador : JugadorBase
    {
        public JugadorCalculador(string nombre) : base(nombre) { }
        public override UnoCarta? JugarTurno(UnoCarta cartaActual, Stack<UnoCarta> mazo, Stack<UnoCarta> descarte, List<JugadorBase> jugadores, int indiceJugador)
        {
            JugadorBase siguiente = jugadores[(indiceJugador + 1) % jugadores.Count];
            var jugables = Mano.Where(c => c.PuedeJugarSobre(cartaActual)).ToList();
            if (siguiente.Mano.Count == 1)
            {
                var cartaEspecial = jugables.FirstOrDefault(c =>
                    c.Tipo == TipoCarta.MasDos || c.Tipo == TipoCarta.MasCuatro ||
                    c.Tipo == TipoCarta.Bloqueo || c.Tipo == TipoCarta.Reversa);

                if (cartaEspecial != null)
                {
                    Mano.Remove(cartaEspecial);
                    JuegosDeCartas.Core.LogHelper.Accion($"{Nombre} juega {cartaEspecial} para frenar a {siguiente.Nombre}");
                    return cartaEspecial;
                }
            }
            var cartaNormal = jugables.FirstOrDefault(c => c.Tipo == TipoCarta.Numero);
            if (cartaNormal != null)
            {
                Mano.Remove(cartaNormal);
                JuegosDeCartas.Core.LogHelper.Accion($"{Nombre} juega {cartaNormal}");
                return cartaNormal;
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
            JuegosDeCartas.Core.LogHelper.Info($"{Nombre} no puede jugar.");
            return cartaActual;
        }
    }
}