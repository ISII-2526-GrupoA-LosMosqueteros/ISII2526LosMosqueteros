namespace AppForSEII2526.API.DTOs
{
    public class DetallesCompraDTO
    {
        public DetallesCompraDTO(int id, string name, string surname, string? email, decimal precioTotal, DateTime fechaCompra, IList<CompraItemDTO> compraItem)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            PrecioTotal = precioTotal;
            FechaCompra = fechaCompra;
            CompraItem = compraItem;
        }

        public int Id { get; set; }
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Apellidos")]
        public string Surname { get; set; }

        [Display(Name = "Correo Electronico")]
        public string? Email { get; set; }
        public decimal PrecioTotal { get; set; }
        public DateTime FechaCompra { get; set; }
        public IList<CompraItemDTO> CompraItem { get; set; }

    }
}
