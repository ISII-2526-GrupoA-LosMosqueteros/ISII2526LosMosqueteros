
namespace AppForSEII2526.API.DTOs
{
    public class CreacionReparacionDTO
    {
        public CreacionReparacionDTO(DateTime fechaEntrega,
            string name, string surname, IList<RepararItemDTO> repararItem, TiposMetodoPago tiposMetodoPago, string phone)
        {
            FechaEntrega = fechaEntrega;
            Name = name;
            Surname = surname;
            RepararItem = repararItem;
            TiposMetodoPago = tiposMetodoPago;
            Phone = phone;
        }

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

        [Display(Name = "Telefono")]
        public string Phone { get; set; }

        public TiposMetodoPago TiposMetodoPago { get; set; }

        public IList<RepararItemDTO> RepararItem { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CreacionReparacionDTO dTO &&
                   FechaEntrega == dTO.FechaEntrega &&
                   FechaRecogida == dTO.FechaRecogida &&
                   PrecioTotal == dTO.PrecioTotal &&
                   Name == dTO.Name &&
                   Surname == dTO.Surname &&
                   Phone == dTO.Phone &&
                   TiposMetodoPago == dTO.TiposMetodoPago &&
                   RepararItem.SequenceEqual(dTO.RepararItem);
        }
    }
}
