using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Alquiler
{
    public class SelectHerramientasParaAlquilar_PO : PageObject
    {
        By insertarNombre = By.Id("insertarNombre");
        By insertarMaterial = By.Id("insertarMaterial");
        By botonBuscarHerramientas = By.Id("buscarHerramientas");
        By tableOfHerramientasBy = By.Id("TablaDeHerramientas");
        By errorShownBy = By.Id("ErrorsShown");
        By botonAlquilarHerramienta = By.Id("alquilarHerramientaButton");

        public SelectHerramientasParaAlquilar_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }
        public void BuscarHerramientas(string nombre, string material)
        {
            WaitForBeingClickable(insertarNombre);
            _driver.FindElement(insertarNombre).Clear();
            _driver.FindElement(insertarNombre).SendKeys(nombre);
            


            WaitForBeingClickable(insertarMaterial);
            _driver.FindElement(insertarMaterial).Clear();
            _driver.FindElement(insertarMaterial).SendKeys(material);


            _driver.FindElement(botonBuscarHerramientas).Click();
        }

        public bool ComprobarResultadosBusqueda(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, tableOfHerramientasBy);
        }

        public bool ComprobarMensajeError(string expectedMessage)
        {
            IWebElement actualErrorShown = _driver.FindElement(errorShownBy);
            _output.WriteLine($"actual Message shown: " + actualErrorShown.Text);
            return actualErrorShown.Text.Equals(expectedMessage);
        }

        public void AddHerramientaParaCarritoAlquiler(string herramientaNombre)
        {
            WaitForBeingClickable(By.Id("herramientaParaAlquilar_" + herramientaNombre));
            _driver.FindElement(By.Id("herramientaParaAlquilar_" + herramientaNombre)).Click();
        }

        public void RemoveHerramientaDeCarritoAlquiler(string herramientaNombre)
        {
            WaitForBeingClickable(By.Id("eliminarHerramienta_" + herramientaNombre));
            _driver.FindElement(By.Id("eliminarHerramienta_" + herramientaNombre)).Click();
        }

        public bool EstaLaHerramientaEnElCarrito(string idHerramienta)
        {
            try
            {
                By itemCarrito = By.Id($"eliminarHerramienta_{idHerramienta}");
                return _driver.FindElements(itemCarrito).Count > 0 && _driver.FindElement(itemCarrito).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void PulsarBotonAlquilar()
        {
            WaitForBeingClickable(botonAlquilarHerramienta);
            _driver.FindElement(botonAlquilarHerramienta).Click();
        }


        public bool RentingNoDisponible()
        {
            return _driver.FindElement(botonAlquilarHerramienta).Displayed == false;
        }
    }
    
}
