using AppForSEII2526.UIT.Shared;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit.Abstractions;

namespace AppForSEII2526.UIT.CU_OfertaHerramienta
{
    public class SelectHerramientasParaOfertaPO : PageObject
    {
        private By inputPrecio = By.Id("inputprecio");
        private By inputFabricante = By.Id("inputfabricante");
        private By botonBuscarHerramientas = By.Id("buscarHerramientas");
        private By tableOfHerramientasBy = By.Id("Tabla de herramientas");
        private By botonOfertar = By.Id("purchaseMovieButton");

        public SelectHerramientasParaOfertaPO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {
        }

        public void BuscarHerramientas(string precio, string fabricante)
        {
            WaitForBeingClickable(inputPrecio);
            IWebElement precioElement = _driver.FindElement(inputPrecio);
            precioElement.Clear();
            precioElement.SendKeys(precio);

            IWebElement fabricanteElement = _driver.FindElement(inputFabricante);
            fabricanteElement.Clear();

            if (!string.IsNullOrEmpty(fabricante) && fabricante != "All")
            {
                fabricanteElement.SendKeys(fabricante);
            }
            _driver.FindElement(botonBuscarHerramientas).Click();
        }

        public bool CheckListOfHerramientas(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, tableOfHerramientasBy);
        }

        public void AddHerramientaToCarrito(string nombreHerramienta)
        {

            By addButton = By.XPath($"//tr[contains(., '{nombreHerramienta}')]//button");

            WaitForBeingClickable(addButton);
            _driver.FindElement(addButton).Click();

            WaitForBeingVisible(botonOfertar);
            IWebElement botonOfertarElement = _driver.FindElement(botonOfertar);
            Actions actions = new Actions(_driver);
            actions.MoveToElement(botonOfertarElement).Perform();
        }

        public void BorrarHerramientaDelCarrito(string nombreHerramienta)
        {
            By removeButton = By.Id("removeMovie_" + nombreHerramienta);
            WaitForBeingClickable(removeButton);

            IWebElement removeButtonElement = _driver.FindElement(removeButton);
            Actions actions = new Actions(_driver);
            actions.MoveToElement(removeButtonElement).Perform();
            Thread.Sleep(300);

            try
            {
                removeButtonElement.Click();
            }
            catch (ElementClickInterceptedException)
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                js.ExecuteScript("arguments[0].click();", removeButtonElement);
            }

            WaitForBeingVisible(botonOfertar);
            IWebElement botonOfertarElement = _driver.FindElement(botonOfertar);
            actions.MoveToElement(botonOfertarElement).Perform();

        }


        public bool OfertarHerramientasNotAvailable()
        {
            try
            {
                return _driver.FindElement(botonOfertar).Displayed == false;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public void ClickOfertarHerramientas()
        {
            WaitForBeingClickable(botonOfertar);
            _driver.FindElement(botonOfertar).Click();
        }
    }
}