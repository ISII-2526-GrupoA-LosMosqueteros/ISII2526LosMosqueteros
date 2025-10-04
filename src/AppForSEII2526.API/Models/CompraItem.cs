namespace AppForSEII2526.API.Models
{
    public class CompraItem
    {
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad minima es 1")]
        public int Cantidad { get; set; }

        [StringLength(200, ErrorMessage = "La descripcion no puede tener más de 200 caracteres.")]
        public string Descripcion { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.05, float.MaxValue, ErrorMessage = "El precio minimo es 0.05")]
        [Precision(10, 2)]
        public decimal Precio { get; set; }

        public int IdHerramienta { get; set; }
        public int IdCompra { get; set; }

        public Herramienta Herramienta { get; set; }
        public Compra Compra { get; set; }

    }
}
