using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_ComprarHerramientas
{
    internal class CrearCompra_PO: PageObject
    {

        By inputNombre = By.Id("Name");
        By inputApellido = By.Id("Surname");
        By inputDireccion = By.Id("DeliveryAddress");
        By buttonComprar = By.Id("Submit");
        By okDialog = By.Id("Button_DialogOK");
        By buttonModificar = By.Id("ModifyCompras");
        By tablaItems = By.Id("TablaCompraItems");

        public CrearCompra_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public void RellenarFormularioCompra(string nombre, string apellido, string direccion)
        {
            WaitForBeingClickable(inputNombre);
            _driver.FindElement(inputNombre).SendKeys(nombre);
            WaitForBeingClickable(inputApellido);
            _driver.FindElement(inputApellido).SendKeys(apellido);
            WaitForBeingClickable(inputDireccion);
            _driver.FindElement(inputDireccion).SendKeys(direccion);
        }

        public void RellenarDescripcionHerramientas(string descripcion, string nombre)
        {
            By inputDescripcion = By.Id("description_" + nombre);
            WaitForBeingClickable(inputDescripcion);
            _driver.FindElement(inputDescripcion).SendKeys(descripcion);

        }

        public void pulsarComprar()
        {
            WaitForBeingClickable(buttonComprar);
            _driver.FindElement(buttonComprar).Click();
        }

        public bool ValidarError(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);
        }

        public void confirmarDialogo()
        {
            WaitForBeingClickable(okDialog);
            _driver.FindElement(okDialog).Click();
        }

        public void rellenarCantidad(int cantidad, string nombre)
        {
            By inputCantidad = By.Id("cantidad_" + nombre);
            WaitForBeingClickable(inputCantidad);
            _driver.FindElement(inputCantidad).Clear();
            _driver.FindElement(inputCantidad).SendKeys(cantidad.ToString());
        }

        public void modificarCarrito()
        {
            WaitForBeingClickable(buttonModificar);
            _driver.FindElement(buttonModificar).Click();
        }
        public bool comprobarListaHerramientasItems(List<string[]> herramientasEsperadas)
        {
            return CheckBodyTable(herramientasEsperadas, tablaItems);
        }



    }
}
