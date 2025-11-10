namespace AppForSEII2526.API.Models
{
    public class Oferta
    {
        public Oferta()
        {
        }

        public Oferta(DateTime fechaFinal, DateTime fechaInicio, DateTime fechaOferta, IList<OfertaItem> ofertaItems, TiposDirigdaOferta? tiposDirigdaOferta, TiposMetodoPago tiposMetodoPago, ApplicationUser applicationUser)
        {
            FechaFinal = fechaFinal;
            FechaInicio = fechaInicio;
            FechaOferta = fechaOferta;
            OfertaItems = ofertaItems;
            TiposDirigdaOferta = tiposDirigdaOferta;
            TiposMetodoPago = tiposMetodoPago;
            ApplicationUser = applicationUser;
        }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinal { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaOferta { get; set; }

        public int Id { get; set; }

        public IList<OfertaItem> OfertaItems { get; set; }

        public TiposDirigdaOferta? TiposDirigdaOferta { get; set; }

        public TiposMetodoPago TiposMetodoPago { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
    public enum TiposDirigdaOferta
    {
        Socios,
        Clientes
    }
}