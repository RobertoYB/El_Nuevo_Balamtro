using System;

namespace El_Nuevo_Balamtro.Clases
{
    
    public interface IJugador
    {
        string Nombre { get; set; }
        
        List<Carta> Mano { get; }
        int PuntuacionActual { get; }
        bool DeseaPedirCarta();
        void RecibirCarta(Carta carta);
        int CalcularPuntuacion();
        void ReiniciarMano();
        bool SeHaPasado();
    }
}
