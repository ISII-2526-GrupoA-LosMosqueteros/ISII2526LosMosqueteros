using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.AlquilerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ControladorDetallesAlquiler_test
{
    public class GetDetallesHerramientasParaAlquilar_test : AppForSEII25264SqliteUT
    {
        public GetDetallesHerramientasParaAlquilar_test()
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

            ApplicationUser user = new ApplicationUser("Mario", "Torres Alarcon", "mario.torres2@alu.uclm.es","612345678");

            var alquiler = new Alquiler("Avda. España s/n, Albacete 02071",
                    DateTime.Today, DateTime.Today.AddDays(2), DateTime.Today.AddDays(5), 3,
                    31.5m, new List<AlquilarItem>(), AppForSEII2526.API.Models.TiposMetodoPago.TarjetaCredito, user);
                    alquiler.AlquilarItems.Add(new AlquilarItem(2, alquiler.PrecioTotal, herramientas[1], alquiler));

            _context.ApplicationUsers.Add(user);
            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.Add(alquiler);
            _context.SaveChanges();
        }

        [Fact]
        [Trait("Database", "WithoutFixture")]
        [Trait("LevelTesting", "Unit Testing")]
        public async Task GetDetallesHerramientasAlquiladas_NotFound_test()
        {
            // Arrange
            var mock = new Mock<ILogger<ControladorDetallesAlquiler>>();
            ILogger<ControladorDetallesAlquiler> logger = mock.Object;

            var controller = new ControladorDetallesAlquiler(_context, logger);

            // Act
            var result = await controller.GetDetallesdeHerramientasAlquiladas(0);

            //Assert
            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        [Trait("LevelTesting", "Unit Testing")]
        [Trait("Database", "WithoutFixture")]
        public async Task GetDetallesHerramientasAlquiladas_Found_test()
        {
            // Arrange
            var mock = new Mock<ILogger<ControladorDetallesAlquiler>>();
            ILogger<ControladorDetallesAlquiler> logger = mock.Object;
            var controller = new ControladorDetallesAlquiler(_context, logger);


            var expectedAlquiler = new DetalleAlquilarDTO(1, "Mario", "Torres Alarcon",
                        "Avda. España s/n, Albacete 02071", DateTime.Today, 31.5m,
                        DateTime.Today.AddDays(2), DateTime.Today.AddDays(5),
                        new List<AlquilarItemDTO>());
            expectedAlquiler.AlquilarItems.Add(new AlquilarItemDTO(2, "Destornillador", "Acero", 31.5m, 2));

            // Act 
            var result = await controller.GetDetallesdeHerramientasAlquiladas(1);

            //Assert
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            var detallesAlquilerDTO = Assert.IsType<DetalleAlquilarDTO>(okResult.Value);
           
            Assert.Equal(expectedAlquiler, detallesAlquilerDTO);

        }
    }
}
