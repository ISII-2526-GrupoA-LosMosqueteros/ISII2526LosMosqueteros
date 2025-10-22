using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs
{
    public class DetalleAlquilarDTO 
    {
        public DetalleAlquilarDTO(int id, string nombre, string apellidos, string direccionEnvio, DateTime fechaAlquiler, decimal precioTotal, DateTime fechaInicio, DateTime fechaFin, IList<AlquilarItemDTO> alquilarItems)
        {
            Id = id;
            Nombre = nombre;
            Apellidos = apellidos;
            DireccionEnvio = direccionEnvio;
            FechaAlquiler = fechaAlquiler;
            PrecioTotal = precioTotal;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            AlquilarItems = alquilarItems;
        }

        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string Nombre { get; set; }
        [StringLength(50, ErrorMessage = "El apellido no puede tener más de 50 caracteres.")]
        public string Apellidos { get; set; }

        public String DireccionEnvio { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaAlquiler { get; set; }


        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.5, 100, ErrorMessage = "Minimo es 0.5 y maximo 100")]
        [Display(Name = "Precio Total de Alquiler")]
        [Precision(10, 2)]
        public decimal PrecioTotal { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFin { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        public IList<AlquilarItemDTO> AlquilarItems { get; set; }
    }
}
