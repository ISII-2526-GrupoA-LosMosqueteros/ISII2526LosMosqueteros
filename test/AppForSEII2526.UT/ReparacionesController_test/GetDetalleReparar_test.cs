using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ReparacionesController_test
{
    public class GetDetalleReparar_test : AppForSEII25264SqliteUT
    {
        public GetDetalleReparar_test()
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
                new Herramienta("Taladro", "Acero", 10.3m, 1, fabricante[0]),
                new Herramienta("Sierra", "Madera", 20.5m, 2, fabricante[1]),
                new Herramienta("Lijadora", "Acero", 15.75m, 3, fabricante[2])

            };

            var reparacion = new Reparacion()
            {
                Id = 1,
                FechaEntrega = DateTime.Today,
                FechaRecogida = DateTime.Today.AddDays(5),
                PrecioTotal = 60.3m,
                ApplicationUser = new ApplicationUser("Juan", "Perez", "juanperez", "642709559"),
                ReparacionItems = new List<ReparacionItem>()
            };

            reparacion.ReparacionItems.Add(new ReparacionItem()
            {
                Herramienta = herramienta[0],
                Precio = 10.3m,
                Descripcion = "Solo repara",
                Cantidad = 1,
                Reparacion = reparacion
            });


            _context.Fabricantes.AddRange(fabricante);
            _context.Herramientas.AddRange(herramienta);
            _context.Reparaciones.Add(reparacion);
            _context.SaveChanges();

        }

        //Theory queremos comprobar varios casos (como varios filtros) si solo es uno se usa Fact
        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetDetalleReparar_NotFound_test()
        {
            //Arrange -> creamos el controlador con el contexto de prueba (sin loggearse)
            //se definen todas las variables que necesitamos en este caso necesitamos acceder al controlador de herramientas.
            var mock = new Mock<ILogger<ReparacionesController>>();
            ILogger<ReparacionesController> logger = mock.Object;
            var controller = new ReparacionesController(_context, logger);

            //Act -> llamamos al metodo a testear
            //hacemos como tal la llamada y el await es para que la base de datos te conteste
            var result = await controller.GetDetalleReparar(0);

            //Assert -> comprobamos que devuelve lo esperado
            //tenemos que quedarnos con el object result porque result tiene mas cosas y tambien con el valor
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetDetalleReparar_Ok_test()
        {
            //Arrange -> creamos el controlador con el contexto de prueba (con el logger)
            //se definen todas las variables que necesitamos en este caso necesitamos acceder al controlador de herramientas.
            var mock = new Mock<ILogger<ReparacionesController>>();
            ILogger<ReparacionesController> logger = mock.Object;
            var controller = new ReparacionesController(_context, logger);

            var expectedReparacion = new DetalleRepararDTO(
                1,
                DateTime.Today,
                DateTime.Today.AddDays(5),
                60.3m,
                "Juan",
                "Perez",
                new List<RepararItemDTO>()
            );
            expectedReparacion.RepararItem.Add(new RepararItemDTO(1, "Taladro", 10.3m, "Solo repara", 1));

            //Act -> llamamos al metodo a testear
            //hacemos como tal la llamada y el await es para que la base de datos te conteste
            var result = await controller.GetDetalleReparar(1);
            //Assert -> comprobamos que devuelve lo esperado
            //tenemos que quedarnos con el object result porque result tiene mas cosas y tambien con el valor
            var okResult = Assert.IsType<OkObjectResult>(result);
            var detalleRepararDTO = Assert.IsType<DetalleRepararDTO>(okResult.Value);
            Assert.Equal(expectedReparacion, detalleRepararDTO);
        }

        
        
    }
}
