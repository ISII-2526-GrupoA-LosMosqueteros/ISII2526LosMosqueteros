using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using AppForSEII2526.UIT.Shared;

namespace AppForSEII2526.UIT.UC_Alquiler
{
    internal class DetalleAlquiler_PO : PageObject
    {
        public DetalleAlquiler_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public bool CheckDetallesAlquiler(string nombre, string apellidos, string direccion, string precioTotal, DateTime fechaInicio, DateTime fechaFin)
        {
            WaitForBeingVisible(By.Id("HerramientasAlquiladas"));

            bool result = true;
            var nombreYApellidos = nombre + " " + apellidos;

            result = result && _driver.FindElement(By.Id("NombreUsuario")).Text.Contains(nombreYApellidos);
            result = result && _driver.FindElement(By.Id("DireccionEnvio")).Text.Contains(direccion);
            result = result && _driver.FindElement(By.Id("PrecioTotal")).Text.Contains(precioTotal);

            string periodoEsperado = fechaInicio.ToString("dd/MM/yyyy") + " - " + fechaFin.ToString("dd/MM/yyyy");
            result = result && _driver.FindElement(By.Id("PeriodoAlquiler")).Text.Contains(periodoEsperado);

            return result;
        }

        public bool CheckListaHerramientasAlquiladas(List<string[]> expectedHerramientas)
        {
            // Usamos tu método base CheckBodyTable apuntando a la tabla de alquileres
            return CheckBodyTable(expectedHerramientas, By.Id("HerramientasAlquiladas"));
        }
    }
}