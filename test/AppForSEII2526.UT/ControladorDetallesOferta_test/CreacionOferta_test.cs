using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ControladorDetallesOferta_test
{
    public class CreacionOferta_test : AppForSEII25264SqliteUT
    {
        public CreacionOferta_test()
        {
            var fabricantes = new List<Fabricante>
            {
                new Fabricante("Herramientas SA"),
                new Fabricante("Tools Inc"),
                new Fabricante("Equipos y Más")
            };

            var herramientas = new List<Herramienta>
            {
                new Herramienta("Martillo", "Acero", 25.50m, 10, fabricantes[0]),
                new Herramienta("Destornillador", "Acero", 15.75m, 12, fabricantes[1]),
                new Herramienta("Taladro", "Plástico y Metal", 56.22m, 14, fabricantes[2])
            };

            ApplicationUser usuario = new ApplicationUser("Juan", "Perez", "juanperez", "642709559");

            var oferta = new Oferta(DateTime.Today, DateTime.Today.AddDays(10), DateTime.Today, new List<OfertaItem>(),
                                    TiposDirigdaOferta.Clientes, TiposMetodoPago.TarjetaCredito, usuario);

            oferta.OfertaItems.Add(new OfertaItem(50, 31.5m, herramientas[1], oferta));

            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.Add(usuario);
            _context.Add(oferta);
            _context.SaveChanges();

        }

        public static IEnumerable<object[]> TestCasesFor_CreateOferta()
        {
            var ofertaNoItem = new CreacionOfertaDTO(DateTime.Today.AddDays(5), DateTime.Today.AddDays(2),
                TiposMetodoPago.PayPal, TiposDirigdaOferta.Clientes, new List<OfertaItemDTO>());

            var ofertaItems = new List<OfertaItemDTO>() { new OfertaItemDTO("Martillo", "Acero", "Herramientas SA", 25, 1, 50) };

            var ofertaFromBeforeToday = new CreacionOfertaDTO(DateTime.Today.AddDays(5), DateTime.Today.AddDays(-1),
                TiposMetodoPago.PayPal, TiposDirigdaOferta.Clientes, ofertaItems);

            var ofertaToBeforeFrom = new CreacionOfertaDTO(DateTime.Today.AddDays(2), DateTime.Today.AddDays(5),
                TiposMetodoPago.PayPal, TiposDirigdaOferta.Clientes, ofertaItems);

            var ofertaHerramientaNoDisponible = new CreacionOfertaDTO(DateTime.Today.AddDays(5), DateTime.Today.AddDays(2),
                TiposMetodoPago.PayPal, TiposDirigdaOferta.Clientes,
                new List<OfertaItemDTO>()
                { new OfertaItemDTO("Llave Inglesa", "Acero", "Herramientas SA", 20 ,1, 50) });

            var ofertaPorcentajeNoValido = new CreacionOfertaDTO(DateTime.Today.AddDays(5), DateTime.Today.AddDays(2),
                TiposMetodoPago.PayPal, TiposDirigdaOferta.Clientes,
                new List<OfertaItemDTO>()
                { new OfertaItemDTO("Martillo", "Acero", "Herramientas SA", 20 ,1, -100) });

            var ofertaSinFechaFinal = new CreacionOfertaDTO(DateTime.MinValue, DateTime.Today.AddDays(2),
                TiposMetodoPago.PayPal, TiposDirigdaOferta.Clientes, ofertaItems);

            var ofertaSinFechaInicio = new CreacionOfertaDTO(DateTime.Today.AddDays(5), DateTime.MinValue,
                TiposMetodoPago.PayPal, TiposDirigdaOferta.Clientes, ofertaItems);

            var allTests = new List<object[]>
            {
                new object[] { ofertaNoItem, "Error! Tienes que incluir al menos una herramienta para aplicar una oferta" },
                new object[] { ofertaFromBeforeToday, "Error! La fecha de inicio de tu oferta debe ser posterior a hoy" },
                new object[] { ofertaToBeforeFrom, "Error! Tu oferta debe terminar después de que empiece" },
                new object[] { ofertaHerramientaNoDisponible, $"La herramienta con nombre {ofertaHerramientaNoDisponible.OfertaItem[0].Nombre} no fue encontrada" },
                new object[] { ofertaPorcentajeNoValido, "Error: Introduce un valor entre 0 y 100" },
                new object[] { ofertaSinFechaFinal, "Error! Fecha Final es un campo obligatorio" },
                new object[] { ofertaSinFechaInicio, "Error! Fecha Inicio es un campo obligatorio" }

            };

            return allTests;
        }

        
        [Theory]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        [MemberData(nameof(TestCasesFor_CreateOferta))]
        public async Task CreateOferta_Error_test(CreacionOfertaDTO ofertaDTO, string errorExpected)
        {
            var mock = new Mock<ILogger<ControladorDetallesOferta>>();
            ILogger<ControladorDetallesOferta> logger = mock.Object;
            var controller = new ControladorDetallesOferta(_context, logger);

            var result = await controller.CreacionOferta(ofertaDTO);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = problemDetails.Errors.First().Value[0];

            Assert.StartsWith(errorExpected, errorActual);
        }

        
        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task CreateOferta_Success_test()
        {

            var mock = new Mock<ILogger<ControladorDetallesOferta>>();
            ILogger<ControladorDetallesOferta> logger = mock.Object;
            var controller = new ControladorDetallesOferta(_context, logger);

            var ofertaDTO = new CreacionOfertaDTO(DateTime.Today.AddDays(5), DateTime.Today.AddDays(2),
                TiposMetodoPago.PayPal, TiposDirigdaOferta.Clientes,
                new List<OfertaItemDTO>() { new OfertaItemDTO("Destornillador", "Acero", "Tools Inc", 15.75m, 2, 50) });

            var expectedOfertaDetailDTO = new DetalleOfertaDTO(DateTime.Today.AddDays(2), DateTime.Today.AddDays(5), TiposMetodoPago.PayPal, TiposDirigdaOferta.Clientes,
                new List<OfertaItemDTO>() { new OfertaItemDTO("Destornillador", "Acero", "Tools Inc", 15.75m, 2, 50) },
                DateTime.Today,
                2
            );

             var result = await controller.CreacionOferta(ofertaDTO);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var actualOfertaDetailDTO = Assert.IsType<DetalleOfertaDTO>(createdResult.Value);

            Assert.Equal(expectedOfertaDetailDTO, actualOfertaDetailDTO);
        }
    }
}
