using El_Nuevo_Balamtro.Clases;
using System;
namespace El_Nuevo_Balamtro.Uno
{
    public abstract class JugadorBase : Jugador<UnoCarta>
    {
        public JugadorBase(string nombre) : base(nombre) { }
        public abstract UnoCarta? JugarTurno(UnoCarta cartaActual, Stack<UnoCarta> mazo, Stack<UnoCarta> descarte, List<JugadorBase> jugadores, int indiceJugador);
        public override UnoCarta? JugarTurno(UnoCarta cartaActual) => cartaActual;
    }
}