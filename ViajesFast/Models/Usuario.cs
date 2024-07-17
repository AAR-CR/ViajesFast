using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ViajesFast.Models
{
    public class Usuario
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }

        [Phone(ErrorMessage = "El número de teléfono no es válido.")]
        public string? Telefono { get; set; }

        public Especial? Necesidad { get; set; }

        public Preferencia? Boleto { get; set; }
        [JsonIgnore]
        public ICollection<Reserva> Reservas { get; set; }  // Propiedad de navegación

        public Usuario()
        {
            Reservas = new List<Reserva>();
        }

    }
    public enum Preferencia
    {
        Economica,
        Turista,
        Ejecutiva
    }
    public enum Especial
    {
        Ninguna,
        Motora,
        Visual,
        Auditiva,
        Otra
    }
}
