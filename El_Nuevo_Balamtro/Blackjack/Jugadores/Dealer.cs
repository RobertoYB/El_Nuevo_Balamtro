using System;
namespace El_Nuevo_Balamtro.Blackjack
{
    public class Dealer : JugadorBlackjack
    {
        public Dealer() : base("Dealer") { }
        public override bool QuiereCarta() => CalcularPuntos() < 17;
    }
}