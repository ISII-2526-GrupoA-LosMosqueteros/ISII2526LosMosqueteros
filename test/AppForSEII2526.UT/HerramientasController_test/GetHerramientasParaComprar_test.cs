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
    public class GetHerramientasParaComprar_test : AppForSEII25264SqliteUT
    {
        public GetHerramientasParaComprar_test()
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

        public static IEnumerable<object[]> GetHerramientasParaComprar_Test_OK()
        {
            var herramientasDTO = new List<HerramientasParaComprarDTO>
            {
                new HerramientasParaComprarDTO (1, "Martillo", "Acero", 25.50m, "Herramientas SA"),
                new HerramientasParaComprarDTO (2, "Destornillador", "Acero", 15.75m, "Tools Inc"),
                new HerramientasParaComprarDTO (3, "Taladro", "Plástico y Metal", 56.22m, "Equipos y Más")
            };

            var herramientasDTOsTC1 = new List<HerramientasParaComprarDTO>
            {
                herramientasDTO[0],
                herramientasDTO[1],
                herramientasDTO[2]
            }.OrderBy(m => m.Nombre).ToList();

            var herramientasDTOsTC2 = new List<HerramientasParaComprarDTO> { herramientasDTO[1],herramientasDTO[0] }.OrderBy(m=>m.Nombre).ToList();
            var herramientasDTOsTC3 = new List<HerramientasParaComprarDTO> { herramientasDTO[1] };



            var allTest = new List<object[]>
            {
                new object[] { null,null,herramientasDTOsTC1 },
                new object[] { 16.02m ,null,herramientasDTOsTC3},
                new object[] { null , "Acero" ,herramientasDTOsTC2},

            };

            return allTest;
        }

        [Theory] //Comprobar varios casos
        [MemberData(nameof(GetHerramientasParaComprar_Test_OK))]
        [Trait("Database", "WithoutFisture")]
        [Trait("LevelTesting", "Unit Testing")]

        public async Task GetHerramientasParaComprar_OK_test(decimal? filtroPrecio, string? filtroMaterial, List<HerramientasParaComprarDTO> expectedHerramientas)
        {
            // Arrange (Se define todas las variables que se necesitan)
            var controlador = new HerramientasController(_context, null);

            // Act
            var resultado = await controlador.GetHerramientasParaComprar(filtroPrecio, filtroMaterial);

            // Assert
            var okResultado=Assert.IsType<OkObjectResult>(resultado);
            var herramientDTosActual = Assert.IsType<List<HerramientasParaComprarDTO>>(okResultado.Value);
            Assert.Equal(expectedHerramientas, herramientDTosActual);


        }
    }
}
