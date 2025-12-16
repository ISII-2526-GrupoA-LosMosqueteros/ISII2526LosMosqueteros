using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_RepararHerramientas
{
    public class DetailHerramientasForReparacion_PO : PageObject
    {

        public DetailHerramientasForReparacion_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }


        public bool ComprobarDetallesReparacion(string nombre, string apellido, DateTime fechaEntrega, DateTime fechaRecogida, string precioTotal)
        {
            WaitForBeingClickable(By.Id("HerramientasReparadas"));
            bool result = true;
            var nombreYApellido = nombre + " " + apellido;
            result = result && _driver.FindElement(By.Id("NombreApellido")).Text.Contains(nombreYApellido);
            result = result && _driver.FindElement(By.Id("FechaEntrega")).Text.Contains(fechaEntrega.ToString("dd/MM/yyyy"));
            result = result && _driver.FindElement(By.Id("FechaRecogida")).Text.Contains(fechaRecogida.ToString("dd/MM/yyyy"));
            result = result && _driver.FindElement(By.Id("PrecioTotal")).Text.Contains(precioTotal);
            

            return result;
        }

        public bool ComprobarTablaHerramientasReparadas(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, By.Id("HerramientasReparadas"));
        }
    }
}
