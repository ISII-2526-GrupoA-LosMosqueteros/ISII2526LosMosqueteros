using AppForSEII2526.UIT.CU_OfertaHerramienta;
using AppForSEII2526.UIT.Shared;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;
using Xunit.Abstractions;
using System.Globalization;

namespace AppForSEII2526.UIT.CU_OfertaHerramienta
{
    public class CUOfertaHerramienta_UIT : UC_UIT
    {
        private SelectHerramientasParaOfertaPO _selectHerramientasParaOfertaPO;
        private CrearOfertaPO _crearOfertaPO;
        private DetallesOfertaPO _detalleOfertaPO;


        private const string nombreHerramienta1 = "Destornillador";
        private const string nombreHerramienta2 = "Tornillo";
        private const int idHerramienta1 = 1;
        private const string materialHerramienta1 = "Acero";
        private const string materialHerramienta2 = "Acero";
        private const string fabricanteHerramienta1 = "Wurt";
        private const string fabricanteHerramienta2 = "Phillips";
        private const decimal precioHerramienta1decimal = 12.5m;
        private const int idHerramienta2 = 3;
        private const decimal precioHerramienta2decimal = 1.15m;
        private const int porcentaje = 50;


        public CUOfertaHerramienta_UIT(ITestOutputHelper output) : base(output)
        {
            _selectHerramientasParaOfertaPO = new SelectHerramientasParaOfertaPO(_driver, _output);
            _crearOfertaPO = new CrearOfertaPO(_driver, _output);
            _detalleOfertaPO = new DetallesOfertaPO(_driver, _output);
        }

        private void InitialStepsForOfertarHerramientas()
        {
            Initial_step_opening_the_web_page();

            const string targetHref = "/Oferta/SelectHerramientaParaOferta";

            _selectHerramientasParaOfertaPO.WaitForBeingVisible(By.CssSelector($"a[href='{targetHref}']"));
            Thread.Sleep(500);

            _driver.FindElement(By.CssSelector($"a[href='{targetHref}']")).Click();
        }

        //-------------------------------------------------- PRUEBAS SELECT ------------------------------------------------------------

        
        [Theory]
        [InlineData("12,5", "Wurt", "1", "Destornillador", "Acero", "Wurt", "12,5")]
        [InlineData("1,15", "", "3", "Tornillo", "Acero", "Phillips", "1,15")]
        [InlineData("", "Wurt", "1", "Destornillador", "Acero", "Wurt", "12,5")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_2_3_AF0_filteringbyPrecioandFabricante(
            string filtroPrecio,
            string filtroFabricante,
            string expectedId,
            string expectedNombre,
            string expectedMaterial,
            string expectedFabricante,
            string expectedPrecio)
        {
            //Arrange
            InitialStepsForOfertarHerramientas();

            _selectHerramientasParaOfertaPO.BuscarHerramientas("", "");
            Thread.Sleep(500);

            var expectedHerramientas = new List<string[]>
            {
                new string[] { expectedNombre, expectedFabricante, expectedMaterial, expectedPrecio }
            };

            //Act
            _selectHerramientasParaOfertaPO.BuscarHerramientas(filtroPrecio, filtroFabricante);
            Thread.Sleep(500);

            //Assert
            Assert.True(_selectHerramientasParaOfertaPO.CheckListOfHerramientas(expectedHerramientas));
        }


        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_4_AF4_CarritoVacioBotonInactivo()
        {
            //Arrange
            InitialStepsForOfertarHerramientas();
            _selectHerramientasParaOfertaPO.BuscarHerramientas("", "");
            Thread.Sleep(500);

            //Act
            //No se añade ninguna herramienta al carrito

            //Assert
            Assert.True(_selectHerramientasParaOfertaPO.OfertarHerramientasNotAvailable());
        }

        //-------------------------------------------------- PRUEBAS SELECT Y POST ------------------------------------------------------------


        [Theory]
        [InlineData(-1, 7, "Errors: (*) Error! La fecha de inicio de tu oferta debe ser posterior a hoy")]
        [InlineData(9, 1, "Errors: (*) Error! Tu oferta debe terminar después de que empiece")]
        [InlineData(1, 6, "Errors: (*) Error! la oferta debe durar al menos una semana")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_6_AF1_ValidacionesFechas(
            int diasFechaInicio,
            int diasFechaFin,
            string expectedError)
        {
            //Arrange
            InitialStepsForOfertarHerramientas();
            _selectHerramientasParaOfertaPO.BuscarHerramientas("", "");
            Thread.Sleep(500);
            _selectHerramientasParaOfertaPO.AddHerramientaToCarrito(nombreHerramienta1);
            _selectHerramientasParaOfertaPO.ClickOfertarHerramientas();
            Thread.Sleep(500);

            DateTime fechaInicio = DateTime.Today.AddDays(diasFechaInicio);
            DateTime fechaFin = DateTime.Today.AddDays(diasFechaFin);

            //Act
            _crearOfertaPO.RellenarFormularioOferta(fechaInicio, fechaFin);
            Thread.Sleep(500);
            _crearOfertaPO.RellenarPorcentajeOferta(idHerramienta1, porcentaje);
            Thread.Sleep(500);
            _crearOfertaPO.ClickSubmitButton();
            Thread.Sleep(500);
            _crearOfertaPO.ConfirmDialog();
            Thread.Sleep(500);

            //Assert
            Assert.True(_crearOfertaPO.CheckValidationError(expectedError), $"Expected error: {expectedError}");

        }


        [Theory]
        [InlineData(-1, "Errors: (*) El porcentaje debe estar entre 0 y 100")]
        [InlineData(101, "Errors: (*) El porcentaje debe estar entre 0 y 100(*) El precio minimo es 0.05")]
        public void UC3_6_AF3_PorcentajeIncorrecto(
            int porcentaje,
            string expectedError)
        {
            //Arrange
            InitialStepsForOfertarHerramientas();
            _selectHerramientasParaOfertaPO.BuscarHerramientas("", "");
            Thread.Sleep(500);
            _selectHerramientasParaOfertaPO.AddHerramientaToCarrito(nombreHerramienta1);
            Thread.Sleep(500);
            _selectHerramientasParaOfertaPO.ClickOfertarHerramientas();
            Thread.Sleep(500);

            //Act
            DateTime fechaInicio = DateTime.Today;
            DateTime fechaFin = DateTime.Today.AddDays(8);
            _crearOfertaPO.RellenarPorcentajeOferta(idHerramienta1, porcentaje);
            Thread.Sleep(500);
            _crearOfertaPO.RellenarFormularioOferta(fechaInicio, fechaFin);
            Thread.Sleep(500);
            _crearOfertaPO.ClickSubmitButton();
            Thread.Sleep(500);
            _crearOfertaPO.ConfirmDialog();
            Thread.Sleep(500);

            //Assert
            Assert.True(_crearOfertaPO.CheckValidationError(expectedError), $"Expected error: {expectedError}");
        }


        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_5_AF3_ModificarCarrito()
        {
            //Arrange
            InitialStepsForOfertarHerramientas();
            _selectHerramientasParaOfertaPO.BuscarHerramientas("", "");
            Thread.Sleep(500);

            //Act
            _selectHerramientasParaOfertaPO.AddHerramientaToCarrito(nombreHerramienta1);
            Thread.Sleep(500);
            _selectHerramientasParaOfertaPO.AddHerramientaToCarrito(nombreHerramienta2);
            Thread.Sleep(500);

            _selectHerramientasParaOfertaPO.ClickOfertarHerramientas();
            _crearOfertaPO.PressModifyHerramientasButton();
            Thread.Sleep(500);
            _selectHerramientasParaOfertaPO.BorrarHerramientaDelCarrito(nombreHerramienta2);
            Thread.Sleep(500);
            _selectHerramientasParaOfertaPO.ClickOfertarHerramientas();
            Thread.Sleep(500);

            //Assert
            var expectedHerramientas = new List<string[]>
            {
                new string[] { nombreHerramienta1, materialHerramienta1, precioHerramienta1decimal.ToString("0.0") }
            };

            Assert.True(_crearOfertaPO.CheckListOfHerramientasParaOfertar(expectedHerramientas));
        }


        [Theory]
        [InlineData(null, 25, 31, "The Porcentaje field must be a number.")]
        [InlineData(50, null, 31, "The FechaInicio field must be a date.")]
        [InlineData(50, 25, null, "The FechaFinal field must be a date.")]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_6_AF5_DatosObligatoriosNoRellenados(
            int? porcentaje,
            int? diasFechaInicio,
            int? diasFechaFin,
            string expectedError)
        {
            //Arrange
            InitialStepsForOfertarHerramientas();
            _selectHerramientasParaOfertaPO.BuscarHerramientas("", "");
            Thread.Sleep(500);
            _selectHerramientasParaOfertaPO.AddHerramientaToCarrito(nombreHerramienta1);
            Thread.Sleep(500);
            _selectHerramientasParaOfertaPO.ClickOfertarHerramientas();
            Thread.Sleep(500);

            //Act
            _crearOfertaPO.RellenarPorcentaje(idHerramienta1, porcentaje);
            Thread.Sleep(500);
            _crearOfertaPO.RellenarFecha(By.Id("FechaInicio"), diasFechaInicio);
            Thread.Sleep(500);
            _crearOfertaPO.RellenarFecha(By.Id("FechaFinal"), diasFechaFin);
            Thread.Sleep(500);

            _crearOfertaPO.ClickSubmitButton();
            Thread.Sleep(2000);

            //Assert 
            Assert.True(_crearOfertaPO.CheckValidationError(expectedError));
        }

        //-------------------------------------------------- PRUEBAS SELECT, POST Y DETAILS ------------------------------------------------------------


        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_1_2_3_4_5_6_7_FlujoBásico()
        {
            int diasFechaInicio = 1;
            int diasFechaFin = 10;
            int porcentaje = 25;

            //Arrange
            InitialStepsForOfertarHerramientas();
            _selectHerramientasParaOfertaPO.BuscarHerramientas("", "");
            Thread.Sleep(500);
            _selectHerramientasParaOfertaPO.AddHerramientaToCarrito(nombreHerramienta1);
            Thread.Sleep(500);
            _selectHerramientasParaOfertaPO.ClickOfertarHerramientas();
            Thread.Sleep(500);

            DateTime fechaInicio = DateTime.Today.AddDays(diasFechaInicio);
            DateTime fechaFin = DateTime.Today.AddDays(diasFechaFin);
            DateTime fechaOferta = DateTime.Today;

            //Act
            _crearOfertaPO.RellenarFormularioOferta(fechaInicio, fechaFin);
            Thread.Sleep(500);
            _crearOfertaPO.RellenarPorcentajeOferta(idHerramienta1, porcentaje);
            Thread.Sleep(500);
            _crearOfertaPO.ClickSubmitButton();
            Thread.Sleep(500);
            _crearOfertaPO.ConfirmDialog();
            Thread.Sleep(500);

            //Assert
            Assert.True(_detalleOfertaPO.CheckOfertaDetail(fechaInicio, fechaFin, fechaOferta, "TarjetaCredito", "Socios", 1 ));
        }

        //-------------------------------------------------- PRUEBAS SELECT, POST Y DETAILS EXAMEN SPRINT 3 ------------------------------------------------------------
        [Fact]
        [Trait("LevelTesting", "Funcional Testing")]
        public void UC3_1_2_3_4_5_6_7_AF0_AF0_AF2_EXAMEN()
        {
            string filtroprecio = precioHerramienta2decimal.ToString();
            string filtrofabricante = fabricanteHerramienta1;
            int diasFechaInicio = 1;
            int diasFechaFin = 10;
            int porcentaje = 25;
            DateTime fechaInicio = DateTime.Today.AddDays(diasFechaInicio);
            DateTime fechaFin = DateTime.Today.AddDays(diasFechaFin);
            DateTime fechaOferta = DateTime.Today;
            decimal preciofinal = precioHerramienta2decimal - (precioHerramienta2decimal * (porcentaje/100m));

            var expectedHerramientas = new List<string[]>
            {
                new string[] { idHerramienta2.ToString(),nombreHerramienta2,materialHerramienta2,fabricanteHerramienta2,precioHerramienta2decimal.ToString() + " €",
                    porcentaje.ToString() + " %", preciofinal.ToString("0.00") + " €" }
            };

            InitialStepsForOfertarHerramientas();

            _selectHerramientasParaOfertaPO.BuscarHerramientas("", "");
            Thread.Sleep(500);
            _selectHerramientasParaOfertaPO.BuscarHerramientas("", filtrofabricante);
            Thread.Sleep(500);
            _selectHerramientasParaOfertaPO.AddHerramientaToCarrito(nombreHerramienta1);
            Thread.Sleep(500);
            _selectHerramientasParaOfertaPO.BuscarHerramientas("", "");
            Thread.Sleep(500);
            _selectHerramientasParaOfertaPO.BuscarHerramientas(filtroprecio, "");
            Thread.Sleep(500);
            _selectHerramientasParaOfertaPO.AddHerramientaToCarrito(nombreHerramienta2);
            Thread.Sleep(500);
            _selectHerramientasParaOfertaPO.ClickOfertarHerramientas();
            Thread.Sleep(500);

            _crearOfertaPO.PressModifyHerramientasButton();
            Thread.Sleep(500);
            _selectHerramientasParaOfertaPO.BorrarHerramientaDelCarrito(nombreHerramienta1);
            Thread.Sleep(500);
            _selectHerramientasParaOfertaPO.ClickOfertarHerramientas();
            Thread.Sleep(500);


            _crearOfertaPO.RellenarFormularioOferta(fechaInicio, fechaFin);
            Thread.Sleep(500);
            _crearOfertaPO.RellenarPorcentajeOferta(idHerramienta2, porcentaje);
            Thread.Sleep(500);
            _crearOfertaPO.ClickSubmitButton();
            Thread.Sleep(500);
            _crearOfertaPO.ConfirmDialog();
            Thread.Sleep(500);

            Assert.True(_detalleOfertaPO.CheckOfertaDetail(fechaInicio, fechaFin, fechaOferta, "TarjetaCredito", "Socios", 1));
            Assert.True(_detalleOfertaPO.CheckListaDeHerramientasOfertadas(expectedHerramientas));
        }
    }
}