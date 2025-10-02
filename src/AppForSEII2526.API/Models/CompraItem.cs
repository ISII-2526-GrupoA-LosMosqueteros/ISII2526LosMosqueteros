namespace AppForSEII2526.API.Models
{
    public class CompraItem
    {
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad minima es 1")]
        public int cantidad { get; set; }

        [StringLength(200, ErrorMessage = "La descripcion no puede tener más de 200 caracteres.")]
        public string descripcion { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.05, float.MaxValue, ErrorMessage = "El precio minimo es 0.05")]
        [Precision(10, 2)]
        public float precioTotal { get; set; }

        public int herramientaId { get; set; }
        public int compraId { get; set; }
    }
}
