using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace AppForSEII2526.API.Models
{
    public class Alquiler
    {
        public Alquiler()
        {
        }

        public Alquiler(string direccionEnvio, DateTime fechaAlquiler, DateTime fechaFin, DateTime fechaInicio, int periodo, decimal precioTotal, IList<AlquilarItem> alquilarItems, TiposMetodoPago tiposMetodoPago, ApplicationUser applicationUser)
        {
            DireccionEnvio = direccionEnvio;
            FechaAlquiler = fechaAlquiler;
            FechaFin = fechaFin;
            FechaInicio = fechaInicio;
            Periodo = periodo;
            PrecioTotal = precioTotal;
            AlquilarItems = alquilarItems;
            TiposMetodoPago = tiposMetodoPago;
            ApplicationUser = applicationUser;
        }

        public String DireccionEnvio { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaAlquiler { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFin { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        public int Id {  get; set; }


        public int Periodo { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.5, 100, ErrorMessage = "Minimo es 0.5 y maximo 100")]
        [Display(Name = "Precio Total de Alquiler")]
        [Precision(10, 2)]
        public decimal PrecioTotal { get; set; }
        public IList<AlquilarItem> AlquilarItems { get; set; }
        
        public TiposMetodoPago TiposMetodoPago { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
    public enum TiposMetodoPago
    {
        TarjetaCredito,
        PayPal,
        Efectivo
    }


}
