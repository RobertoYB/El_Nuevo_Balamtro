using System;
namespace El_Nuevo_Balamtro.Blackjack
{
    public class JugadorCauteloso : JugadorBlackjack
    {
        private int limite;
        public JugadorCauteloso(string nombre, int limite) : base(nombre)
        {
            this.limite = limite;
        }
        public override bool QuiereCarta() => CalcularPuntos() < limite;
    }
}