using System;
using El_Nuevo_Balamtro.Clases;

namespace El_Nuevo_Balamtro.Blackjack
{
    public class JugadorAleatorio : IJugadorUno
    {
        private Random _random;

        public string Nombre { get; set; }
        public List<CartaUno> Mano { get; private set; }
        public int NumeroDeCartas => Mano.Count;

        public JugadorAleatorio(string nombre)
        {
            Nombre = nombre;
            Mano = new List<CartaUno>();
            _random = new Random();
        }
        public CartaUno DecidirJugada(CartaUno cartaEnMesa, IJugadorUno siguienteJugador)
        {
            if (cartaEnMesa == null)
                throw new ArgumentNullException(nameof(cartaEnMesa));

            List<CartaUno> cartasJugables = ObtenerCartasJugables(cartaEnMesa);

            if (cartasJugables.Count == 0)
                return null;

            int indiceAleatorio = _random.Next(cartasJugables.Count);
            CartaUno cartaElegida = cartasJugables[indiceAleatorio];

            return cartaElegida;
        }

        public void RecibirCarta(CartaUno carta)
        {
            if (carta == null)
                throw new ArgumentNullException(nameof(carta));

            Mano.Add(carta);
        }

        public void JugarCarta(CartaUno carta)
        {
            if (carta == null)
                throw new ArgumentNullException(nameof(carta));

            if (!Mano.Contains(carta))
                throw new InvalidOperationException($"{Nombre} no tiene la carta {carta} en su mano");

            Mano.Remove(carta);

            if (NumeroDeCartas == 1)
                GritarUno();
        }

        public List<CartaUno> ObtenerCartasJugables(CartaUno cartaEnMesa)
        {
            if (cartaEnMesa == null)
                return new List<CartaUno>();

            return Mano.Where(carta => carta.PuedeJugarseSobre(cartaEnMesa)).ToList();
        }

        public bool HaGanado()
        {
            return NumeroDeCartas == 0;
        }

        public void GritarUno()
        {
            Console.WriteLine($"🎉 ¡{Nombre} grita: UUUUNOOOOO!");
        }

        public void ReiniciarMano()
        {
            Mano.Clear();
        }
        public override string ToString()
        {
            return $"{Nombre} (Aleatorio) - Cartas: {NumeroDeCartas}";
        }
    }
}
