using AppForSEII2526.UIT.Shared;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace AppForSEII2526.UIT.CU_OfertaHerramienta
{
    public class CrearOfertaPO : PageObject
    {
        private By botonFechaInicio = By.Id("FechaInicio");
        private By botonFechaFinal = By.Id("FechaFinal");
        private By botonOfertarHerramientas = By.Id("Submit");
        private By botonDialogOkButton = By.Id("Button_DialogOK");
        private By modificarHerramientas = By.Id("ModificarHerramientas");
        private By tableOfOfertasItemsBy = By.Id("TablaOfertaHerramientas");
        private By errorsShown = By.Id("ErrorsShown");

        public CrearOfertaPO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {

        }

        public void RellenarFormularioOferta(DateTime fechaInicio, DateTime fechaFin)
        {
            InputDateInDatePicker(botonFechaInicio, fechaInicio);
            InputDateInDatePicker(botonFechaFinal, fechaFin);
        }

        public void RellenarPorcentajeOferta(int hID, int porcentaje)
        {
            By inputPorcentaje = By.Id($"porcentaje_{hID}");

            WaitForBeingClickable(inputPorcentaje);
            _driver.FindElement(inputPorcentaje).Clear();
            _driver.FindElement(inputPorcentaje).SendKeys(porcentaje.ToString());
        }

        public bool CheckValidationError(string expectedError)
        {
            return _driver.PageSource.Contains(expectedError);
        }

        public void ClickSubmitButton()
        {
            WaitForBeingClickable(botonOfertarHerramientas);
            _driver.FindElement(botonOfertarHerramientas).Click();
        }

        public void ConfirmDialog()
        {
            WaitForBeingClickable(botonDialogOkButton);
            _driver.FindElement(botonDialogOkButton).Click();
        }

        public void PressModifyHerramientasButton()
        {
            WaitForBeingClickable(modificarHerramientas);
            _driver.FindElement(modificarHerramientas).Click();
        }

        public bool CheckListOfHerramientasParaOfertar(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, tableOfOfertasItemsBy);
        }

        public void RellenarPorcentaje(int hID, int? porcentaje)
        {
            By inputPorcentaje = By.Id($"porcentaje_{hID}");
            WaitForBeingClickable(inputPorcentaje);
            var inputElement = _driver.FindElement(inputPorcentaje);
            inputElement.Clear();
            if (porcentaje.HasValue)
                inputElement.SendKeys(porcentaje.Value.ToString());
        }

        public void RellenarFecha(By campo, int? dias)
        {
            WaitForBeingVisible(campo);
            if (dias.HasValue && dias > 0)
                InputDateInDatePicker(campo, DateTime.Today.AddDays(dias.Value));
            else
                _driver.FindElement(campo).Clear();
        }

        
    }
}