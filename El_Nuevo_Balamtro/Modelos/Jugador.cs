using System;
namespace El_Nuevo_Balamtro.Modelos
{
    public abstract class Jugador<T> where T : Carta
    {
        public string Nombre { get; set; }
        public List<T> Mano { get; set; } = new List<T>();
        protected Jugador(string nombre)
        {
            Nombre = nombre;
        }
        public override string ToString() => Nombre;
    }
}