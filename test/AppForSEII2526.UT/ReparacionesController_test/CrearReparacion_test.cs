using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ReparacionesController_test
{
    public class CrearReparacion_test : AppForSEII25264SqliteUT
    {
        public CrearReparacion_test()
        {
            //Inicialización de datos de prueba para las pruebas unitarias de GetDetalleReparar
            var fabricante = new List<Fabricante>()
            {
                new Fabricante("Bosch"),
                new Fabricante("Makita"),
                new Fabricante("DeWalt")

            };

            var herramienta = new List<Herramienta>()
            {
                new Herramienta("Taladro", "Acero", 10.3m, 2, fabricante[0]),
                new Herramienta("Sierra", "Madera", 20.5m, 2, fabricante[1]),
                new Herramienta("Lijadora", "Acero", 15.75m, 3, fabricante[2])

            };

            ApplicationUser usuario = new ApplicationUser("Juan", "Perez", "juanperez", "642709559");

            var reparacion = new Reparacion(DateTime.Today, DateTime.Today.AddDays(1), 20.6m, TiposMetodoPago.Efectivo, usuario, new List<ReparacionItem>());
            reparacion.ReparacionItems.Add(new ReparacionItem(10.3m, 2, "Solo repara", herramienta[0], reparacion));

            _context.AddRange(fabricante);
            _context.AddRange(herramienta);
            _context.Add(usuario);
            _context.Add(reparacion);
            _context.SaveChanges();



        }

        public static IEnumerable<object[]> CasosDePruebaPara_CrearReparacion_test()
        {

            var reparacionFechaEntregaAnteriorAHoy = new CreacionReparacionDTO(
                DateTime.Today.AddDays(-1),
                "Juan",
                "Perez",
                new List<RepararItemDTO>(),
                TiposMetodoPago.Efectivo,
                "+34111111111");
            reparacionFechaEntregaAnteriorAHoy.RepararItem.Add(new RepararItemDTO(1, "Taladro", 10.3m, "Reparar motor", 2));

            var reparacionSinItems = new CreacionReparacionDTO(
                DateTime.Today,
                "Juan",
                "Perez",
                new List<RepararItemDTO>(),
                TiposMetodoPago.Efectivo,
                "+34111111111");

            var reparacionSinNombre = new CreacionReparacionDTO(
                DateTime.Today,
                "",
                "Perez",
                new List<RepararItemDTO>(),
                TiposMetodoPago.Efectivo,
                "+34111111111");
            reparacionSinNombre.RepararItem.Add(new RepararItemDTO(1, "Taladro", 10.3m, "Reparar motor", 2));

            var reparacionSinApellido = new CreacionReparacionDTO(
                DateTime.Today,
                "Juan",
                "",
                new List<RepararItemDTO>(),
                TiposMetodoPago.Efectivo,
                "+34111111111");
            reparacionSinApellido.RepararItem.Add(new RepararItemDTO(1, "Taladro", 10.3m, "Reparar motor", 2));

            //nombre y apellidos rellenado, pero no existen en la base de datos
            var reparacionSinUsuario = new CreacionReparacionDTO(
                DateTime.Today,
                "Francisco",
                "Lopez",
                new List<RepararItemDTO>(),
                TiposMetodoPago.Efectivo,
                "+34111111111");
            reparacionSinUsuario.RepararItem.Add(new RepararItemDTO(1, "Taladro", 10.3m, "Reparar motor", 2));

            var reparacionCantidadErronea = new CreacionReparacionDTO(
                DateTime.Today,
                "Juan",
                "Perez",
                new List<RepararItemDTO>(),
                TiposMetodoPago.Efectivo,
                "+34111111111");
            reparacionCantidadErronea.RepararItem.Add(new RepararItemDTO(1, "Taladro", 10.3m, "Reparar motor", -1));

            var reparacionHerramientaErronea = new CreacionReparacionDTO(
                DateTime.Today,
                "Juan",
                "Perez",
                new List<RepararItemDTO>(),
                TiposMetodoPago.Efectivo,
                "+34111111111");
            reparacionHerramientaErronea.RepararItem.Add(new RepararItemDTO(1, "Martillo", 10.3m, "Reparar motor", 2));

            var reparacionTelefonoSinPrefijo = new CreacionReparacionDTO(
                DateTime.Today,
                "Juan",
                "Perez",
                new List<RepararItemDTO>(),
                TiposMetodoPago.Efectivo,
                "111111111");
            reparacionFechaEntregaAnteriorAHoy.RepararItem.Add(new RepararItemDTO(1, "Taladro", 10.3m, "Reparar motor", 2));

            var allTest = new List<object[]>
            {
                new object[] {reparacionFechaEntregaAnteriorAHoy, "La fecha de entrega no puede ser anterior a hoy." },
                new object[] {reparacionSinItems, "La reparacion debe contener al menos un item a reparar."},
                new object[] {reparacionSinNombre, "El nombre no puede estar vacio"},
                new object[] {reparacionSinApellido, "El apellido no puede estar vacio"},
                new object[] {reparacionSinUsuario, "Error! El usuario no está registrado" },
                new object[] {reparacionCantidadErronea, "La cantidad debe ser mayor de 0"},
                new object[] {reparacionHerramientaErronea, $"La herramienta {reparacionHerramientaErronea.RepararItem[0].Nombre} no existe." },
                new object[] { reparacionTelefonoSinPrefijo, "¡Error!, el telefono debe empezar por +34."}
            };

            return allTest;
        }

        //Theory porque queremos comprobar todos los casos de error (los ifs del POST)
        [Theory]
        [MemberData(nameof(CasosDePruebaPara_CrearReparacion_test))]
        [Trait("Database", "WithoutFisture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task CrearReparacion_Test_BadRequest(CreacionReparacionDTO creacionDeReparacionesDTO, string errorEsperado) //campoEsperado es el campo del error que queremos comprobar de allTest
        {
            // Arrange
            var mock = new Mock<ILogger<ReparacionesController>>();
            ILogger<ReparacionesController> logger = mock.Object;
            var controller = new ReparacionesController(_context, logger);

            // Act
            var result = await controller.CrearReparacion(creacionDeReparacionesDTO);

            //Assert
            //we check that the response type is BadRequest and obtain the error returned
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];

            //we check that the expected error message and actual are the same
            Assert.StartsWith(errorEsperado, errorActual);
        }



        //Fact para el caso de que la información introducida es correcta
        [Fact]
        [Trait("Database", "WithoutFisture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task CreacionReparacion_Test_OK()
        {
            // Arrange (Se define todas las variables que se necesitan)
            var controller = new ReparacionesController(_context, null);

            DateTime desde = DateTime.Today.AddDays(6);
            DateTime hasta = DateTime.Today.AddDays(8);


            var creacionDeReparaciones = new CreacionReparacionDTO(desde, "Juan", "Perez", new List<RepararItemDTO>(), TiposMetodoPago.TarjetaCredito, "+34111111111");
            creacionDeReparaciones.RepararItem.Add(new RepararItemDTO(500,"Sierra", 1200.0m, "Sierra para Madera", 2));

            var expectedReparacion = new DetalleRepararDTO(2, desde, hasta, 41.0m, "Juan", "Perez", new List<RepararItemDTO>());
            expectedReparacion.RepararItem.Add(new RepararItemDTO(2, "Sierra", 41.0m, "Sierra para Madera", 2));

            //Act (Se ejecuta la acción a testear)
            var result = await controller.CrearReparacion(creacionDeReparaciones);

            //Assert (Se comprueba que el resultado es el esperado)
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var reparacionCreada = Assert.IsType<DetalleRepararDTO>(createdAtActionResult.Value);

            Assert.Equal(expectedReparacion, reparacionCreada);
        }
    }


}
