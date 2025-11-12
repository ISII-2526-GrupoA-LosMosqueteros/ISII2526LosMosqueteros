using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.AlquilerDTOs;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ControladorDetallesAlquiler_test
{
    public class CreacionAlquiler_test : AppForSEII25264SqliteUT
    {
        private const string _userName = "Mario";
        private const string _customerSurname = "Torres";
        private const string _deliveryAddress = "Avda. España s/n, Albacete 02071";

        private const string _herramienta1Nombre = "Martillo";
        private const string _herramienta2Nombre = "Destornillador";
        private const string _herramienta3Nombre = "Taladro";
        private const string _fabricante1Nombre = "Herramientas SA";
        private const string _fabricante2Nombre = "Tools Inc";
        private const string _fabricante3Nombre = "Equipos y Más";

        public CreacionAlquiler_test()
        {
            var fabricantes = new List<Fabricante>
            {
                new Fabricante (_fabricante1Nombre),
                new Fabricante (_fabricante2Nombre),
                new Fabricante (_fabricante3Nombre)
            };
            var herramientas = new List<Herramienta>
            {
                new Herramienta (_herramienta1Nombre, "Acero", 25.50m, 10,fabricantes[0]),
                new Herramienta (_herramienta2Nombre, "Acero", 15.75m, 12, fabricantes[1]),
                new Herramienta (_herramienta3Nombre, "Plástico y Metal", 56.22m, 14, fabricantes[2])
            };

            ApplicationUser user = new ApplicationUser("Mario", "Torres", _userName, "612345678");

            var alquiler = new Alquiler(_deliveryAddress,
                    DateTime.Today, DateTime.Today.AddDays(2), DateTime.Today.AddDays(5), 3,
                    31.5m, new List<AlquilarItem>(), AppForSEII2526.API.Models.TiposMetodoPago.TarjetaCredito, user);
            alquiler.AlquilarItems.Add(new AlquilarItem(2, alquiler.PrecioTotal, herramientas[1], alquiler));

            _context.ApplicationUsers.Add(user);
            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.Add(alquiler);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_CreacionAlquiler()
        {

            var alquilarNoITem = new CreacionAlquilerDTO(_userName, _customerSurname,
                _deliveryAddress, TiposMetodoPago.TarjetaCredito,
                DateTime.Today, DateTime.Today, new List<AlquilarItemDTO>());

            var alquilarItems = new List<AlquilarItemDTO>() { new AlquilarItemDTO(2, _herramienta2Nombre, "Acero", 31.5m, 3) };

            var alquilerFechaInicio = new CreacionAlquilerDTO(_userName, _customerSurname,
                _deliveryAddress, TiposMetodoPago.TarjetaCredito,
                DateTime.Today.AddDays(-1), DateTime.Today.AddDays(5), alquilarItems);

            var alquilarFechaInicioFin = new CreacionAlquilerDTO(_userName, _customerSurname,
                _deliveryAddress, TiposMetodoPago.TarjetaCredito,
                DateTime.Today.AddDays(5), DateTime.Today.AddDays(2), alquilarItems);

            var alquilarNoUsuario = new CreacionAlquilerDTO("Martin", _customerSurname,
                _deliveryAddress, TiposMetodoPago.TarjetaCredito,
                DateTime.Today.AddDays(2), DateTime.Today.AddDays(4), alquilarItems);

            var alquilarNoNombre = new CreacionAlquilerDTO(null, _customerSurname,
                _deliveryAddress, TiposMetodoPago.TarjetaCredito,
                DateTime.Today.AddDays(2), DateTime.Today.AddDays(4), alquilarItems);

            var alquilarNoApellidos = new CreacionAlquilerDTO(_userName, null,
                _deliveryAddress, TiposMetodoPago.TarjetaCredito,
                DateTime.Today.AddDays(2), DateTime.Today.AddDays(4), alquilarItems);

            var alquilarNoDireccionEnvio = new CreacionAlquilerDTO(_userName, _customerSurname,
                null, TiposMetodoPago.TarjetaCredito,
                DateTime.Today.AddDays(2), DateTime.Today.AddDays(4), alquilarItems);

            var allTests = new List<object[]>
            {             
                new object[] { alquilarNoITem, "¡Error! Tienes que incluir al menos una herramienta para alquilar",  },
                new object[] { alquilerFechaInicio, "¡Error! Tu alquiler no debe empezar antes que hoy", },
                new object[] { alquilarFechaInicioFin, "¡Error! Tu alquiler debe acabar después de cuando empezó", },
                new object[] { alquilarNoUsuario, "¡Error! Usuario no registrado", },
                new object[] { alquilarNoNombre, "¡Error! El nombre es un campo obligatorio", },
                new object[] { alquilarNoApellidos, "¡Error! Los apellidos son un campo obligatorio", },
                new object[] { alquilarNoDireccionEnvio, "¡Error! La direccion de envio es un campo obligatorio", },


            };

            return allTests;
        }

        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesFor_CreacionAlquiler))]
        public async Task CreacionAlquiler_Error_test(CreacionAlquilerDTO alquilerDTO, string errorExpected)
        {
            // Arrange
            var mock = new Mock<ILogger<ControladorDetallesAlquiler>>();
            ILogger<ControladorDetallesAlquiler> logger = mock.Object;

            var controller = new ControladorDetallesAlquiler(_context, logger);

            // Act
            var result = await controller.CreacionAlquiler(alquilerDTO);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];

            Assert.StartsWith(errorExpected, errorActual);

        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CreacionAlquiler_Success_test()
        {
            // Arrange
            var mock = new Mock<ILogger<ControladorDetallesAlquiler>>();
            ILogger<ControladorDetallesAlquiler> logger = mock.Object;

            var controller = new ControladorDetallesAlquiler(_context, logger);

            DateTime fechaInicio = DateTime.Today.AddDays(2);
            DateTime fechaFin = DateTime.Today.AddDays(5);

            var alquilerDTO = new CreacionAlquilerDTO(_userName, _customerSurname,
                _deliveryAddress, TiposMetodoPago.TarjetaCredito,
                fechaInicio, fechaFin, new List<AlquilarItemDTO>()
                { new AlquilarItemDTO(2, _herramienta1Nombre, "Acero", 31.5m,2) });

            var expectedalquilerDetalleDTO = new DetalleAlquilarDTO(2, _userName, _customerSurname,
                        "Avda. España s/n, Albacete 02071", DateTime.Today, 31.5m,
                        DateTime.Today.AddDays(2), DateTime.Today.AddDays(5),
                        new List<AlquilarItemDTO>());
            expectedalquilerDetalleDTO.AlquilarItems.Add(new AlquilarItemDTO(1, _herramienta1Nombre, "Acero", 31.5m, 2));


            // Act
            var result = await controller.CreacionAlquiler(alquilerDTO);

            //Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualRentalDetailDTO = Assert.IsType<DetalleAlquilarDTO>(createdResult.Value);

            Assert.Equal(expectedalquilerDetalleDTO, actualRentalDetailDTO);

        }

    }
}