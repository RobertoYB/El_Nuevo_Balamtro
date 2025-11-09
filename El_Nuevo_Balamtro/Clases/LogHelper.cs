using System;
namespace JuegosDeCartas.Core
{
    public static class LogHelper
    {
        public static void Info(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"[INFO] {mensaje}");
            Console.ResetColor();
        }
        public static void Accion(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"→ {mensaje}");
            Console.ResetColor();
        }
        public static void Exito(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"🏆 {mensaje}");
            Console.ResetColor();
        }
        public static void Advertencia(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"⚠️ {mensaje}");
            Console.ResetColor();
        }
    }
}