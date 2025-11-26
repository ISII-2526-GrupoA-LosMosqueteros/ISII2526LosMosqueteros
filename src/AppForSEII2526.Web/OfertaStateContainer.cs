using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class OfertaStateContainer
    {
        public CreacionOfertaDTO Oferta { get; private set; } = new CreacionOfertaDTO()
        {
            OfertaItem = new List<OfertaItemDTO>()
        };

        public decimal PrecioTotal
        {
            get
            {
                return (decimal)Oferta.OfertaItem.Sum(item => item.PrecioFinal);
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        public void AñadirHerramientaParaOferta(HerramientasParaOfertaDTO herramienta)
        {
            if (!Oferta.OfertaItem.Any(oi => oi.Id == herramienta.Id))
            {
                decimal precioFinal = (decimal)herramienta.Precio * (1 - (Oferta.Porcentaje / 100m));

                Oferta.OfertaItem.Add(new OfertaItemDTO()
                {
                    Id = herramienta.Id,
                    Nombre = herramienta.Nombre,
                    Material = herramienta.Material,
                    Fabricante = herramienta.Fabricante,
                    Precio = herramienta.Precio,
                });
            }
        }

        public void RemoveOfertaItemToRent(OfertaItemDTO item)
        {
            Oferta.OfertaItem.Remove(item);
        }

        public void ClearOfertaCart()
        {
            Oferta.OfertaItem.Clear();
        }

        public void OfertaProcessed()
        {
            Oferta = new CreacionOfertaDTO()
            {
                OfertaItem = new List<OfertaItemDTO>()
            };
        }
    }
}