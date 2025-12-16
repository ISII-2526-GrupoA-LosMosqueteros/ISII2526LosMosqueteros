using AppForSEII2526.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI; //para los desplegables

namespace AppForSEII2526.UIT.CU_RepararHerramientas
{
    public class CreateHerramientasForReparacion_PO : PageObject
    {
        private By _inputNombreUBy = By.Id("Name");
        private By _inputApellidoUBy = By.Id("Surname");
        private By _inputTelefonoUBy = By.Id("Phone");
        private By _inputFechaBy = By.Id("FechaEntrega");
        //private By _inputMetodoPagoBy = By.Id("MetodoPago");
        private By _buttonProcederRepararBy = By.Id("ProcederRepararButton");
        private By _okDialogBy = By.Id("Button_DialogOK"); //dialogo de confirmación está en Web/Components/Shared/Dialog.razor
        private By _buttonModificarHerramientasBy = By.Id("ModificarHerramientasButton");
        private By _tablaItems = By.Id("TablaHerramientasReparacionItems");

        private IWebElement usuarioNombre() => _driver.FindElement(_inputNombreUBy);
        private IWebElement usuarioApellido() => _driver.FindElement(_inputApellidoUBy);
        private IWebElement usuarioTelefono() => _driver.FindElement(_inputTelefonoUBy);
        //private IWebElement _metodoPago() => _driver.FindElement(_inputMetodoPagoBy);
        private IWebElement repararButton() => _driver.FindElement(_buttonProcederRepararBy);
        private IWebElement dialogoOk() => _driver.FindElement(_okDialogBy);
        private IWebElement modificarHerramientas() => _driver.FindElement(_buttonModificarHerramientasBy);



        public CreateHerramientasForReparacion_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public void InputUsuario(string nombreU, string apellidoU, string telefonoU, DateTime fechaEntrega)
        {
            //we wait for the webelement to be clickable
            WaitForBeingClickable(_inputNombreUBy);
            usuarioNombre().SendKeys(nombreU);

            WaitForBeingClickable(_inputApellidoUBy);
            usuarioApellido().SendKeys(apellidoU);

            //El telefono es obligatorio desde el examen del Sprint 2 con lo del prefijo de +34
            WaitForBeingClickable(_inputTelefonoUBy);
            usuarioTelefono().SendKeys(telefonoU);
            
            InputDateInDatePicker(_inputFechaBy, fechaEntrega);

            //SelectElement selectElement = new SelectElement(_MetodoPago());
            //selectElement.SelectByText(genre);
        }

        public void RellenarDescripcionHerramientas(string descripcion, string nombreHerramienta)
        {
            By _inputDescripcionBy = By.Id($"description_{nombreHerramienta}");
            WaitForBeingClickable(_inputDescripcionBy);
            _driver.FindElement(_inputDescripcionBy).SendKeys(descripcion);
        }

        public void ClickReparaTusHerramientas()
        {
            WaitForBeingClickable(_buttonProcederRepararBy);
            repararButton().Click();
        }


        public void confirmarDialogo()
        {
            WaitForBeingClickable(_okDialogBy);
            dialogoOk().Click();
        }

        public bool ValidarError(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);
        }

        public void ClickModificarHerramientas()
        {
            WaitForBeingClickable(_buttonModificarHerramientasBy);
            Thread.Sleep(500);
            modificarHerramientas().Click();
        }

        public bool comprobarListaHerramientasItems(List<string[]> herramientasEsperadas)
        {
            //Thread.Sleep(500);
            return CheckBodyTable(herramientasEsperadas, _tablaItems);
        }

        public void asignarCantidad(int cantidad, string nombre)
        {
            By inputCantidad = By.Id("cantidad_" + nombre);
            WaitForBeingClickable(inputCantidad);
            Thread.Sleep(500);
            _driver.FindElement(inputCantidad).Clear();
            _driver.FindElement(inputCantidad).SendKeys(cantidad.ToString());
        }



    }
}
