using System;
using El_Nuevo_Balamtro.Modelos;
namespace El_Nuevo_Balamtro.Blackjack
{
    public class BarajaBlackjack : Baraja<CartaBlackjack>
    {
        public BarajaBlackjack()
        {
            string[] palos = { "Corazones", "Diamantes", "Tréboles", "Espadas" };
            string[] valores = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
            foreach (var palo in palos)
                foreach (var valor in valores)
                    AgregarCarta(new CartaBlackjack(valor, palo));
            Barajear();
        }
    }
}