namespace AppForSEII2526.API.Models
{
    [PrimaryKey(nameof(AlquilerId), nameof(HerramientaId))]
    public class AlquilarItem
    {
        public AlquilarItem()
        {
        }

        }
        [Display(Name = "Cantidad para Alquilar")]
        [Range(0, int.MaxValue, ErrorMessage = "La minima cantidad para alquilar es 1")]
        public int Cantidad { get; set; }

        public int AlquilerId { get; set; }
        public int HerramientaId { get; set; } 

        [DataType(System.ComponentModel.DataAnnotations.DataType.Currency)]
        [Range(0.5, 100, ErrorMessage = "Minimo precio es 0.5 y el maximo 100")]
        [Display(Name = "Precio para alquilar")]
        [Precision(10, 2)]
        public decimal Precio { get; set; }

        public Herramienta Herramienta { get; set; }

        public Alquiler Alquiler { get; set; }

        


    }
}
