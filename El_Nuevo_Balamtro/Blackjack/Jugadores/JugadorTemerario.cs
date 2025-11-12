using System;
namespace El_Nuevo_Balamtro.Blackjack
{
    public class JugadorTemerario : JugadorBlackjack
    {
        public JugadorTemerario(string nombre) : base(nombre) { }
        public override bool QuiereCarta() => CalcularPuntos() < 21;
    }
}