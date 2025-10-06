namespace AppForSEII2526.API.Models
{
    public class Compra
    {
        public string ApellidoCliente { get; set; }
        public string? CorreoElectronico { get; set; }
        public string DireccionEnvio { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCompra { get; set; }

        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string NombreCliente { get; set; }


        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.05, float.MaxValue, ErrorMessage = "El precio minimo es 0.05")]
        [Precision(10, 2)]

        public decimal PrecioTotal { get; set; }
        public int? Telefono { get; set; }
        public IList<CompraItem> CompraItem { get; set; }
        public TiposMetodoPago TipoMetodoPago { get; set; }
    }


}
