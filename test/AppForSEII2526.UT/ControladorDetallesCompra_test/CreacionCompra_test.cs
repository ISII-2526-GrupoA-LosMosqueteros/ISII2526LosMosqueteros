using AppForSEII2526.API.Controllers;
using AppForSEII2526.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppForSEII2526.UT.ControladorDetallesCompra_test
{
    public class CreacionCompra_test:AppForSEII25264SqliteUT
    {
        public CreacionCompra_test()
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

            var compra = new Compra("Av. España 21", DateTime.Today, new List<CompraItem>(), 31.5m, TiposMetodoPago.TarjetaCredito, usuario);
            compra.CompraItem.Add(new CompraItem(2, "", herramientas[1], compra));

            _context.AddRange(fabricantes);
            _context.AddRange(herramientas);
            _context.Add(usuario);
            _context.Add(compra);
            _context.SaveChanges();
        }

        public static IEnumerable<object[]> CasosPrueba_CreacionCompraDTOs()
        {
            var compra_sin_herramientas= new CreacionCompraDTO("Juan", "Perez", "Av. España 21", TiposMetodoPago.TarjetaCredito, null, null, new List<CompraItemDTO>());

            var compra_sin_nombre= new CreacionCompraDTO("", "Perez", "Av. España 21", TiposMetodoPago.TarjetaCredito, null, null, new List<CompraItemDTO>());
               compra_sin_nombre.CompraItems.Add( new CompraItemDTO("Destornillador", "Acero", 2, "", 15.75m));

            var compra_sin_apellido= new CreacionCompraDTO("Juan", "", "Av. España 21", TiposMetodoPago.TarjetaCredito, null, null, new List<CompraItemDTO>());
               compra_sin_apellido.CompraItems.Add( new CompraItemDTO("Destornillador", "Acero", 2, "", 15.75m));

            var compra_sin_direccion= new CreacionCompraDTO("Juan", "Perez", "", TiposMetodoPago.TarjetaCredito, null, null, new List<CompraItemDTO>());
                compra_sin_direccion.CompraItems.Add( new CompraItemDTO("Destornillador", "Acero", 2, "", 15.75m));

            var compra_usuario_NF= new CreacionCompraDTO("Pedro", "Gomez", "Calle Falsa 123", TiposMetodoPago.PayPal, null, null, new List<CompraItemDTO>());
               compra_usuario_NF.CompraItems.Add( new CompraItemDTO("Martillo", "Acero", 1, "", 25.50m));

            var compra_sin_descripcion= new CreacionCompraDTO("Juan", "Perez", "Av. España 21", TiposMetodoPago.TarjetaCredito, null, null, new List<CompraItemDTO>());
               compra_sin_descripcion.CompraItems.Add( new CompraItemDTO("Destornillador", "Acero", 2, "", 15.75m));

            var compra_cantidad= new CreacionCompraDTO("Juan", "Perez", "Av. España 21", TiposMetodoPago.TarjetaCredito, null, null, new List<CompraItemDTO>());
               compra_cantidad.CompraItems.Add( new CompraItemDTO("Destornillador", "Acero", 0, "", 15.75m));
            
            var compra_herramienta_erronea= new CreacionCompraDTO("Juan", "Perez", "Av. España 21", TiposMetodoPago.TarjetaCredito, null, null, new List<CompraItemDTO>());
               compra_herramienta_erronea.CompraItems.Add( new CompraItemDTO("Tornillo", "Acero", 2, "Tornillo de Acero", 15.75m));


            var allTest = new List<object[]>
            {
                new object[] {compra_sin_herramientas,"La compra debe contener al menos un item." },
                new object[] {compra_sin_nombre, "El nombre no puede estar vacío"},
                new object[] {compra_sin_apellido, "El apellido no puede estar vacío"},
                new object[] {compra_sin_direccion, "La dirección de envio no puede estar vacío" },
                new object[] {compra_usuario_NF, "El usuario no existe." },
                new object[] {compra_sin_descripcion, "La descripción no puede estar vacia"},
                new object[] {compra_cantidad, "La cantidad debe ser mayor que cero."},
                new object[] {compra_herramienta_erronea, $"La herramienta '{compra_herramienta_erronea.CompraItems[0].Nombre}' no existe." }
            };

            return allTest;
        }

        [Theory]
        [MemberData(nameof(CasosPrueba_CreacionCompraDTOs))]
        [Trait("Database", "WithoutFisture")]
        [Trait("LevelTesting", "Unit Testing")]

        public async Task CreacionCompra_Test_BadRequest(CreacionCompraDTO creaciondecompras, string erroresperado)
        {
            //Arrange (Se define todas las variables que se necesitan)
            var controller = new ControladorDetallesCompra(_context, null);
            var mock = new Mock<ILogger<ControladorDetallesCompra>>();
            ILogger<ControladorDetallesCompra> logger = mock.Object;

            //Act (Se ejecuta la acción a testear)

            var result = await controller.CreacionCompra(creaciondecompras);

            //Arrange (Se comprueba que el resultado es el esperado)

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var detallesProblem = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);

            var errorActual = detallesProblem.Errors.First().Value[0];
            Assert.StartsWith(erroresperado, errorActual);
        }


        [Fact]
        [Trait("Database", "WithoutFisture")]
        [Trait("LevelTesting", "Unit Testing")]

        public async Task CreacionCompra_Test_OK()
        {
            // Arrange (Se define todas las variables que se necesitan)
            var controller = new ControladorDetallesCompra(_context, null);

            var creaciondecompras = new CreacionCompraDTO("Juan", "Perez", "Av. España 21", TiposMetodoPago.TarjetaCredito, null, null, new List<CompraItemDTO>());
            creaciondecompras.CompraItems.Add(new CompraItemDTO("Destornillador", "Acero", 2, "Destornillador Estrella", 31.5m));

            var expectedCompra = new DetallesCompraDTO("Juan", "Perez", "Av. España 21", 31.5m, DateTime.Today, new List<CompraItemDTO>());
            expectedCompra.CompraItem.Add(new CompraItemDTO("Destornillador", "Acero", 2, "Destornillador Estrella", 31.5m));

            //Act (Se ejecuta la acción a testear)

            var result= await controller.CreacionCompra(creaciondecompras);

            //Assert (Se comprueba que el resultado es el esperado)

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var compraCreada=Assert.IsType<DetallesCompraDTO>(createdAtActionResult.Value);

            Assert.Equal(expectedCompra, compraCreada);
        }
    }
}
