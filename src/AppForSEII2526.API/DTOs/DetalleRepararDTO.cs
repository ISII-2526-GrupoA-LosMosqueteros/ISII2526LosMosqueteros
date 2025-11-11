namespace AppForSEII2526.API.DTOs
{
    public class DetalleRepararDTO
    {
        public DetalleRepararDTO(int id, DateTime fechaEntrega, DateTime fechaRecogida,
            decimal precioTotal, string name, string surname, IList<RepararItemDTO> repararItem)
        {
            Id = id;
            FechaEntrega = fechaEntrega;
            FechaRecogida = fechaRecogida;
            PrecioTotal = precioTotal;
            Name = name;
            Surname = surname;
            RepararItem = repararItem;
        }

        public int Id { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaEntrega { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaRecogida { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.5, 100, ErrorMessage = "Minimo es 0.5 y maximo 100")]
        [Display(Name = "Precio Total de Alquiler")]
        [Precision(10, 2)]
        public decimal PrecioTotal { get; set; }

        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Apellidos")]
        public string Surname { get; set; }

        public IList<RepararItemDTO> RepararItem { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is DetalleRepararDTO dTO &&
                   Name == dTO.Name &&
                   Surname == dTO.Surname &&
                   FechaEntrega == dTO.FechaEntrega &&
                   FechaRecogida == dTO.FechaRecogida &&
                   PrecioTotal == dTO.PrecioTotal &&
                   RepararItem.SequenceEqual(dTO.RepararItem);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Surname, FechaEntrega, FechaRecogida, RepararItem);
        }

    }
}
