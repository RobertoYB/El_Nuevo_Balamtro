using System;

namespace El_Nuevo_Balamtro.Clases
{
    public class Carta
    {
        public string Palo { get; set; }
        public string Valor { get; set; }
        public int Puntos { get; set; }

        public Carta(string palo, string valor, int puntos)
        {
            Palo = palo;
            Valor = valor;
            Puntos = puntos;
        }

        public override string ToString()
        {
            return $"{Valor} de {Palo}";
        }
    }
}