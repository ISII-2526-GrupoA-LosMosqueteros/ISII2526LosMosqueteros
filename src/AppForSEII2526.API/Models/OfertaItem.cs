namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(OfertaId), nameof(HerramientaId))]
    public class OfertaItem
    {
        public OfertaItem()
        {
        }

        public OfertaItem(int porcentaje, decimal precioFinal, Herramienta herramienta, Oferta oferta)
        {
            Porcentaje = porcentaje;
            PrecioFinal = precioFinal;
            Herramienta = herramienta;
            Oferta = oferta;
            HerramientaId = herramienta.Id;
            OfertaId = oferta.Id;
        }

        public int HerramientaId { get; set; }
        public int OfertaId { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0, 100, ErrorMessage = "El porcentaje debe estar entre 0 y 100")]
        public int Porcentaje { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.05, float.MaxValue, ErrorMessage = "El precio minimo es 0.05")]
        [Precision(10, 2)]
        public decimal PrecioFinal { get; set; }

        public Herramienta Herramienta { get; set; }
        public Oferta Oferta { get; set; }

    }
}