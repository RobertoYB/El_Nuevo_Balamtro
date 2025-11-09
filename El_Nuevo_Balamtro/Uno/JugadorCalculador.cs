using System;
using El_Nuevo_Balamtro.Clases;

namespace El_Nuevo_Balamtro.Uno
{
    public class JugadorCalculador : IJugadorUno
    {
        public string Nombre { get; set; }
        public List<Carta> Mano { get; private set; }
        public int NumeroDeCartas => Mano.Count;

        public JugadorCalculador(string nombre)
        {
            Nombre = nombre;
            Mano = new List<Carta>();
        }
    public Carta DecidirJugada(Carta cartaEnMesa, IJugadorUno siguienteJugador)
    {
      if (cartaEnMesa == null)
        throw new ArgumentNullException(nameof(cartaEnMesa));

      List<Carta> cartasJugables = ObtenerCartasJugables(cartaEnMesa);

      if (cartasJugables.Count == 0)
        return null;

      bool siguienteJugadorCasiGana = siguienteJugador != null &&
                                     siguienteJugador.NumeroDeCartas == 1;

      if (siguienteJugadorCasiGana)
      {
        var masCuatro = cartasJugables.FirstOrDefault(c =>
        {
          if (c is UnoCarta unoCarta)
            return unoCarta.Especial == UnoCarta.TipoEspecial.MasCuatro;
          return false;
        });

        if (masCuatro != null)
        {
          Console.WriteLine($"   💡 {Nombre}: ¡El siguiente jugador casi gana! Usaré +4");
          return masCuatro;
        }

        var masDos = cartasJugables.FirstOrDefault(c =>
        {
          if (c is UnoCarta unoCarta)
            return unoCarta.Especial == UnoCarta.TipoEspecial.MasDos;
          return false;
        });

        if (masDos != null)
        {
          Console.WriteLine($"   💡 {Nombre}: ¡El siguiente jugador casi gana! Usaré +2");
          return masDos;
        }


        var bloqueo = cartasJugables.FirstOrDefault(c =>
        {
          if (c is UnoCarta unoCarta)
            return unoCarta.Especial == UnoCarta.TipoEspecial.Bloqueo;
          return false;
        });

        if (bloqueo != null)
        {
          Console.WriteLine($"   💡 {Nombre}: ¡El siguiente jugador casi gana! Usaré Bloqueo");
          return bloqueo;
        }


        var reversa = cartasJugables.FirstOrDefault(c =>
        {
          if (c is UnoCarta unoCarta)
            return unoCarta.Especial == UnoCarta.TipoEspecial.Reversa;
          return false;
        });

        if (reversa != null)
        {
          Console.WriteLine($"   💡 {Nombre}: ¡El siguiente jugador casi gana! Usaré Reversa");
          return reversa;
        }



        var cambioColor = cartasJugables.FirstOrDefault(c =>
        {
          if (c is UnoCarta unoCarta)
            return unoCarta.Especial == UnoCarta.TipoEspecial.CambioColor;
          return false;
        });

        if (cambioColor != null)
        {
          Console.WriteLine($"   💡 {Nombre}: ¡El siguiente jugador casi gana! Usaré Cambio Color");
          return cambioColor;
        }

        Console.WriteLine($"   💡 {Nombre}: No tengo especiales. Tomaré carta del mazo...");
        return null;
      }

      else
      {
        var cartasNormales = cartasJugables.Where(c =>
        {
          if (c is UnoCarta unoCarta)
            return unoCarta.Tipo == UnoCarta.TipoCarta.Normal;
          return false;
        }).ToList();

        if (cartasNormales.Count > 0)
        {
          return cartasNormales[0];
        }
        else
        {
          Console.WriteLine($"   💡 {Nombre}: Solo tengo especiales disponibles");
          return cartasJugables[0];
        }
      }
    }
        


    public void RecibirCarta(Carta carta)
    {
      if (carta == null)
        throw new ArgumentNullException(nameof(carta));

      Mano.Add(carta);
    }
        


    public void JugarCarta(Carta carta)
    {
      if (carta == null)
        throw new ArgumentNullException(nameof(carta));

      if (!Mano.Contains(carta))
        throw new InvalidOperationException($"{Nombre} no tiene la carta {carta} en su mano");

      Mano.Remove(carta);

      if (NumeroDeCartas == 1)
        GritarUno();
    }
        



    public List<Carta> ObtenerCartasJugables(Carta cartaEnMesa)
    {
      if (cartaEnMesa == null || !(cartaEnMesa is UnoCarta unoCartaEnMesa))
        return new List<Carta>();

      return Mano.Where(carta =>
      {
        if (carta is UnoCarta unoCarta)
          return unoCarta.PuedeJugarseSobre(unoCartaEnMesa);
        return false;
      }).ToList();
    }
        


    public bool HaGanado()
    {
      return NumeroDeCartas == 0;
    }
        

        public void GritarUno()
        {
            Console.WriteLine($"🎯 ¡{Nombre} grita estratégicamente: UNO!");
        }

        public void ReiniciarMano()
        {
            Mano.Clear();
        }

        public override string ToString()
        {
            return $"{Nombre} (Calculador) - Cartas: {NumeroDeCartas}";
        }
    }
}