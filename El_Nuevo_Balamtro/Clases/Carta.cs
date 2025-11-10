using System;
namespace El_Nuevo_Balamtro.Clases
{
    public abstract class Carta
    {
        public string Nombre { get; protected set; }
        public Carta(string nombre)
        {
            Nombre = nombre;
        }
        public override string ToString() => Nombre;
    }
}