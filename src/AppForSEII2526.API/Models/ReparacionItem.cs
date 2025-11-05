namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(ReparacionId), nameof(HerramientaId))]
    public class ReparacionItem
    {
        public ReparacionItem()
        {
        }

        public int ReparacionId { get; set; }
        public int HerramientaId { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.5, 1000, ErrorMessage = "Minimo precio es 0.5 y el maximo 1000")]
        [Display(Name = "Precio para reparar")]
        [Precision(10, 2)]
        public decimal Precio { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad minima es 1")]
        public int Cantidad { get; set; }

        [StringLength(200, ErrorMessage = "La descripcion no puede tener más de 200 caracteres.")]
        public String? Descripcion { get; set; }

        public Herramienta Herramienta { get; set; }

        public Reparacion Reparacion { get; set; }
    }
}
