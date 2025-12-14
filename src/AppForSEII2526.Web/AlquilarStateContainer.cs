using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class AlquilarStateContainer
    {

        public CreacionAlquilerDTO Alquiler { get; private set; } = new CreacionAlquilerDTO()
        {
            AlquilerItems = new List<AlquilarItemDTO>()
        };

        public decimal PrecioTotal
        {
            get
            {
                int numeroDeDias = (Alquiler.FechaFin - Alquiler.FechaInicio).Days;
                return Convert.ToDecimal(Alquiler.AlquilerItems.Sum(ri => ri.Precio * numeroDeDias * ri.Cantidad));
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        public void AgregarHerramientaParaAlquilar(HerramientaParaAlquilarDTO herramienta)
        {
            if (!Alquiler.AlquilerItems.Any(ri => ri.Id == herramienta.Id))
                Alquiler.AlquilerItems.Add(new AlquilarItemDTO()
                {
                    Id = herramienta.Id,
                    Nombre = herramienta.Nombre,
                    Material = herramienta.Material,
                    Precio = herramienta.Precio,
                }
            );

        }

        public void EliminarItemAlquilerParaAlquilar(AlquilarItemDTO item)
        {
            Alquiler.AlquilerItems.Remove(item);

        }

        public void LimpiarCarrito()
        {
            Alquiler.AlquilerItems.Clear();

        }

        public void AlquilerProcesado()
        {
            Alquiler = new CreacionAlquilerDTO()
            {
                AlquilerItems = new List<AlquilarItemDTO>()
            };
        }
    }
}