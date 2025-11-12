using System;
using El_Nuevo_Balamtro.Blackjack;
using El_Nuevo_Balamtro.Uno;
namespace El_Nuevo_Balamtro
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("--- El Nuevo Balamtro ---");
            Console.WriteLine("Creado por: Ángel Rosado, Roberto Yupit y Santiago Ortega");
            Console.WriteLine("Seleccione el juego:");
            Console.WriteLine("1) Uno");
            Console.WriteLine("2) Blackjack 21");
            Console.Write("Opción: ");
            string opcion = Console.ReadLine();
            if (opcion == "1")
            {
                var uno = new UnoJuego();
                uno.Iniciar();
            }
            else if (opcion == "2")
            {
                var jugadores = new List<JugadorBlackjack>()
                {
                    new JugadorCauteloso("Raúl", 17),
                    new JugadorCauteloso("Ángel", 15),
                    new JugadorTemerario("Santiago")
                };
                var bj = new BlackjackJuego(3, jugadores);
                bj.Iniciar();
            }
            else
            {
                Console.WriteLine("Opción inválida. Fin del programa.");
            }
            Console.WriteLine("\nPresiona cualquier tecla para salir.");
            Console.ReadKey();
        }
    }
}