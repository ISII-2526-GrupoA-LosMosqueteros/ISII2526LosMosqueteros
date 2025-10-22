namespace AppForSEII2526.API.DTOs
{
    public class CompraItemDTO
    {
        public CompraItemDTO(int id, string nombre, string material, int cantidad, string descripcion, decimal precio)
        {
            Id = id;
            Nombre = nombre;
            Material = material;
            Cantidad = cantidad;
            Descripcion = descripcion;
            Precio = precio;
        }

        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(50, ErrorMessage = "El nombre del material no puede tener más de 50 caracteres.")]
        public string Material { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad minima es 1")]
        public int Cantidad { get; set; }

        [StringLength(200, ErrorMessage = "La descripcion no puede tener más de 200 caracteres.")]
        public string Descripcion { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.05, float.MaxValue, ErrorMessage = "El precio minimo es 0.05")]
        [Precision(10, 2)]
        public decimal Precio { get; set; }

    }
}
