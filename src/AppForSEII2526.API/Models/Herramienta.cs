namespace AppForSEII2526.API.Models
{
    public class Herramienta
    {
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string Nombre { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.05, float.MaxValue, ErrorMessage = "El precio minimo es 0.05")]
        [Precision(10, 2)]
        public decimal Precio { get; set; }


        public string? TiempoReparacion { get; set; }

        [StringLength(50, ErrorMessage = "El nombre del material no puede tener más de 50 caracteres.")]
        public string Material { get; set; }

        //public IList<AlquilarItem> AlquilarItem { get; set; }
        //public IList<ReparacionItem> ItemsReparacion { get; set; }
        //public IList<OfertaItem> OfertaItems { get; set; }
        public Fabricante Fabricante { get; set; }
        public IList<CompraItem> CompraItem { get; set; }
    }

   
}
