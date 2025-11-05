using System;

namespace El_Nuevo_Balamtro.Clases
{
    public abstract class Juego
    {
        public string Nombre { get; protected set; }
        public Juego(string Nombre)
        {
            Nombre = nombre;
        }
        public abstract void Iniciar();
        public virtual void MostrarTitulo()
        {
            Console.WriteLine($"\n===== {Nombre.ToUpper()} =====");
        }
    }
}
