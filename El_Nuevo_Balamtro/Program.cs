using System;
using El_Nuevo_Balamtro.Simulador;
using El_Nuevo_Balamtro.Uno;
using El_Nuevo_Balamtro.Blackjack;
class Program
{
    static void Main()
    {
        Console.WriteLine("Proyecto Juegos de Cartas - Simulador");
        Console.WriteLine("Seleccione el juego a ejecutar:");
        Console.WriteLine("1) UNO");
        Console.WriteLine("2) Blackjack");
        Console.Write("Opción: ");
        var opcion = Console.ReadLine();
        if (opcion == "1")
        {
            var uno = new UnoJuego();
            GameSimulator.Ejecutar(uno);
        }
        else if (opcion == "2")
        {
            var bj = new BlackjackJuego(Configuracion.BlackjackRondas);
            GameSimulator.Ejecutar(bj);
        }
        else
        {
            Console.WriteLine("Opción no válida. Saliendo...");
        }
        Console.WriteLine("\nPresiona ENTER para terminar.");
        Console.ReadLine();
    }
}