using System;
using El_Nuevo_Balamtro.Modelos;
namespace El_Nuevo_Balamtro.Uno
{
    public abstract class JugadorUno : Jugador<UnoCarta>
    {
        public JugadorUno(string nombre) : base(nombre) { }
        public abstract UnoCarta JugarTurno(
            UnoCarta cartaActual,
            Stack<UnoCarta> mazo,
            Stack<UnoCarta> descarte,
            List<JugadorUno> jugadores,
            int indice);
    }
}