using System;

namespace El_Nuevo_Balamtro.Blackjack
{
    public class JugadorTemerario : IJugador
    {
        public string Nombre { get; set; }
        public List<Carta> Mano { get; private set; }
        public int PuntuacionActual => CalcularPuntuacion();

        public JugadorTemerario(string nombre)
        {
            Nombre = nombre;
            Mano = new List<Carta>();
        }

        public bool DeseaPedirCarta()
        {
            return PuntuacionActual < 21 && !SeHaPasado();
        }

        public void RecibirCarta(Carta carta)
        {
            if (carta == null)
                throw new ArgumentNullException(nameof(carta));
            
            Mano.Add(carta);
        }

        public int CalcularPuntuacion()
        {
            if (Mano.Count == 0)
                return 0;

            int puntuacion = Mano.Sum(c => c.Puntos);
            int ases = Mano.Count(c => c.Valor == "A");

            while (puntuacion > 21 && ases > 0)
            {
                puntuacion -= 10;
                ases--;
            }

            return puntuacion;
        }

        public void ReiniciarMano()
        {
            Mano.Clear();
        }

        public bool SeHaPasado()
        {
            return PuntuacionActual > 21;
        }

        public override string ToString()
        {
            return $"{Nombre} (Temerario) - Puntuación: {PuntuacionActual}";
        }
    }
}
