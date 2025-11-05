namespace AppForSEII2526.API.Models
{
    [Index(nameof(Nombre), IsUnique = true)]
    public class Fabricante
    {
        public Fabricante()
        {
        }
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres.")]
        public string Nombre { get; set; }

        public IList<Herramienta> Herramientas { get; set; }
    }
}
