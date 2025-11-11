using AppForSEII2526.API.Models;

namespace AppForSEII2526.API.DTOs
{
    public class OfertaItemDTO
    {
        public OfertaItemDTO(string nombre, string material, string fabricante, decimal precio, decimal precioFinal, int id)
        {
            Nombre = nombre;
            Material = material;
            Fabricante = fabricante;
            Precio = precio;
            PrecioFinal = precioFinal;
            Id = id;
        }

        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(50, ErrorMessage = "El nombre del material no puede tener más de 50 caracteres.")]
        public string Material { get; set; }

        public string Fabricante { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.05, float.MaxValue, ErrorMessage = "El precio minimo es 0.05")]
        [Precision(10, 2)]
        public decimal Precio { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.05, float.MaxValue, ErrorMessage = "El precio minimo es 0.05")]
        [Precision(10, 2)]
        public decimal PrecioFinal { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is OfertaItemDTO dTO &&
                   Id == dTO.Id &&
                   Nombre == dTO.Nombre &&
                   Material == dTO.Material &&
                   Fabricante == dTO.Fabricante &&
                   Precio == dTO.Precio &&
                   PrecioFinal == dTO.PrecioFinal;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nombre, Material, Fabricante, Precio, PrecioFinal);
        }
    }
}
