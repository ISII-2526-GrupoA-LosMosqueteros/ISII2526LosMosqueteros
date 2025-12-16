using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppForSEII2526.UIT.Shared;
using System.Runtime.CompilerServices;

namespace AppForSEII2526.UIT.CU_RepararHerramientas
{
    public class CURepararHerramientas_UIT : UC_UIT
    {
        private SelectHerramientasForReparacion_PO _selectHerramientas;
        private CreateHerramientasForReparacion_PO _createHerramientas;

        private const int herramientaId1 = 1;
        private const string herramientaNombre1 = "Destornillador";
        private const string herramientaMaterial1 = "Acero";
        private const string herramientaFabricante1 = "Wurt";
        private const int herramientaTiempoReparacion1I = 1;
        private const string herramientaTiempoReparacion1 = "1";
        private const string herramientaPrecio1 = "12,5 €";
        private const decimal herramientaPrecio1D = 12.5m;
        private const string descripcionHerr1 = "Mango roto";

        private const string herramientaNombre2 = "Llave Inglesa";
        private const string herramientaMaterial2 = "Acero";
        private const string herramientaFabricante2 = "Phillips";
        private const int herramientaTiempoReparacion2I = 2;
        private const string herramientaTiempoReparacion2 = "2";
        private const string herramientaPrecio2 = "10,3 €";
        private const decimal herramientaPrecio2D = 10.3m;
        private const string descripcionHerr2 = "No gira";

        private const string herramientaNombre3 = "Tornillo";
        private const string herramientaMaterial3 = "Acero";
        private const string herramientaFabricante3 = "Phillips";
        private const int herramientaTiempoReparacion3I = 1;
        private const string herramientaTiempoReparacion3 = "1";
        private const string herramientaPrecio3 = "1,15 €";
        private const decimal herramientaPrecio3D = 0.5m;
        private const string descripcionHerr3 = "No enrosca";

        private const string herramientaNombre4 = "Tuerca";
        private const string herramientaMaterial4 = "Acero";
        private const string herramientaFabricante4 = "Bosch";
        private const int herramientaTiempoReparacion4I = 1;
        private const string herramientaTiempoReparacion4 = "1";
        private const string herramientaPrecio4 = "1,4 €";
        private const decimal herramientaPrecio4D = 0.1m;
        private const string descripcionHerr4 = "Hay que soldar";

        private const string nombreU = "Lucia";
        private const string apellidoU = "Martinez";
        private const string telefonoU = "+34123456789";

        public CURepararHerramientas_UIT(ITestOutputHelper output) : base(output)
        {
            _selectHerramientas = new SelectHerramientasForReparacion_PO(_driver, _output);
            _createHerramientas = new CreateHerramientasForReparacion_PO(_driver, _output);
        }

        private void InitialStepsForRepararHerramientas()
        {
            
            //esperamos a que el menú sea visible
            Initial_step_opening_the_web_page();// Página de inicio (está en UC_UIT)
            _selectHerramientas.WaitForBeingVisible(By.Id("CrearReparacion"));


            //we wait for 0.5 seconds (500 milliseconds) till the Razor is reloaded
            Thread.Sleep(500);

            //click en el menú
            _driver.FindElement(By.Id("CrearReparacion")).Click();
        }

        //SELECT

        //Paso 2,3 - Filtrar por nombre y tiempo de reparación o solo por nombre, Flujo alternativo 0
        [Theory]
        [InlineData(herramientaNombre1, herramientaMaterial1, herramientaFabricante1, herramientaTiempoReparacion1, herramientaPrecio1, herramientaNombre1, herramientaTiempoReparacion1)] // Filtro por nombre y tiempo de reparación
        [InlineData(herramientaNombre1, herramientaMaterial1, herramientaFabricante1, herramientaTiempoReparacion1, herramientaPrecio1, herramientaNombre1, "")] // Filtro nombre
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_2_3_AF0_filtrarPorNombre(
            string expectedNombre,
            string expectedMaterial,
            string expectedFabricante,
            string expectedTiempoReparacion,
            string expectedPrecio,
            string filtroNombre,
            string filtroTiempoReparacion)
        {
            //Arrange
            InitialStepsForRepararHerramientas();
            var expectedHerramientas = new List<string[]> { new string[] { expectedNombre, expectedMaterial, expectedFabricante, expectedTiempoReparacion, expectedPrecio }  };
            
            //Act
            _selectHerramientas.FilterHerramientas(filtroNombre, filtroTiempoReparacion);
            Thread.Sleep(500); // Esperar a que devuelva

            //Assert
            Assert.True(_selectHerramientas.CheckListOfHerramientas(expectedHerramientas));
        }

        //Paso 2,3 - Filtrar por tiempoReparación. Como este devuelve 3 herramientas, se hace en un test aparte, Flujo alternativo 0 
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_2_3_AF0_filtrarPorTiempoReparacion()
        {
            //Arrange
            InitialStepsForRepararHerramientas();
            var expectedHerramientas = new List<string[]>
            {
                new string[] { herramientaNombre1, herramientaMaterial1, herramientaFabricante1, herramientaTiempoReparacion1, herramientaPrecio1 },
                new string[] { herramientaNombre3, herramientaMaterial3, herramientaFabricante3, herramientaTiempoReparacion3, herramientaPrecio3 },
                new string[] { herramientaNombre4, herramientaMaterial4, herramientaFabricante4, herramientaTiempoReparacion4, herramientaPrecio4 }
            };

            //Act
            _selectHerramientas.FilterHerramientas("", "1");
            Thread.Sleep(500); // Esperar a que devuelva

            //Assert
            Assert.True(_selectHerramientas.CheckListOfHerramientas(expectedHerramientas));
        }

        //Paso 4 - Reparar herramientas con carrito vacío, Flujo alternativo 3
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_4_AF3_RepararConCarritoVacio()
        {
            //Arrange
            InitialStepsForRepararHerramientas();
            Thread.Sleep(500);

            //Act
            _selectHerramientas.AddHerramientaAlCarrito(herramientaNombre1);
            Thread.Sleep(500);
            _selectHerramientas.RemoveHerramientaDelCarrito(herramientaNombre1);
            Thread.Sleep(500);


            //Assert
            Assert.True(_selectHerramientas.RepararHerramientasNoDisponible());
        }


        //POST

        //Paso 5,6 - Fecha de entrega anterior, flujo alternativo 1
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_5_6_AF1_FechaEntregaAnteriorAHoy()
        {
            // Arrange
            InitialStepsForRepararHerramientas();
            
            Thread.Sleep(500);

            _selectHerramientas.AddHerramientaAlCarrito(herramientaNombre1);
            _selectHerramientas.ClickRepararHerramientas();

            DateTime fechaEntregaAnterior = DateTime.Today.AddDays(-1);

            // Act
            _createHerramientas.InputUsuario(nombreU, apellidoU, telefonoU, fechaEntregaAnterior);
            Thread.Sleep(1000);
            _createHerramientas.RellenarDescripcionHerramientas(descripcionHerr1, herramientaNombre1);
            Thread.Sleep(1000);
            _createHerramientas.ClickReparaTusHerramientas();
            Thread.Sleep(500);
            _createHerramientas.confirmarDialogo();
            Thread.Sleep(500);

            // Assert
            Assert.True(_createHerramientas.ValidarError("La fecha de entrega no puede ser anterior a hoy."));
        }

        //Paso 5,6 - Modificar el carrito , flujo alternativo 2
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_5_6_AF2_ModificarCarrito()
        {
            // Arrange
            InitialStepsForRepararHerramientas();

            Thread.Sleep(500);

            _selectHerramientas.AddHerramientaAlCarrito(herramientaNombre1);
            _selectHerramientas.AddHerramientaAlCarrito(herramientaNombre2);
            _selectHerramientas.ClickRepararHerramientas();
            Thread.Sleep(500);

            // Act
            _createHerramientas.ClickModificarHerramientas();
            Thread.Sleep(500);
            _selectHerramientas.RemoveHerramientaDelCarrito(herramientaNombre2);
            Thread.Sleep(500);
            _selectHerramientas.ClickRepararHerramientas();
            Thread.Sleep(500);

            var expectedHerramientas = new List<string[]>
            {
                new string[] { herramientaNombre1, herramientaTiempoReparacion1 }
            };
            Thread.Sleep(500);

            // Assert
            Assert.True(_createHerramientas.comprobarListaHerramientasItems(expectedHerramientas));
        }

        //Paso 5,6 - Faltan campos obligatorios, Flujo alternativo 4
        [Theory]
        [InlineData("", apellidoU, telefonoU, "The Nombre field is required.")]
        [InlineData(nombreU, "", telefonoU, "The Apellidos field is required.")]
        [InlineData(nombreU, apellidoU, "", "The Telefono field is required.")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_5_6_AF4_FaltanCamposObligatorios(
            string expectedName,
            string expectedSurname,
            string expectedPhone,
            string expectedError)
        {
            // Arrange
            InitialStepsForRepararHerramientas();
            Thread.Sleep(500);
            _selectHerramientas.AddHerramientaAlCarrito(herramientaNombre1);
            _selectHerramientas.ClickRepararHerramientas();
            Thread.Sleep(500);
            // Act
            _createHerramientas.InputUsuario(expectedName, expectedSurname, expectedPhone, DateTime.Today); //la fecha de entrega se inicializa a hoy automáticamente, no hace falta ponerla y si es errónea se ve en otro test
            Thread.Sleep(1000);
            _createHerramientas.RellenarDescripcionHerramientas(descripcionHerr1, herramientaNombre1);
            Thread.Sleep(1000);
            _createHerramientas.ClickReparaTusHerramientas();
            Thread.Sleep(500);
            _createHerramientas.confirmarDialogo();
            Thread.Sleep(500);
            // Assert
            Assert.True(_createHerramientas.ValidarError(expectedError));
        }

        //Paso 5,6 - La cantidad es 0, Flujo alternativo 6
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_5_6_AF6_CantidadErronea()
        {
            // Arrange
            InitialStepsForRepararHerramientas();
            Thread.Sleep(500);
            _selectHerramientas.AddHerramientaAlCarrito(herramientaNombre1);
            _selectHerramientas.AddHerramientaAlCarrito(herramientaNombre2);
            _selectHerramientas.ClickRepararHerramientas();
            Thread.Sleep(500);
            // Act
            _createHerramientas.InputUsuario(nombreU, apellidoU, telefonoU, DateTime.Today);
            Thread.Sleep(1000);
            _createHerramientas.RellenarDescripcionHerramientas(descripcionHerr1, herramientaNombre1);
            Thread.Sleep(1000);
            _createHerramientas.asignarCantidad(0, herramientaNombre1); // Cantidad 0
            Thread.Sleep(500);
            _createHerramientas.ClickReparaTusHerramientas();
            Thread.Sleep(500);
            _createHerramientas.confirmarDialogo();
            Thread.Sleep(500);
            // Assert
            Assert.True(_createHerramientas.ValidarError("La cantidad minima es 1"));
        }

        //DETAILS
        //Flujo Básico - Reparar herramientas correctamente
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC2_FlujoBasico()
        {
            // Arrange
            InitialStepsForRepararHerramientas();
            Thread.Sleep(500);
            
            // Act
            _selectHerramientas.AddHerramientaAlCarrito(herramientaNombre1);
            _selectHerramientas.ClickRepararHerramientas();

            _createHerramientas.InputUsuario(nombreU, apellidoU, telefonoU, DateTime.Today);
            Thread.Sleep(1000);
            _createHerramientas.RellenarDescripcionHerramientas(descripcionHerr1, herramientaNombre1);
            Thread.Sleep(1000);
            _createHerramientas.asignarCantidad(2, herramientaNombre1);
            _createHerramientas.ClickReparaTusHerramientas();
            Thread.Sleep(500);
            _createHerramientas.confirmarDialogo();
            Thread.Sleep(500);



        }

        /*
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
