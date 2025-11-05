namespace AppForSEII2526.API.Models
{

    [PrimaryKey(nameof(CompraId), nameof(HerramientaId))]
    public class CompraItem
    {
        public CompraItem()
        {
        }

        public CompraItem(int cantidad, string descripcion, Herramienta herramienta, Compra compra)
        {
            Cantidad = cantidad;
            Descripcion = descripcion;
            Precio = herramienta.Precio;
            Herramienta = herramienta;
            Compra = compra;
            HerramientaId= herramienta.Id;
            CompraId= compra.Id;
        }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad minima es 1")]
        public int Cantidad { get; set; }

        [StringLength(200, ErrorMessage = "La descripcion no puede tener más de 200 caracteres.")]
        public string Descripcion { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.05, float.MaxValue, ErrorMessage = "El precio minimo es 0.05")]
        [Precision(10, 2)]
        public decimal Precio { get; set; }

        public int HerramientaId { get; set; }
        public int CompraId { get; set; }

        public Herramienta Herramienta { get; set; }
        public Compra Compra { get; set; }

    }
}
