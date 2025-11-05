using AppForSEII2526;
using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace AppForSEII2526.UT.HerramientasControler_test
{
    public class GetHerramientasParaOferta_test : AppForSEII25264SqliteUT
    {

        public GetHerramientasParaOferta_test()
        {
            var fabricantes = new List<Fabricante>
            {
                new Fabricante ("Herramientas SA"),
                new Fabricante ("Tools Inc"),
                new Fabricante ("Equipos y Más")
            };
            var herramientas = new List<Herramienta>
            {
                new Herramienta ("Martillo", "Acero", 25.51m, 10,fabricantes[0]),
                new Herramienta ("Destornillador", "Acero", 15.75m, 12, fabricantes[1]),
                new Herramienta ("Taladro", "Plástico", 56.22m, 14, fabricantes[2])
            };

            _context.Fabricantes.AddRange(fabricantes);
            _context.Herramientas.AddRange(herramientas);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> TestCasesFor_GetHerramientasParaOferta_OK()
        {

            var herramientaDTOs = new List<HerramientasParaOfertaDTO>()
            {
                new HerramientasParaOfertaDTO (1, "Martillo", "Acero",  "Herramientas SA", 25.51m),
                new HerramientasParaOfertaDTO (2, "Destornillador", "Acero","Tools Inc", 15.75m),
                new HerramientasParaOfertaDTO (3, "Taladro", "Plástico","Equipos y Más", 56.22m)
             };

            var herramientaDTOsTC1 = new List<HerramientasParaOfertaDTO>()
            {
                herramientaDTOs[0],
                herramientaDTOs[1],
                herramientaDTOs[2]
            };

            var herramientaDTOsTC2 = new List<HerramientasParaOfertaDTO>()
            {
                herramientaDTOs[1]
            };

            var herramientaDTOsTC3 = new List<HerramientasParaOfertaDTO>()
            {
                herramientaDTOs[0],
                herramientaDTOs[1],
                herramientaDTOs[2]
            };

            var alltest = new List<object[]>
                {
                    new object[] { null, null, herramientaDTOsTC1 },
                    new object[] { 56.22m, null, herramientaDTOsTC3 },
                    new object[] { null, "Tools Inc", herramientaDTOsTC2 }
                };

            return alltest;
        }


        [Theory]
        [MemberData(nameof(TestCasesFor_GetHerramientasParaOferta_OK))]
        [Trait("Database", "WhithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetHerramientasParaOferta_OK(decimal? precio, string? fabricante, List<HerramientasParaOfertaDTO> ofertasEsperadas)
        {
            var controller = new HerramientasController(_context, null);

            var result = await controller.GetHerramientasParaOferta(precio, fabricante);

            var okResult = Assert.IsType<OkObjectResult>(result);

            var herramientasDTOactual = Assert.IsType<List<HerramientasParaOfertaDTO>>(okResult.Value);

            Assert.Equal(ofertasEsperadas, herramientasDTOactual);
        }
    }

}

