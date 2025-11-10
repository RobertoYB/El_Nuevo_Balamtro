using System;
namespace El_Nuevo_Balamtro.Clases
{
    public abstract class Jugador<T> where T : Carta
    {
        public string Nombre { get; set; }
        public List<T> Mano { get; set; } = new List<T>();
        public Jugador(string nombre)
        {
            Nombre = nombre;
        }
        public abstract T? JugarTurno(T cartaActual);
        public override string ToString() => Nombre;
    }
}