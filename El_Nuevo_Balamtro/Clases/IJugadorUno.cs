using System;

namespace El_Nuevo_Balamtro.Clases
{
    public interface IJugadorUno
    {
        string Nombre { get; set; }
        List<CartaUno> Mano { get; }
        int NumeroDeCartas { get; }
        CartaUno DecidirJugada(CartaUno cartaEnMesa, IJugadorUno siguienteJugador);
        void RecibirCarta(CartaUno carta);
        void JugarCarta(CartaUno carta);
        List<CartaUno> ObtenerCartasJugables(CartaUno cartaEnMesa);
        bool HaGanado();
        void GritarUno();
        void ReiniciarMano();
    }
}