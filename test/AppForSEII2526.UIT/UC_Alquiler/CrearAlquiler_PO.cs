using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.UC_Alquiler
{
    public class CrearAlquiler_PO : PageObject
    {
        // El ID que acabamos de poner en el HTML
        private By precioTotalBy = By.Id("precioTotalCalculado");
        private By nameInput = By.Id("Name");
        private By surnameInput = By.Id("Surname");
        private By addressInput = By.Id("DireccionEnvio");
        private By fechaInicioBy = By.Id("FechaInicio");
        private By fechaFinBy = By.Id("FechaFin");
        private By botonAlquilarBy = By.Id("Submit");
        private By botonConfirmarDialogo = By.CssSelector(".modal .btn-primary");

        public CrearAlquiler_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }
        public void CamposObligatorios(string nombre, string apellidos, string direccionEnvio)
        {
            WaitForBeingVisible(nameInput);
            _driver.FindElement(nameInput).Clear();
            _driver.FindElement(nameInput).SendKeys(nombre);

            // Limpiamos Apellido
            _driver.FindElement(surnameInput).Clear();
            _driver.FindElement(surnameInput).SendKeys(apellidos);

            // Limpiamos Dirección
            _driver.FindElement(addressInput).Clear();
            _driver.FindElement(addressInput).SendKeys(direccionEnvio);
        }

       public bool ValidarErrores(string errorExpected)
        {
            return _driver.PageSource.Contains(errorExpected);
        }
        public void EstablecerFechaInicio(DateTime fecha)
        {
            WaitForBeingVisible(fechaInicioBy); 
            _driver.FindElement(fechaInicioBy).SendKeys(fecha.ToString("d"));
        }



        public void EstablecerFechaFin(DateTime fecha)
        {
            WaitForBeingVisible(fechaFinBy);
            _driver.FindElement(fechaFinBy).SendKeys(fecha.ToString("d"));
        }

        public void ClickBotonAlquilarFinal()
        {
            WaitForBeingClickable(botonAlquilarBy);
            _driver.FindElement(botonAlquilarBy).Click();
        }

        public void ClickConfirmarEnDialogo()
        {
            WaitForBeingVisible(botonConfirmarDialogo);
            _driver.FindElement(botonConfirmarDialogo).Click();
            Thread.Sleep(500);
        }

        public string ObtenerPrecioTotal()
        {
            WaitForBeingVisible(precioTotalBy);
            return _driver.FindElement(precioTotalBy).Text;
        }


       
    }
}

