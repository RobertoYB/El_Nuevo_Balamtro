using System;

namespace El_Nuevo_Balamtro.Clases
{
    public class CartaUno
    {
        public enum TipoCarta
        {
            Normal,
            Especial,
            Comodin
        }

        public enum ColorCarta
        {
            Rojo,
            Azul,
            Verde,
            Amarillo,
            SinColor // Para comodines
        }

        public enum TipoEspecial
        {
            Ninguno,      // Para cartas normales
            Bloqueo,
            Reversa,
            MasDos,
            CambioColor,
            MasCuatro
        }

        public TipoCarta Tipo { get; set; }
        public ColorCarta Color { get; set; }
        public int? Numero { get; set; } // Nuleable porque las especiales no tienen numero
        public TipoEspecial Especial { get; set; }

        // Constructor para cartas normales
        public CartaUno(ColorCarta color, int numero)
        {
            Tipo = TipoCarta.Normal;
            Color = color;
            Numero = numero;
            Especial = TipoEspecial.Ninguno;
        }

        // Constructor para cartas especiales con color
        public CartaUno(ColorCarta color, TipoEspecial especial)
        {
            Tipo = TipoCarta.Especial;
            Color = color;
            Numero = null;
            Especial = especial;
        }

        // Constructor para cartas comodín (sin color)
        public CartaUno(TipoEspecial especial)
        {
            Tipo = TipoCarta.Comodin;
            Color = ColorCarta.SinColor;
            Numero = null;
            Especial = especial;
        }

        public override string ToString()
        {
            if (Tipo == TipoCarta.Normal)
                return $"{Color} {Numero}";
            else if (Tipo == TipoCarta.Comodin)
                return $"Comodín {Especial}";
            else
                return $"{Color} {Especial}";
        }
        public bool PuedeJugarseSobre(CartaUno cartaEnMesa)
        {
            // Los comodines siempre se pueden jugar
            if (Tipo == TipoCarta.Comodin)
                return true;

            // Cartas normales: mismo color o mismo número
            if (Tipo == TipoCarta.Normal && cartaEnMesa.Tipo == TipoCarta.Normal)
            {
                return Color == cartaEnMesa.Color || Numero == cartaEnMesa.Numero;
            }

            // Cartas especiales: mismo color o mismo tipo
            if (Tipo == TipoCarta.Especial)
            {
                return Color == cartaEnMesa.Color || 
                       (cartaEnMesa.Tipo == TipoCarta.Especial && Especial == cartaEnMesa.Especial);
            }

            // Carta normal sobre especial: debe coincidir el color
            if (Tipo == TipoCarta.Normal && cartaEnMesa.Tipo == TipoCarta.Especial)
            {
                return Color == cartaEnMesa.Color;
            }

            return false;
        }
    }
}
