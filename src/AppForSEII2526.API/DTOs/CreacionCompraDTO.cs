
namespace AppForSEII2526.API.DTOs
{
    public class CreacionCompraDTO
    {
        public CreacionCompraDTO(string name, string surname, string direccionEnvio, TiposMetodoPago tipoMetodoPago, string? phone, string? email, IList<CompraItemDTO> compraItems)
        {
            Name = name;
            Surname = surname;
            DireccionEnvio = direccionEnvio;
            TipoMetodoPago = tipoMetodoPago;
            Phone = phone;
            Email = email;
            CompraItems = compraItems;
         
        }

        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Apellidos")]
        public string Surname { get; set; }
        public string DireccionEnvio { get; set; }
        public TiposMetodoPago TipoMetodoPago { get; set; }

        [Display(Name = "Telefono")]
        public string? Phone { get; set; }
        [Display(Name = "Correo Electronico")]
        public string? Email { get; set; }
        public IList<CompraItemDTO> CompraItems { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is CreacionCompraDTO dTO &&
                   Name == dTO.Name &&
                   Surname == dTO.Surname &&
                   DireccionEnvio == dTO.DireccionEnvio &&
                   TipoMetodoPago == dTO.TipoMetodoPago &&
                   Phone == dTO.Phone &&
                   Email == dTO.Email &&
                   EqualityComparer<IList<CompraItemDTO>>.Default.Equals(CompraItems, dTO.CompraItems);
        }
    }
}
