
namespace AppForSEII2526.API.DTOs
{
    public class DetallesCompraDTO
    {
        public DetallesCompraDTO(string name, string surname, string direccionenvio, decimal precioTotal, DateTime fechaCompra, IList<CompraItemDTO> compraItem)
        {
            
            Name = name;
            Surname = surname;
            DireccionEnvio = direccionenvio;
            PrecioTotal = precioTotal;
            FechaCompra = fechaCompra;
            CompraItem = compraItem;
        }

       
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Apellidos")]
        public string Surname { get; set; }

        public string DireccionEnvio { get; set; }
        public decimal PrecioTotal { get; set; }
        public DateTime FechaCompra { get; set; }
        public IList<CompraItemDTO> CompraItem { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is DetallesCompraDTO dTO &&
                   Name == dTO.Name &&
                   Surname == dTO.Surname &&
                   DireccionEnvio == dTO.DireccionEnvio &&
                   PrecioTotal == dTO.PrecioTotal &&
                   CompraItem.SequenceEqual(dTO.CompraItem);
        }
    }
}
