using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppForSEII2526.UIT.Shared;

namespace AppForSEII2526.UIT.CU_RepararHerramientas
{
    internal class CURepararHerramientas_UIT : UC_UIT
    {
        //Webdriver: A reference to the browser. Referencia al navegador
        IWebDriver _driver;
        //A reference to the URI of the web page to test. Referencia a la página web a probar
        string _URI;
        //this may be used whenever some result should be printed in E
        private readonly ITestOutputHelper _output;

        public CURepararHerramientas_UIT(ITestOutputHelper output) : base(output)
        {

            //it is needed to run the browser and know the URI of your app
            UC_UIT.(out _driver, out _URI);
            //it is initialized using the logger provided by xUnit
            this._output = output;
        }

        /*
        //The code for your test Methods will go here
        void IDisposable.Dispose()
        {
            //To close and release all the resources allocated by the web driver
            _driver.Close();
            _driver.Dispose();
            GC.SuppressFinalize(this);
        }
        */
    }
}
