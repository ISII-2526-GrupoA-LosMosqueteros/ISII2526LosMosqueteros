namespace AppForSEII2526.API.DTOs.AlquilerDTOs
{
    public class CreacionAlquilerDTO
    {
        public CreacionAlquilerDTO(string name, string surname, string direccionEnvio, TiposMetodoPago metodoPago, DateTime fechaInicio, DateTime fechaFin, IList<AlquilarItemDTO> alquilerItems,string? telefono = null, string? correoElectronico = null)
        {
            Name = name ;
            Surname = surname;
            DireccionEnvio = direccionEnvio;
            MetodoPago = metodoPago;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Telefono = telefono;
            CorreoElectronico = correoElectronico;
            AlquilerItems = alquilerItems; 
        }

      

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar su nombre.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe ingresar sus apellidos.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Los apellidos deben tener entre 2 y 50 caracteres.")]
        [Display(Name = "Apellidos")]
        public string Surname { get; set; }

        [Display(Name = "Teléfono (opcional)")]
        public string? Telefono { get; set; }

        [EmailAddress]
        [Display(Name = "Correo Electrónico (opcional)")]
        public string? CorreoElectronico { get; set; }

        public string DireccionEnvio { get; set; }

        [Required]
        [Display(Name = "Método de Pago")]
        public TiposMetodoPago MetodoPago { get; set; }


        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }


        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFin { get; set; }

        [Required]
        public IList<AlquilarItemDTO> AlquilerItems { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.5, 100, ErrorMessage = "Minimo es 0.5 y maximo 100")]
        [Display(Name = "Precio Total de Alquiler")]
        [Precision(10, 2)]
        public decimal PrecioTotal { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CreacionAlquilerDTO dto &&    
                   DireccionEnvio == dto.DireccionEnvio &&
                   Name == dto.Name &&
                   Surname == dto.Surname &&
                   MetodoPago == dto.MetodoPago &&
                   AlquilerItems.SequenceEqual(dto.AlquilerItems) &&
                   PrecioTotal == dto.PrecioTotal;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Surname, DireccionEnvio, FechaInicio, FechaFin, MetodoPago, PrecioTotal);
        }
    }
}
