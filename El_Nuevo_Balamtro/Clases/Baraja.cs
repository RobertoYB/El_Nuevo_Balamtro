using System;

namespace El_Nuevo_Balamtro.Clases
{
    public abstract class Baraja<T> where T : Carta
    {
        protected List<T> cartas = new List<T>();
        protected Random random = new Random();
        public int Conteo => cartas.Count;
        public void AgregarCarta(T carta) => cartas.Add(carta);
        public T TomarCarta()
        {
            if (cartas.Count == 0)
            {
                throw new InvalidOperationException("Mazo vacío.");
            }
            var carta = cartas[^1];
            cartas.RemoveAt(cartas.Count - 1);
            return carta;
        }
        public void Barajear()
        {
            cartas = cartas.OrderBy(x => random.Next()).ToList();
        }
        public void Rellenar(IEnumerable<T> nuevasCartas)
        {
            cartas.AddRange(nuevasCartas);
            Barajear();
        }
    }
}
