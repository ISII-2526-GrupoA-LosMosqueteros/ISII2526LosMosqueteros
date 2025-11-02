namespace AppForSEII2526.API.Models
{
    public class Reparacion
    {
        public Reparacion()
        {
        }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaEntrega { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaRecogida { get; set; }

        public int Id { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.5, 100, ErrorMessage = "Minimo es 0.5 y maximo 100")]
        [Display(Name = "Precio Total de Alquiler")]
        [Precision(10, 2)]
        public decimal PrecioTotal { get; set; }
        public TiposMetodoPago TiposMetodoPago { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public IList<ReparacionItem> ReparacionItems { get; set; }

    }
}
