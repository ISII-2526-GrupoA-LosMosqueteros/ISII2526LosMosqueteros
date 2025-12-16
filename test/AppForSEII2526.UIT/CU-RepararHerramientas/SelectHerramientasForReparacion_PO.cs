using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_RepararHerramientas
{
    public class SelectHerramientasForReparacion_PO : PageObject
    {
        private By _inputNombreBy = By.Id("inputNombre");
        private By _inputTiempoReparacionBy = By.Id("inputTiempoReparacion");

        private By _showRentingCartBy = By.Id("showRentingCart");
        private By _buttonSearchHerramientasBy = By.Id("searchHerramientas");
        private By _repararButtonBy = By.Id("repararHerramientaButton");

        private By _tableOfHerramientasBy = By.Id("TableOfHerramientas");
        //private By _modalBy = By.Id("DialogOKSaveDelete");


        private IWebElement _herramientaNombre() => _driver.FindElement(_inputNombreBy);
        private IWebElement _herramientaTiempoReparacion() => _driver.FindElement(_inputTiempoReparacionBy);
        private IWebElement _buscarHerramientasButton() => _driver.FindElement(_buttonSearchHerramientasBy);
        private IWebElement _repararButton() => _driver.FindElement(_repararButtonBy);
        private IWebElement _showRentingCartButton() => _driver.FindElement(_showRentingCartBy);


        public SelectHerramientasForReparacion_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {


        }

        public void FilterHerramientas(string filtroNombre, string filtroTiempoReparacion)
        {
            //wait for the webelement to be clickable
            WaitForBeingClickable(_inputNombreBy);
            _herramientaNombre().SendKeys(filtroNombre);
            

            WaitForBeingClickable(_inputTiempoReparacionBy);
            _herramientaTiempoReparacion().SendKeys(filtroTiempoReparacion);

            _buscarHerramientasButton().Click();
            //we wait for 2 seconds (2000 milliseconds) till the table is reloaded as we have to wait for the API service to be called
            System.Threading.Thread.Sleep(2000);
        }


        public void SelectHerramientas(List<string> nombreHerramientas)
        {
            //we wait for till the herramientas are available to be selected 
            foreach (var nombreHerramienta in nombreHerramientas)
            {
                WaitForBeingVisible(By.Id($"herramientaToReparar_{nombreHerramienta}"));
                _driver.FindElement(By.Id($"herramientaToReparar_{nombreHerramienta}")).Click();
            }
        }

        public void ClickRepararHerramientas()
        {
            WaitForBeingClickable(_repararButtonBy);
            _repararButton().Click();
        }

        public bool CheckShoppingCart(string precio)
        {
            return _showRentingCartButton().Text.Contains(precio);
        }

        public bool CheckMessageErrorNotAvaibleHerramientas(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);

        }
        
        public bool CheckListOfHerramientas(List<string[]> expectedHerramientas)
        {

            return CheckBodyTable(expectedHerramientas, _tableOfHerramientasBy);
        }


        //El botón de reparar no está visible
        public bool RepararHerramientasNoDisponible()
        {
            try
            {
                return _repararButton().Displayed == false;
            }
            catch (Exception e)
            {
                return true;
            }
        }
        public void AddHerramientaAlCarrito(string nombre)
        {
            By addBoton = By.Id("herramientaToReparar_" + nombre);
            WaitForBeingClickable(addBoton);
            Thread.Sleep(500); //wait for half a second to avoid issues with clicking too fast
            _driver.FindElement(addBoton).Click();
        }


        public void RemoveHerramientaDelCarrito(string nombre)
        {
            By removeBoton = By.Id("removeHerramienta_" + nombre);
            WaitForBeingClickable(removeBoton);
            Thread.Sleep(500); //wait for half a second to avoid issues with clicking too fast
            _driver.FindElement(removeBoton).Click();
        }

        /*


        public bool CheckMessageError(string expectedError)
        {
            return CheckModalBodyText(expectedError, _modalBy);
        }
        */

    }
}
