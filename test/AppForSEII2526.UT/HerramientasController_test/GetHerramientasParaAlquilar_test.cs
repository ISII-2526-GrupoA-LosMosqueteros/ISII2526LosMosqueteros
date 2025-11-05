using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.HerramientasController_test
{
    public class GetHerramientasParaAlquilar_test : AppForSEII25264SqliteUT
    {
        public GetHerramientasParaAlquilar_test()
        {

            var fabricantes = new List<Fabricante>
            {
                new Fabricante ("Herramientas SA"),
                new Fabricante ("Tools Inc"),
                new Fabricante ("Equipos y Más")
            };
            var herramientas = new List<Herramienta>
            {
                new Herramienta ("Martillo", "Acero", 25.50m, 10,fabricantes[0]),
                new Herramienta ("Destornillador", "Acero", 15.75m, 12, fabricantes[1]),
                new Herramienta ("Taladro", "Plástico y Metal", 56.22m, 14, fabricantes[2])
            };

      
            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.SaveChanges();

        }

        public static IEnumerable<object[]> GetHerramientasParaAlquilar_TestData()
        {
            var herramientasDTO = new List<HerramientaParaAlquilarDTO>
            {
                new HerramientaParaAlquilarDTO (1, "Martillo", "Acero", "Herramientas SA", 25.50m),
                new HerramientaParaAlquilarDTO (2, "Destornillador", "Acero", "Tools Inc", 15.75m),
                new HerramientaParaAlquilarDTO (3, "Taladro", "Plástico y Metal", "Equipos y Más", 56.22m)
            };

            var herramientasDTOsTC1 = new List<HerramientaParaAlquilarDTO>
            {
                herramientasDTO[0],
                herramientasDTO[1],
                herramientasDTO[2]
            };

            var herramientasDTOsTC2 = new List<HerramientaParaAlquilarDTO> { herramientasDTO[0], herramientasDTO[1] };
            var herramientasDTOsTC3 = new List<HerramientaParaAlquilarDTO> { herramientasDTO[1] };



            var allTest = new List<object[]>
            {
                new object[] { null,null,herramientasDTOsTC1 },
                new object[] { "Destornillador" ,null,herramientasDTOsTC3},
                new object[] { null , "Acero" ,herramientasDTOsTC2},

            };

            return allTest;
        }

        [Theory] //Comprobar varios casos
        [MemberData(nameof(GetHerramientasParaAlquilar_TestData))]
        [Trait("Database", "WithoutFisture")]
        [Trait("LevelTesting", "Unit Testing")]

        public async Task GetHerramientasParaAlquilar_OK_test(string? filtroNombre, string? filtroMaterial, List<HerramientaParaAlquilarDTO> expectedHerramientas)
        {
            // Arrange (Se define todas las variables que se necesitan)
            var controlador = new HerramientasController(_context, null);
            // Act
            var resultado = await controlador.GetHerramientasParaAlquilar(filtroNombre, filtroMaterial);

            // Assert
            var okResultado = Assert.IsType<OkObjectResult>(resultado);
            var herramientaDTosActual = Assert.IsType<List<HerramientaParaAlquilarDTO>>(okResultado.Value);
            Assert.Equal(expectedHerramientas, herramientaDTosActual);


        }
    }
}