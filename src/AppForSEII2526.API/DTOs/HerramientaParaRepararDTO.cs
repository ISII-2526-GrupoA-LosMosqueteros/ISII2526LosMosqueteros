namespace AppForSEII2526.API.DTOs
{
    public class HerramientaParaRepararDTO
    {

        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(50, ErrorMessage = "El nombre del material no puede tener más de 50 caracteres.")]
        public string Material { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.05, float.MaxValue, ErrorMessage = "El precio minimo es 0.05")]
        [Precision(10, 2)]
        public decimal Precio { get; set; }

        public int TiempoReparacion { get; set; }
        public string Fabricante { get; set; }

        public HerramientaParaRepararDTO(int id, string nombre, string material, decimal precio, int tiempoReparacion, string fabricante)
        {
            Id = id;
            Nombre = nombre;
            Material = material;
            Precio = precio;
            TiempoReparacion = tiempoReparacion;
            Fabricante = fabricante;
        }



    }
}
