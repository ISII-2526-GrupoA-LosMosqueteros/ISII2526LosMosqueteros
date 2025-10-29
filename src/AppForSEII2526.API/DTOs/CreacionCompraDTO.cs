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

    }
}
