namespace AppForSEII2526.API.DTOs
{
    public class AlquilarItemDTO
    {
        public AlquilarItemDTO(int id, string nombre, string material, decimal precio, int cantidad)
        {
            Id = id;
            Nombre = nombre;
            Material = material;
            Precio = precio;
            Cantidad = cantidad;

        }
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string Nombre { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.5, 100, ErrorMessage = "Minimo precio es 0.5 y el maximo 100")]
        [Display(Name = "Precio para alquilar")]
        [Precision(10, 2)]
        public decimal Precio { get; set; }

        [StringLength(50, ErrorMessage = "El nombre del material no puede tener más de 50 caracteres.")]
        public string Material { get; set; }

        [Display(Name = "Cantidad para Alquilar")]
        [Range(0, int.MaxValue, ErrorMessage = "La minima cantidad para alquilar es 1")]
        public int Cantidad { get; set; }
    }
}
