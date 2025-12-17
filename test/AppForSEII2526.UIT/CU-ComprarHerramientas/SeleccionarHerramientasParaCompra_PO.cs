using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_ComprarHerramientas
{
    public class SeleccionarHerramientasParaComprar_PO : PageObject
    {
        By inputPrecio = By.Id("inputPrecio");
        By inputMaterial = By.Id("inputMaterial");
        By buttonBuscarHerramientas = By.Id("buscarHerramientas");
        By tablaofHerramientas = By.Id("TablaHerramientas");
        By buttonComprarHerramientas=By.Id("purchaseHerraminetaButton");
        public SeleccionarHerramientasParaComprar_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }
        public void BuscarHerramientas(decimal precio, string material)
        {
            //wait for the webelement to be clickable
            _driver.FindElement(inputMaterial).Clear();
            WaitForBeingClickable(inputMaterial);
            _driver.FindElement(inputMaterial).SendKeys(material);
            _driver.FindElement(inputPrecio).Clear();
            WaitForBeingClickable(inputPrecio);
            _driver.FindElement(inputPrecio).SendKeys(precio.ToString());
            _driver.FindElement(buttonBuscarHerramientas).Click();

        }

        public bool CheckListOfHerramientas(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, tablaofHerramientas);
        }

        public void AnadirHerramientaACarrito(string nombreHerramienta)
        {
            By botonAnadir = By.Id("herramientaparacomprar_" + nombreHerramienta);
            WaitForBeingClickable(botonAnadir);
            _driver.FindElement(botonAnadir).Click();
        }

        public void EliminarHerramientaDeCarrito(string nombreHerramienta)
        {
            By botonEliminar = By.Id("eliminarherramientas_" + nombreHerramienta);
            WaitForBeingClickable(botonEliminar);
            _driver.FindElement(botonEliminar).Click();
        }

        public bool CompraNotAvailable()
        {
            //the button is not Displayed=hidden
            try
            {
                return _driver.FindElement(buttonComprarHerramientas).Displayed == false;
            }
            catch (Exception ex)
            {
                return true;

            }
        }

        public void PulsarComprarHerramientas()
        {
            WaitForBeingClickable(buttonComprarHerramientas);
            _driver.FindElement(buttonComprarHerramientas).Click();
        }

        
    }
}
