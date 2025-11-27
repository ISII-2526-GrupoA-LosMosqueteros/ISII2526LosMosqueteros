using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;

namespace AppForSEII2526.UT.HerramientasController_test
{
    public class GetHerramientasParaReparar_test : AppForSEII25264SqliteUT
    {
        public GetHerramientasParaReparar_test()
        {
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


            
            _context.Fabricantes.AddRange(fabricante);
            _context.Herramientas.AddRange(herramienta);
            _context.SaveChanges();


        }

        //Cada caso contiene filtros
        public static IEnumerable<object[]> CasosDePruebaPara_GetHerramientasParaReparar_test()
        {
            var herramientaDTOs = new List<HerramientaParaRepararDTO>()
            {
                new HerramientaParaRepararDTO(1, "Taladro", "Acero", 10.3m, 1, "Bosch"),
                new HerramientaParaRepararDTO(2, "Sierra", "Madera", 20.5m, 2, "Makita"),
                new HerramientaParaRepararDTO(3, "Lijadora", "Acero", 15.75m, 3, "DeWalt")
            };

            var herramientaDTOsTC1 = new List<HerramientaParaRepararDTO>()
            {
                herramientaDTOs[0],
                herramientaDTOs[1],
                herramientaDTOs[2]
            }.ToList();

            var herramientaDTOsTC2 = new List<HerramientaParaRepararDTO>()
            {
                herramientaDTOs[1]
            }.OrderBy(h => h.Nombre).ToList();

            var herramientaDTOsTC3 = new List<HerramientaParaRepararDTO>()
            {
                herramientaDTOs[0]
            }.OrderBy(h => h.Nombre).ToList();

            var alltests = new List<object[]>
            {
                new object[] { null, null, herramientaDTOsTC1 },
                new object[] { "Sier", null, herramientaDTOsTC2 },
                new object[] { null, 1, herramientaDTOsTC3 },
            };
            return alltests;
        }
        

        //Theory queremos comprobar varios casos (como varios filtros) si solo es uno se usa Fact
        [Theory]
        [MemberData(nameof(CasosDePruebaPara_GetHerramientasParaReparar_test))]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetHerramientasParaReparar_Ok_test(string? filtroNombre, int? filtroTiempoReparacion, IList<HerramientaParaRepararDTO> expectedHerramientas)
        {
            //Arrange -> creamos el controlador con el contexto de prueba (sin loggearse)
            //se definen todas las variables que necesitamos en este caso necesitamos acceder al controlador de herramientas.
            var controller = new HerramientasController(_context, null);

            //Act -> llamamos al metodo a testear
            //hacemos como tal la llamada y el await es para que la base de datos te conteste
            var result = await controller.GetHerramientasParaReparar(filtroNombre, filtroTiempoReparacion);

            //Assert -> comprobamos que devuelve lo esperado
            //tenemos que quedarnos con el object result porque result tiene mas cosas y tambien con el valor
            var okResult = Assert.IsType<OkObjectResult>(result);
            var herramientaDTOsActual = Assert.IsType<List<HerramientaParaRepararDTO>>(okResult.Value);
            //equal para ver si lo que conseguimos es lo que queremos que sea
            Assert.Equal(expectedHerramientas, herramientaDTOsActual);
        }
    }
}
