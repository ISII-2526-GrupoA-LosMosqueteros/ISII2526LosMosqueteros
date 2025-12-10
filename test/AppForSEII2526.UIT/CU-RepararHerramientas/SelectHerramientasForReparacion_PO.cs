using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UIT.CU_RepararHerramientas
{
    public class SelectHerramientasForReparacion_PO : PageObject
    {
        By inputNombre = By.Id("movieTitle");
        By inputFabricante = By.Id("selectGenre");
        By buttonSearchHerramientas = By.Id("searchMovies");
        public SelectHerramientasForReparacion_PO(IWebDriver driver, ITestOutputHelper output) : base(driver, output)
        {


        }

        public void SearchMovies(string title)
        {
            //wait for the webelement to be clickable
            WaitForBeingClickable(inputNombre);
            _driver.FindElement(inputTitle).SendKeys(title);
            _driver.FindElement(buttonSearchMovies).Click();
        }
    }
}
