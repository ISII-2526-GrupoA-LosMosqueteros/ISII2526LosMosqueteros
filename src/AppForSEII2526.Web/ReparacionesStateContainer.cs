using AppForSEII2526.Web.API;

namespace AppForSEII2526.Web
{
    public class ReparacionesStateContainer
    {
        //we create an instance of Reparaciones when an instance of ReparacionesStateContainer is created
        public CreacionReparacionDTO Reparacion { get; private set; } = new CreacionReparacionDTO()
        {
            RepararItem = new List<RepararItemDTO>()
        };

        public event Action? OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();


        public void AddHerramientaToReparacion(HerramientaParaRepararDTO herramienta)
        {
            //before adding a movie we checked whether it has been already added
            if (!Reparacion.RepararItem.Any(ri => ri.Id == herramienta.Id))
                //we add it if it is not in the list
                Reparacion.RepararItem.Add(new RepararItemDTO()
                {
                    Id = herramienta.Id,
                    //Fabricante = herramienta.Fabricante,
                    Nombre = herramienta.Nombre,
                    Precio = herramienta.Precio, //precio reparacion
                    TiempoReparacion = herramienta.TiempoReparacion,
                }
            );
            ComputeTotalPrice();
        }

        private void ComputeTotalPrice()
        {
            Reparacion.PrecioTotal = Reparacion.RepararItem.Sum(ri => ri.Precio);
        }

        //to delete herramientas from the list of selected herramienta
        public void RemoveReparacionItemToReparar(RepararItemDTO item)
        {
            Reparacion.RepararItem.Remove(item);
            ComputeTotalPrice();
        }

        //we eliminate all the herramientas from the list
        public void ClearReparacionesCart()
        {
            Reparacion.RepararItem.Clear();
            Reparacion.PrecioTotal = 0;
        }

        //we have already finished the process of reparacion, thus, we create a new Reparacion
        public void ReparacionProcessed()
        {
            //we have finished el proceso de reparacion so we create a new object without data
            Reparacion = new CreacionReparacionDTO()
            {
                RepararItem = new List<RepararItemDTO>()
            };
        }


        /*
        //we create an instance of Reparaciones when an instance of ReparacionesStateContainer is created
        public CreacionReparacionDTO Reparacion { get; private set; } = new CreacionReparacionDTO()
        {
            RepararItem = new List<RepararItemDTO>()
        };

        //we compute the PrecioTotal of the herramientas we have selected for reparalas
        public decimal PrecioTotal
        {
            get
            {
                int numberOfDays = (Reparacion. - Rental.RentalDateFrom).Days;
                return Convert.ToDecimal(Rental.RentalItems.Sum(ri => ri.PriceForRenting * numberOfDays));
            }
        }

        public event Action? OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

        public void AddHerramientaToReparacion(MovieForRentalDTO movie)
        {
            //before adding a movie we checked whether it has been already added
            if (!Rental.RentalItems.Any(ri => ri.MovieID == movie.Id))
                //we add it if it is not in the list
                Rental.RentalItems.Add(new RentalItemDTO()
                {
                    MovieID = movie.Id,
                    Genre = movie.Genre,
                    Title = movie.Title,
                    PriceForRenting = movie.PriceForRenting,
                }
            );
        }
        */
    }
}
