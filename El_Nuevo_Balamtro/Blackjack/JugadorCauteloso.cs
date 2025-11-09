using System;

namespace El_Nuevo_Balamtro.Blackjack
{
    public class JugadorCauteloso : IJugador
    {
        private int _puntoDeCorte;

        public string Nombre { get; set; }
        public List<Carta> Mano { get; private set; }
        //propiedad calculada
        public int PuntuacionActual => CalcularPuntuacion();
        //valida que este entre 1 y 21 y si no 
        public int PuntoDeCorte 
        { 
            get => _puntoDeCorte;
            set
            {
                if (value < 1 || value > 21)
                    throw new ArgumentException("El punto de corte debe estar entre 1 y 21");
                _puntoDeCorte = value;
            }
        }

        public JugadorCauteloso(string nombre, int puntoDeCorte = 17)
        {
            Nombre = nombre;
            PuntoDeCorte = puntoDeCorte;
            Mano = new List<Carta>();
        }

        public bool DeseaPedirCarta()
        {
            return PuntuacionActual < PuntoDeCorte && !SeHaPasado();
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
            return $"{Nombre} (Cauteloso - Límite: {PuntoDeCorte}) - Puntuación: {PuntuacionActual}";
        }
    }
}
