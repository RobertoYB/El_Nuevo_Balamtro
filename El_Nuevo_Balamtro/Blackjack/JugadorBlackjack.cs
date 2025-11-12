using System;
using El_Nuevo_Balamtro.Modelos;
namespace El_Nuevo_Balamtro.Blackjack
{
    public abstract class JugadorBlackjack : Jugador<CartaBlackjack>
    {
        public int Victorias { get; set; }
        public JugadorBlackjack(string nombre) : base(nombre) { }
        public void RecibirCarta(CartaBlackjack carta)
        {
            Mano.Add(carta);
            Console.WriteLine($"{Nombre} recibe: {carta}");
        }
        public int CalcularPuntos()
        {
            int total = 0;
            foreach (var c in Mano)
                total += c.ObtenerPuntos(total);
            return total;
        }
        public bool SePaso() => CalcularPuntos() > 21;
        public void NuevaRonda() => Mano.Clear();
        public void MostrarMano() =>
            Console.WriteLine($"{Nombre} tiene: {string.Join(", ", Mano)} (Total: {CalcularPuntos()})");
        public abstract bool QuiereCarta();
    }
}