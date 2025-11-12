using System;
using El_Nuevo_Balamtro.Modelos;
namespace El_Nuevo_Balamtro.Blackjack
{
    public class CartaBlackjack : Carta
    {
        public string Valor { get; private set; }
        public string Palo { get; private set; }
        public CartaBlackjack(string valor, string palo)
            : base($"{valor} de {palo}")
        {
            Valor = valor;
            Palo = palo;
        }
        public int ObtenerPuntos(int totalActual = 0)
        {
            if (int.TryParse(Valor, out int numero)) return numero;
            if (Valor == "A") return (totalActual + 11 > 21) ? 1 : 11;
            return 10;
        }
    }
}