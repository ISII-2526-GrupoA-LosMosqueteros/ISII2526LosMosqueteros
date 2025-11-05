using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ControladorDetallesCompra_test
{
    public class GetDetallesdeHerramientasCompradas_test: AppForSEII25264SqliteUT
    {
        public GetDetallesdeHerramientasCompradas_test()
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

            var compra = new Compra ("Av. España 21",DateTime.Today,new List<CompraItem>(),31.5m, TiposMetodoPago.TarjetaCredito, usuario);
            compra.CompraItem.Add(new CompraItem(2, "", herramientas[1], compra));

            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.Add(usuario);
            _context.Add(compra);
            _context.SaveChanges();

        }

        [Fact] 
        [Trait("Database", "WithoutFisture")]
        [Trait("LevelTesting", "Unit Testing")]

        public async Task GetDetallesHerramientasCompradas_NotFound_test()
        {
            //Arrange (Se define todas las variables que se necesitan)
            var mock=new Mock<ILogger<ControladorDetallesCompra>>();
            ILogger<ControladorDetallesCompra> logger=mock.Object;

            var controller= new ControladorDetallesCompra(_context,logger);

            //Act (Se ejecuta la acción a testear)
            var result=await controller.GetDetallesdeHerramientasCompradas(0);

            //Assert (Se comprueba que el resultado es el esperado)
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        [Trait("Database", "WithoutFisture")]
        [Trait("LevelTesting", "Unit Testing")]

        public async Task GetDetallesHerramientasCompradas_Found_test()
        {
            //Arrange (Se define todas las variables que se necesitan)
            var mock = new Mock<ILogger<ControladorDetallesCompra>>();
            ILogger<ControladorDetallesCompra> logger = mock.Object;

            var controller = new ControladorDetallesCompra(_context, logger);

            var expectedCompra = new DetallesCompraDTO("Juan", "Perez", "Av. España 21", 31.5m, DateTime.Today, new List<CompraItemDTO>());
            expectedCompra.CompraItem.Add(new CompraItemDTO("Destornillador", "Acero", 2, "", 15.75m));

            //Act
            var result= await controller.GetDetallesdeHerramientasCompradas(1);

            //Assert

            var Okresult=Assert.IsType<OkObjectResult>(result);
            var detallesCompraDTO=Assert.IsType<DetallesCompraDTO>(Okresult.Value);

            Assert.Equal(expectedCompra, detallesCompraDTO);
        }
    }
}
