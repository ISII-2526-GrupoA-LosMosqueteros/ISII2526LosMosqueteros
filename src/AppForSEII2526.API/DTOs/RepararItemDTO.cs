namespace AppForSEII2526.API.DTOs
{
    public class RepararItemDTO
    {
        public RepararItemDTO(int id, string nombre, decimal precio, string? descripcion, int cantidad)
        {
            Id = id;
            Nombre = nombre;
            Precio = precio;
            Descripcion = descripcion;
            Cantidad = cantidad;
        }

        int Id { get; set; }

        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string Nombre { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.5, 1000, ErrorMessage = "Minimo precio es 0.5 y el maximo 1000")]
        [Display(Name = "Precio para reparar")]
        [Precision(10, 2)]
        public decimal Precio { get; set; }

        [StringLength(200, ErrorMessage = "La descripcion no puede tener más de 200 caracteres.")]
        public String? Descripcion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad minima es 1")]
        public int Cantidad { get; set; }


    }
}
