using System;
namespace El_Nuevo_Balamtro.Modelos
{
    public abstract class Carta
    {
        public string Nombre { get; protected set; }
        public Carta(string nombre) => Nombre = nombre;
        public override string ToString() => Nombre;
    }
}