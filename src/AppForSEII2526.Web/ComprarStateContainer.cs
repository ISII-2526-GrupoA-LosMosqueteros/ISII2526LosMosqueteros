using AppForSEII2526.Web.API;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow.CopyAnalysis;
namespace AppForSEII2526.Web
{
    public class ComprarStateContainer
    {
        //Creamos una instancia de Compra cuando se crea una instancia de CompraStateContainer
        public CreacionCompraDTO Compra { get; private set; } = new CreacionCompraDTO() { 
            CompraItems = new List<CompraItemDTO>()
        };
        //Calculamos el Precio Total de las herramientas que hemos seleccionado para comprarlas
        public double PrecioTotal
        {
           get
           {
                return Compra.CompraItems.Sum(item => item.Precio * item.Cantidad);
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        public void AddHerramienta (HerramientasParaComprarDTO herramienta)
        {
            //Antes de agregar una herramienta, verificamos si ya se ha agregado.
            if (!Compra.CompraItems.Any(h => h.Nombre == herramienta.Nombre))
                //Lo agregamos si no está en la lista.
                Compra.CompraItems.Add(new CompraItemDTO() { 
                    Nombre = herramienta.Nombre,
                    Precio = herramienta.Precio,
                    Material = herramienta.Material,
                    Cantidad = 1,
                    Descripcion = ""
                }
            );
        }

        //Eliminar herramienta de la lista de herramientas seleccionadas
        public void EliminarHerramienta (CompraItemDTO item)
        {
            Compra.CompraItems.Remove(item);
        }

        //Eliminamos todas las herramientas de la lista
        public void EliminarTodasLasHerramientas()
        {
            Compra.CompraItems.Clear();
        }

        //Ya hemos finalizado el proceso de compra, por lo tanto, creamos una nueva Compra
        public void FinalizarCompra()
        {
            Compra = new CreacionCompraDTO()
            {
                CompraItems = new List<CompraItemDTO>()
            };
        }
    }
}
