using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ControladorDetallesOferta_test
{
    
    public class GetDetallesdeOfertasCreadas_test : AppForSEII25264SqliteUT
    {
        public GetDetallesdeOfertasCreadas_test()
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
            ApplicationUser usuario = new ApplicationUser("Juan", "Perez", "juanperez", "642709559");

            var oferta = new Oferta(DateTime.Today, DateTime.Today.AddDays(10), DateTime.Today, new List<OfertaItem>(), TiposDirigdaOferta.Clientes, TiposMetodoPago.TarjetaCredito, usuario);
            oferta.OfertaItems.Add(new OfertaItem(50, 38.2m, herramientas[1], oferta));
            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.Add(usuario);
            _context.Add(oferta);
            _context.SaveChanges();

        }

        [Fact]
        [Trait("Database", "WithoutFisture")]
        [Trait("LevelTesting", "Unit Testing")]

        public async Task GetDetallesdeOfertasCreadas_NotFound_test()
        {
            //Arrange (Se define todas las variables que se necesitan)
            var mock = new Mock<ILogger<ControladorDetallesOferta>>();
            ILogger<ControladorDetallesOferta> logger = mock.Object;

            var controller = new ControladorDetallesOferta(_context, logger);

            //Act (Se ejecuta la acción a testear)
            var result = await controller.GetDetallesdeOfertasCreadas(0);

            //Assert (Se comprueba que el resultado es el esperado)
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("Database", "WithoutFisture")]
        [Trait("LevelTesting", "Unit Testing")]

        public async Task GetDetallesdeOfertasCreadas_Found_test()
        {
            //Arrange (Se define todas las variables que se necesitan)
            var mock = new Mock<ILogger<ControladorDetallesOferta>>();
            ILogger<ControladorDetallesOferta> logger = mock.Object;

            var controller = new ControladorDetallesOferta(_context, logger);

            var expectedOferta = new DetalleOfertaDTO(DateTime.Today, DateTime.Today.AddDays(10), TiposMetodoPago.TarjetaCredito, TiposDirigdaOferta.Clientes, new List<OfertaItemDTO>(), DateTime.Today, 1);
            expectedOferta.OfertaItem.Add(new OfertaItemDTO("Destornillador", "Acero", "Tools Inc", 15.75m, 38.2m, 2));

            //Act
            var result = await controller.GetDetallesdeOfertasCreadas(1);

            //Assert

            var Okresult = Assert.IsType<OkObjectResult>(result);
            var detallesCompraDTO = Assert.IsType<DetalleOfertaDTO>(Okresult.Value);

            Assert.Equal(expectedOferta, detallesCompraDTO);
        }
    }
}
