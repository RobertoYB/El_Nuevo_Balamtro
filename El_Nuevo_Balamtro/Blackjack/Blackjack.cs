using System;
using El_Nuevo_Balamtro.Modelos;
namespace El_Nuevo_Balamtro.Blackjack
{
    public class BlackjackJuego : Juego
    {
        private BarajaBlackjack baraja = new BarajaBlackjack();
        private List<JugadorBlackjack> jugadores;
        private Dealer dealer = new Dealer();
        private int rondas;
        public BlackjackJuego(int rondas, List<JugadorBlackjack> jugadores)
            : base("Blackjack 21")
        {
            this.rondas = rondas;
            this.jugadores = jugadores;
        }
        public override void Iniciar()
        {
            MostrarTitulo();
            for (int i = 1; i <= rondas; i++)
            {
                Console.WriteLine($"\n- Ronda {i} -");
                JugarRonda();
            }
            Console.WriteLine("\n--- RESULTADOS FINALES ---");
            foreach (var j in jugadores)
                Console.WriteLine($"{j.Nombre}: {j.Victorias} victorias");
            Console.WriteLine($"Dealer: {dealer.Victorias} victorias");
            int max = Math.Max(dealer.Victorias, jugadores.Max(j => j.Victorias));
            var ganadores = new List<string>();
            if (dealer.Victorias == max) ganadores.Add("Dealer (la casa)");
            ganadores.AddRange(jugadores.Where(j => j.Victorias == max).Select(j => j.Nombre));
            Console.WriteLine($"\nGanador(es): {string.Join(", ", ganadores)}");
        }
        private void JugarRonda()
        {
            baraja = new BarajaBlackjack();
            dealer.NuevaRonda();
            foreach (var j in jugadores)
            {
                j.NuevaRonda();
                j.RecibirCarta(baraja.TomarCarta());
                j.RecibirCarta(baraja.TomarCarta());
            }
            dealer.RecibirCarta(baraja.TomarCarta());
            dealer.RecibirCarta(baraja.TomarCarta());
            foreach (var j in jugadores)
            {
                Console.WriteLine($"\nTurno de {j.Nombre}:");
                while (j.QuiereCarta())
                {
                    j.RecibirCarta(baraja.TomarCarta());
                    if (j.SePaso()) break;
                }
                j.MostrarMano();
            }
            Console.WriteLine("\nTurno del Dealer:");
            while (dealer.QuiereCarta())
                dealer.RecibirCarta(baraja.TomarCarta());
            dealer.MostrarMano();
            int puntosDealer = dealer.CalcularPuntos();
            foreach (var j in jugadores)
            {
                int puntos = j.CalcularPuntos();
                if (!j.SePaso() && (dealer.SePaso() || puntos > puntosDealer))
                {
                    j.Victorias++;
                    Console.WriteLine($"{j.Nombre} gana esta ronda.");
                }
                else Console.WriteLine($"{j.Nombre} pierde esta ronda.");
            }
            if (!dealer.SePaso())
            {
                bool dealerVencedor = jugadores.All(j => j.SePaso() || puntosDealer >= j.CalcularPuntos());
                if (dealerVencedor)
                {
                    dealer.Victorias++;
                    Console.WriteLine("El Dealer gana esta ronda.");
                }
            }
        }
    }
}