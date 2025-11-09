using JuegosDeCartas.Core;
using System;
namespace JuegosDeCartas.Uno
{
    public enum TipoCarta
    {
        Numero,
        MasDos,
        Reversa,
        Bloqueo,
        CambioColor,
        MasCuatro
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
            Nombre = BuildNombre();
        }
        private string BuildNombre()
        {
            if (Tipo == TipoCarta.Numero)
                return $"{Color} {Valor}";
            else
                return Tipo == TipoCarta.CambioColor || Tipo == TipoCarta.MasCuatro
                    ? $"{Tipo}"
                    : $"{Color} {Tipo}";
        }
        public override string ToString() => Nombre;
        public bool PuedeJugarSobre(UnoCarta otra)
        {
            if (Tipo == TipoCarta.CambioColor || Tipo == TipoCarta.MasCuatro)
                return true;
            if (Color == otra.Color) return true;
            if (Tipo == otra.Tipo && Tipo != TipoCarta.Numero) return true;
            if (Tipo == TipoCarta.Numero && otra.Tipo == TipoCarta.Numero && Valor == otra.Valor) return true;
            return false;
        }
    }
}