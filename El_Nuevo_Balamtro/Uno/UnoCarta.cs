using System;
using El_Nuevo_Balamtro.Modelos;
namespace El_Nuevo_Balamtro.Uno
{
    public enum TipoCarta
    {
        Numero, MasDos, Reversa, Bloqueo, CambioColor, MasCuatro
    }
    public class UnoCarta : Carta
    {
        public string Color { get; set; }
        public TipoCarta Tipo { get; set; }
        public int Valor { get; set; }
        public UnoCarta(string color, TipoCarta tipo, int valor = -1) : base("")
        {
            Color = color;
            Tipo = tipo;
            Valor = valor;
            Nombre = (Tipo == TipoCarta.Numero)
                ? $"{Color} {Valor}"
                : (Tipo == TipoCarta.CambioColor || Tipo == TipoCarta.MasCuatro)
                    ? $"{Tipo}"
                    : $"{Color} {Tipo}";
        }
        public bool PuedeJugarSobre(UnoCarta otra)
        {
            if (Tipo == TipoCarta.CambioColor || Tipo == TipoCarta.MasCuatro) return true;
            if (Color == otra.Color) return true;
            if (Tipo == otra.Tipo && Tipo != TipoCarta.Numero) return true;
            if (Tipo == TipoCarta.Numero && otra.Tipo == TipoCarta.Numero && Valor == otra.Valor) return true;
            return false;
        }
    }
}