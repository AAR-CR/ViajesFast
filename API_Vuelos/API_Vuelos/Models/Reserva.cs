using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API_Vuelos.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int VueloId { get; set; }
        public DateTime FechaReserva { get; set; }



        [ForeignKey("UsuarioId")]
        [JsonIgnore]
        public Usuario? Usuario { get; set; }  // Propiedad de navegación

        [ForeignKey("VueloId")]
        [JsonIgnore]
        public Vuelo? Vuelo { get; set; }  // Propiedad de navegación
    }

}

