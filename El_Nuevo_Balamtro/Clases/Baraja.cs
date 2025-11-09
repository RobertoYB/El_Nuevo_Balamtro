using System;

namespace El_Nuevo_Balamtro.Clases
{

  public abstract class Baraja<T> where T : Carta
  {
    protected List<T> _cartas;
    protected Random _random;

    public Baraja()
    {
      _cartas = new List<T>();
      _random = new Random();
    }

    public int CartasRestantes => _cartas.Count;

    public bool EstaVacia => _cartas.Count == 0;

    public abstract void Inicializar();


    public virtual void Barajar()
    {
      int n = _cartas.Count;
      while (n > 1)
      {
        n--;
        int k = _random.Next(n + 1);
        T temp = _cartas[k];
        _cartas[k] = _cartas[n];
        _cartas[n] = temp;
      }
    }

    public virtual T TomarCarta()
    {
      if (EstaVacia)
        throw new InvalidOperationException("No hay cartas en la baraja");

      T carta = _cartas[0];
      _cartas.RemoveAt(0);
      return carta;
    }

    public virtual void AgregarCarta(T carta)
    {
      if (carta == null)
        throw new ArgumentNullException(nameof(carta));

      _cartas.Add(carta);
    }

    public virtual void AgregarCartas(IEnumerable<T> cartas)
    {
      if (cartas == null)
        throw new ArgumentNullException(nameof(cartas));

      _cartas.AddRange(cartas);
    }


    public virtual void Reiniciar()
    {
      _cartas.Clear();
    }

  }
    
}