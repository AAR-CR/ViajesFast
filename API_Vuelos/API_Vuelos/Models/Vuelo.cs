using System.Text.Json.Serialization;

namespace API_Vuelos.Models
{
    public class Vuelo
    {
        public int Id { get; set; }
        public string Destino { get; set; }
        public DateTime Fecha { get; set; }
        public int NumeroDePasajeros { get; set; }
        public string Precio { get; set; }
        public string Aerolinea { get; set; }

        [JsonIgnore]
        public ICollection<Reserva>? Reservas { get; set; }  // Propiedad de navegación
    }
}
