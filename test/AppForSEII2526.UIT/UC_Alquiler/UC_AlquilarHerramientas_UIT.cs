using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppForSEII2526.UIT.Shared;

namespace AppForSEII2526.UIT.UC_Alquiler
{
    public class UC_AlquilarHerramientas_UIT : UC_UIT
    {
        private SelectHerramientasParaAlquilar_PO selectHerramientasParaAlquilar_PO;
        private CrearAlquiler_PO crearAlquiler_PO;
        private const string idHerramienta1 = "1";
        private const string nombreHerramienta1 = "Destornillador";
        private const string materialHerramienta1 = "Acero";
        private const string precioHerramienta1 = "12,5";
        private const string fabricanteHerramienta1 = "Wurt";
        private const string idHerramienta2 = "5";
        private const string nombreHerramienta2 = "Martillo";
        private const string materialHerramienta2 = "Plastico";
        private const string precioHerramienta2 = "10";
        private const string fabricanteHerramienta2 = "Bosch";


        public UC_AlquilarHerramientas_UIT(ITestOutputHelper output) : base(output)
        {
            selectHerramientasParaAlquilar_PO = new SelectHerramientasParaAlquilar_PO(_driver, _output);
            crearAlquiler_PO = new CrearAlquiler_PO(_driver, _output);
        }

        private void InitialStepsParaAlquilarHerramientas()
        {
            Initial_step_opening_the_web_page();
            By id = By.Id("CrearAlquiler");
            selectHerramientasParaAlquilar_PO.WaitForBeingClickable(id);
            Thread.Sleep(500);

            _driver.FindElement(id).Click();
        }

        [Theory]
        [InlineData(nombreHerramienta1, materialHerramienta1, fabricanteHerramienta1, precioHerramienta1, "Destornillador", "")]
        [InlineData(nombreHerramienta2, materialHerramienta2, fabricanteHerramienta2, precioHerramienta2, "", "Plastico")]
        [Trait("LevelTesting", "Funcional Testing")]

        public void CU4_2_3_4_AF1_filteringPorNombreMaterial(string nombreHerramienta, string materialHerramienta, string fabricanteHerramienta, string precioHerramienta, string filtroNombre, string filtroMaterial)
        {
            //ARRANGE
            InitialStepsParaAlquilarHerramientas();
            var expectedHerramientas = new List<string[]>
            {
                new string[] { nombreHerramienta, materialHerramienta, fabricanteHerramienta, precioHerramienta }
            };

            //ACT
            selectHerramientasParaAlquilar_PO.BuscarHerramientas(filtroNombre, filtroMaterial);

            Thread.Sleep(500);


            //ASSERT
            Assert.True(selectHerramientasParaAlquilar_PO.ComprobarResultadosBusqueda(expectedHerramientas));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU4_AF2_ModificarCarrito()
        {
            // ARRANGE
            InitialStepsParaAlquilarHerramientas();

            // ACT 
            selectHerramientasParaAlquilar_PO.BuscarHerramientas(nombreHerramienta1, "");
            Thread.Sleep(500);
            selectHerramientasParaAlquilar_PO.AddHerramientaParaCarritoAlquiler(idHerramienta1);

            selectHerramientasParaAlquilar_PO.BuscarHerramientas(nombreHerramienta2, "");
            Thread.Sleep(500);
            selectHerramientasParaAlquilar_PO.AddHerramientaParaCarritoAlquiler(idHerramienta2);

            // ACT 
            selectHerramientasParaAlquilar_PO.RemoveHerramientaDeCarritoAlquiler(idHerramienta1);
            Thread.Sleep(500);

            // ASSERT
            Assert.False(selectHerramientasParaAlquilar_PO.EstaLaHerramientaEnElCarrito(idHerramienta1));
            Assert.True(selectHerramientasParaAlquilar_PO.EstaLaHerramientaEnElCarrito(idHerramienta2));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU4_BF_P3_CrearAlquiler()
        {
            // ARRANGE
            InitialStepsParaAlquilarHerramientas();
            selectHerramientasParaAlquilar_PO.BuscarHerramientas(nombreHerramienta1, "");
            Thread.Sleep(500);
            selectHerramientasParaAlquilar_PO.AddHerramientaParaCarritoAlquiler(idHerramienta1);
            selectHerramientasParaAlquilar_PO.BuscarHerramientas(nombreHerramienta2, "");
            Thread.Sleep(500);
            selectHerramientasParaAlquilar_PO.AddHerramientaParaCarritoAlquiler(idHerramienta2);

            // ACT 
            selectHerramientasParaAlquilar_PO.PulsarBotonAlquilar();
            string precioEsperado = "22,5";

            // ASSERT
            string precioReal = crearAlquiler_PO.ObtenerPrecioTotal();
            Assert.Equal(precioEsperado, precioReal);
        }


        [Theory]
        [InlineData("Carlos", "Gomez", "", "¡Error! La dirección de envío debe empezar por la palabra Calle")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU4_AF4_ValidacionCamposEnvio(string nombre, string apellidos, string direccionEnvio, string errorExpected)
        {
            // ARRANGE
            InitialStepsParaAlquilarHerramientas();

            selectHerramientasParaAlquilar_PO.BuscarHerramientas(nombreHerramienta1, "");
            Thread.Sleep(500);
            selectHerramientasParaAlquilar_PO.AddHerramientaParaCarritoAlquiler(idHerramienta1);

            selectHerramientasParaAlquilar_PO.PulsarBotonAlquilar();

            // ACT
            crearAlquiler_PO.CamposObligatorios(nombre, apellidos, direccionEnvio);
            crearAlquiler_PO.ClickBotonAlquilarFinal();
            crearAlquiler_PO.ClickConfirmarEnDialogo();

            Thread.Sleep(500);

            // ASSERT
            Assert.True(crearAlquiler_PO.ValidarErrores(errorExpected));

        }

        [Theory]
        [InlineData("", "Gomez", "Calle Prueba", "The Name field is required")]
        [InlineData("Carlos", "", "Calle Prueba", "The Surname field is required")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU4_AF4_ValidacionCampoNombre(string nombre, string apellido, string direccionEnvio, string errorExpected)
        {
            // ARRANGE
            InitialStepsParaAlquilarHerramientas();

            selectHerramientasParaAlquilar_PO.BuscarHerramientas(nombreHerramienta1, "");
            Thread.Sleep(500);
            selectHerramientasParaAlquilar_PO.AddHerramientaParaCarritoAlquiler(idHerramienta1);

            selectHerramientasParaAlquilar_PO.PulsarBotonAlquilar();

            // ACT
            crearAlquiler_PO.CamposObligatorios(nombre, apellido, direccionEnvio);
            crearAlquiler_PO.ClickBotonAlquilarFinal();
            Thread.Sleep(500);

            // ASSERT
            Assert.True(crearAlquiler_PO.ValidarErrores(errorExpected));

        }

        public static IEnumerable<object[]> GetFechasInvalidas()
        {
            yield return new object[] { DateTime.Now.AddDays(-1), DateTime.Now.AddDays(2), "¡Error! Tu alquiler no debe empezar antes que hoy" };
        }
        [Theory]
        [MemberData(nameof(GetFechasInvalidas))]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU4_AF6_FechaIncorrectaInicio(DateTime fechaInicio, DateTime fechaFin, string errorExpected)
        {
            // ARRANGE
            InitialStepsParaAlquilarHerramientas();
            selectHerramientasParaAlquilar_PO.BuscarHerramientas(nombreHerramienta1, "");
            Thread.Sleep(500);
            selectHerramientasParaAlquilar_PO.AddHerramientaParaCarritoAlquiler(idHerramienta1);
            selectHerramientasParaAlquilar_PO.PulsarBotonAlquilar();

            crearAlquiler_PO.CamposObligatorios("Carlos", "Gomez", "Calle Prueba");

            // ACT
            crearAlquiler_PO.EstablecerFechaInicio(fechaInicio);
            crearAlquiler_PO.EstablecerFechaFin(fechaFin);
            crearAlquiler_PO.ClickBotonAlquilarFinal();
            crearAlquiler_PO.ClickConfirmarEnDialogo();

            Thread.Sleep(500);

            // ASSERT
            Assert.True(crearAlquiler_PO.ValidarErrores(errorExpected));
        }

        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void CU4_AF6_FechaIncorrectaFinal()
        {
            // ARRANGE
            InitialStepsParaAlquilarHerramientas();
            selectHerramientasParaAlquilar_PO.BuscarHerramientas(nombreHerramienta1, "");
            Thread.Sleep(500);
            selectHerramientasParaAlquilar_PO.AddHerramientaParaCarritoAlquiler(idHerramienta1);
            selectHerramientasParaAlquilar_PO.PulsarBotonAlquilar();

            crearAlquiler_PO.CamposObligatorios("Carlos", "Gomez", "Calle Prueba");

            // ACT
            crearAlquiler_PO.EstablecerFechaInicio(DateTime.Now.AddDays(1));
            crearAlquiler_PO.EstablecerFechaFin(DateTime.Now);
            crearAlquiler_PO.ClickBotonAlquilarFinal();

            Thread.Sleep(500);

            // ASSERT
            // Como la fecha final es anterior a la de inicio, el precio total no se calcula y se muestra el error de este campo 
            Assert.True(crearAlquiler_PO.ValidarErrores("The field PrecioTotal must be between 0,5 and 100")); 
            
        }


    }
} 
