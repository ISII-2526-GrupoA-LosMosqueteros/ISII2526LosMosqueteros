
using AppForSEII2526.UIT.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AppForSEII2526.UIT.CU_ComprarHerramientas
{
    public class CUComprarHerramientas_UIT : UC_UIT
    {
        private SeleccionarHerramientasParaComprar_PO seleccionarHerramientasParaComprar_PO;
        private CrearCompra_PO crearCompra_PO;
        private DetallesCompra_PO detallesCompra_PO;

        public CUComprarHerramientas_UIT(ITestOutputHelper output) : base(output) {
            seleccionarHerramientasParaComprar_PO = new SeleccionarHerramientasParaComprar_PO(_driver, _output);
            crearCompra_PO = new CrearCompra_PO(_driver, _output);
            detallesCompra_PO = new DetallesCompra_PO(_driver, _output);
        }
        private const string herramienta1 = "Tuerca";
        private const string herramienta2 = "Martillo";
        private const string herramienta3 = "Tornillo";
        private const string fabricante1 = "Bosch";
        private const string fabricante3 = "Phillips";
        private const string material1 = "Acero";
        private const string material2 = "Plastico";
        private const string precio1 = "1,4";
        private const string precio3 = "1,15";
        private const string precio2 = "10";

        private void InitialStepsForComprarHerramientas() {
            Initial_step_opening_the_web_page();
            seleccionarHerramientasParaComprar_PO.WaitForBeingClickable(By.Id("CrearCompra"));
            _driver.FindElement(By.Id("CrearCompra")).Click();
        }

        //Flujo alternativo 1 del paso 2
        [Theory]
        [InlineData(herramienta3,fabricante3,material1,precio3,1.3,"")]
        [InlineData(herramienta2, fabricante1, material2, precio2, null ,"Plastico")]
        [InlineData(herramienta2, fabricante1, material2, precio2, 12, "Plastico")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_2_AF1_filteringByPrecioYMaterial(string nombreHerramienta, string fabricanteHerramienta, string materialHerramienta, string precioHerramienta, decimal filtroPrecio, string filtroMaterial)
        {
            //Arrange
            Thread.Sleep(1000);
            InitialStepsForComprarHerramientas();
            Thread.Sleep(2000);
            var expectedMovies = new List<string[]> { new string[] { nombreHerramienta,fabricanteHerramienta,materialHerramienta,precioHerramienta.ToString()}, };
            Thread.Sleep(500);

            //Act
            seleccionarHerramientasParaComprar_PO.BuscarHerramientas(filtroPrecio, filtroMaterial);
            Thread.Sleep(2000);

            //Assert

            Assert.True(seleccionarHerramientasParaComprar_PO.CheckListOfHerramientas(expectedMovies));

        }

        
        //Flujo alternativo 3 del paso 4
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]

        public void UC1_4_AF3_CompraNotavailable()
        {
            //Arrange
            InitialStepsForComprarHerramientas();
            Thread.Sleep(2000);
            //Act
            seleccionarHerramientasParaComprar_PO.AnadirHerramientaACarrito(herramienta2);
            Thread.Sleep(2000);
            seleccionarHerramientasParaComprar_PO.EliminarHerramientaDeCarrito(herramienta2);
            Thread.Sleep(2000);

            //Assert

            Assert.True(seleccionarHerramientasParaComprar_PO.CompraNotAvailable());

        }



        //PRUEBAS FUNCIONALES DEL POST
        //Flujo alternativo 4 del paso 6
        [Theory]
        [InlineData("", "Martinez", "Av. España","Tuerca Acero ", "The Nombre field is required.")]
        [InlineData("Lucia", "", "Av. España", "Tuerca Acero ", "The Apellidos field is required.")]
        [InlineData("Lucia", "Martinez", "", "Tuerca Acero ", "The DireccionEnvio field is required.")]
        [InlineData("Lucia", "Martinez", "Av. España", "", "La descripción no puede estar vacia")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC1_6_AF4_datosErroneos(string nombre, string apellidos, string direccion, string descripcion, string error)
        {
            //Arrange
            InitialStepsForComprarHerramientas();
            Thread.Sleep(2000);




            //Act

            seleccionarHerramientasParaComprar_PO.AnadirHerramientaACarrito(herramienta1);
            Thread.Sleep(2000);
            seleccionarHerramientasParaComprar_PO.PulsarComprarHerramientas();
            Thread.Sleep(2000);

            crearCompra_PO.RellenarFormularioCompra(nombre, apellidos, direccion);
            Thread.Sleep(2000);
            crearCompra_PO.RellenarDescripcionHerramientas(descripcion,herramienta1);
            Thread.Sleep(500);
            crearCompra_PO.pulsarComprar();
            Thread.Sleep(2000);
            crearCompra_PO.confirmarDialogo();
            Thread.Sleep(1000);

            //Assert

            Assert.True(crearCompra_PO.ValidarError(error));

        }

        //Flujo alternativo 2 del paso 5
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]

        public void UC1_5_AF2_ModificarCarrito()
        {
            //Arrange
            InitialStepsForComprarHerramientas();
            Thread.Sleep(2000);


            //Act

            seleccionarHerramientasParaComprar_PO.AnadirHerramientaACarrito(herramienta1);
            seleccionarHerramientasParaComprar_PO.AnadirHerramientaACarrito(herramienta2);
            Thread.Sleep(2000);
            seleccionarHerramientasParaComprar_PO.PulsarComprarHerramientas();
            Thread.Sleep(2000);
            crearCompra_PO.modificarCarrito();
            Thread.Sleep(2000);
            seleccionarHerramientasParaComprar_PO.EliminarHerramientaDeCarrito(herramienta2);
            Thread.Sleep(500);
            seleccionarHerramientasParaComprar_PO.PulsarComprarHerramientas();
            Thread.Sleep(500);


            var expectedHerramientas = new List<string[]> { new string[] { herramienta1, material1 }, };
            Thread.Sleep(500);


            //Assert
            Assert.True(crearCompra_PO.comprobarListaHerramientasItems(expectedHerramientas));

        }

        //Flujo alternativo 5 del paso 6
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]

        public void UC1_6_AF5_CantidadErronea()
        {
            //Arrange
            InitialStepsForComprarHerramientas();
            Thread.Sleep(2000);


            //Act

            seleccionarHerramientasParaComprar_PO.AnadirHerramientaACarrito(herramienta1);
            Thread.Sleep(2000);
            seleccionarHerramientasParaComprar_PO.PulsarComprarHerramientas();
            Thread.Sleep(2000);

            crearCompra_PO.RellenarFormularioCompra("Lucia", "Martinez", "Av. España");
            Thread.Sleep(2000);
            crearCompra_PO.RellenarDescripcionHerramientas("Tuerca de Acero", herramienta1);
            Thread.Sleep(500);
            crearCompra_PO.rellenarCantidad(0, herramienta1);
            Thread.Sleep(500);
            crearCompra_PO.pulsarComprar();
            Thread.Sleep(2000);
            crearCompra_PO.confirmarDialogo();
            Thread.Sleep(1000);

            //Assert

            Assert.True(crearCompra_PO.ValidarError("La cantidad minima es 1"));

        }


        //Flujo básico
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]

        public void UC1_FlujoBasico()
        {
            //Arrange

            InitialStepsForComprarHerramientas();
            Thread.Sleep(2000);


            //Act

            seleccionarHerramientasParaComprar_PO.AnadirHerramientaACarrito(herramienta1);
            Thread.Sleep(2000);

            seleccionarHerramientasParaComprar_PO.PulsarComprarHerramientas();
            Thread.Sleep(2000);
            crearCompra_PO.RellenarFormularioCompra("Lucia", "Martinez", "Calle Tuerca");
            Thread.Sleep(2000);
            crearCompra_PO.RellenarDescripcionHerramientas("Tuerca de Acero", herramienta1);
            Thread.Sleep(500);

            crearCompra_PO.rellenarCantidad(1, herramienta1);
            Thread.Sleep(500);
            crearCompra_PO.pulsarComprar();
            Thread.Sleep(2000);
            crearCompra_PO.confirmarDialogo();
            Thread.Sleep(500);

            //Assert

            Assert.True(detallesCompra_PO.CheckDetallesCompra("Lucia","Martinez","Calle Tuerca",precio1,DateTime.Today));

            var expectedDetallesHerramienta = new List<string[]> { new string[] { herramienta1,material1,"1","Tuerca de Acero", precio1 }, };
            Assert.True(detallesCompra_PO.CheckListaHerramientasCompradas(expectedDetallesHerramienta));

        }


    }
}
