using System;
namespace JuegosDeCartas.Core
{
    public abstract class Juego
    {
        public string Nombre { get; protected set; }
        public Juego(string nombre)
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