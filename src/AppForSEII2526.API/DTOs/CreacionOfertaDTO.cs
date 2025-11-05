namespace AppForSEII2526.API.DTOs
{
    public class CreacionOfertaDTO
    {
        public CreacionOfertaDTO(int porcentaje, DateTime fechaFinal, DateTime fechaInicio, TiposMetodoPago tiposMetodoPago, TiposDirigdaOferta tiposDirigdaOferta, IList<OfertaItemDTO> ofertaItem)
        {
            Porcentaje = porcentaje;
            FechaFinal = fechaFinal;
            FechaInicio = fechaInicio;
            TiposMetodoPago = tiposMetodoPago;
            TiposDirigdaOferta = tiposDirigdaOferta;
            OfertaItem = ofertaItem;
        }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100")]
        public int Porcentaje { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinal { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        public TiposMetodoPago TiposMetodoPago { get; set; }

        public TiposDirigdaOferta TiposDirigdaOferta { get; set; }

        public IList<OfertaItemDTO> OfertaItem { get; set; }

    }
}
