using AppForSEII2526.UIT.Shared;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;

namespace AppForSEII2526.UIT.CU_OfertaHerramienta
{
    public class DetallesOfertaPO : PageObject
    {
        private By tableOfHerramientasBy = By.Id("HerramientasOfertadas");

        public DetallesOfertaPO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {

        }

        public bool CheckOfertaDetail(DateTime fechaInicio, DateTime fechaFin, DateTime fechaOferta, string metodoPago, string dirigidaA, int ofertaItems)
        {
            WaitForBeingVisible(tableOfHerramientasBy);
            bool result = true;

            result = result && _driver.FindElement(By.Id("FechaInicio")).Text.Contains(fechaInicio.ToString("dd/MM/yyyy"));
            result = result && _driver.FindElement(By.Id("FechaFinal")).Text.Contains(fechaFin.ToString("dd/MM/yyyy"));
            result = result && _driver.FindElement(By.Id("FechaOferta")).Text.Contains(fechaOferta.ToString("dd/MM/yyyy"));
            result = result && _driver.FindElement(By.Id("MetodoPago")).Text.Contains(metodoPago);
            result = result && _driver.FindElement(By.Id("TipoDirigida")).Text.Contains(dirigidaA);
            result = result && _driver.FindElement(By.Id("ItemsCount")).Text.Contains(ofertaItems.ToString());

            return result;
        }

        public bool CheckListaDeHerramientasOfertadas(List<string[]> expectedHerramientas)
        {
            return CheckBodyTable(expectedHerramientas, tableOfHerramientasBy);
        }
    }
}