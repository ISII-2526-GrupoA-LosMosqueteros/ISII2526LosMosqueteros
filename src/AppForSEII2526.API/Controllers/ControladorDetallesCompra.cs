using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControladorDetallesCompra : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<ControladorDetallesCompra> _logger;

        public ControladorDetallesCompra(ApplicationDbContext context, ILogger<ControladorDetallesCompra> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<DetallesCompraDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetDetallesdeHerramientasCompradas(int id)
        {
            if (_context.Compras == null)
            {
                _logger.LogError("No se encontraron compras en la base de datos.");
            }
            var compras = await _context.Compras
                .Where(c => c.Id == id)
                .Include(c => c.ApplicationUser)
                .Include(c => c.CompraItem)
                    .ThenInclude(ci => ci.Herramienta)
                        .ThenInclude(h => h.Fabricante)
                .Select(c => new DetallesCompraDTO(c.ApplicationUser.Name,c.ApplicationUser.UserName,c.ApplicationUser.Email,c.PrecioTotal,c.FechaCompra,c.CompraItem
                    .Select(ci => new CompraItemDTO(ci.Herramienta.Nombre,ci.Herramienta.Material,ci.Cantidad,ci.Descripcion,ci.Precio)).ToList<CompraItemDTO>()))
                .FirstOrDefaultAsync();
            
            if (compras == null)
            {
                _logger.LogError("No se encontraron detalles de compra para el ID proporcionado: {Id}", id);
                return NotFound();
            }
                return Ok(compras);

        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(DetallesCompraDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]

        public async Task<ActionResult> CreacionCompra(CreacionCompraDTO creaciondecompras)
        {


            if (creaciondecompras.CompraItems.Count == 0)
            {
                ModelState.AddModelError("Items de Compra", "La compra debe contener al menos un item.");
            }

            if (string.IsNullOrEmpty(creaciondecompras.Name))
            {
                ModelState.AddModelError("Nombre", "El nombre no puede estar vacío");
            }

            if (string.IsNullOrEmpty(creaciondecompras.Surname))
            {
                ModelState.AddModelError("Apellido", "El apellido no puede estar vacío");
            }

            if (string.IsNullOrEmpty(creaciondecompras.DireccionEnvio))
            {
                ModelState.AddModelError("Dirección de envio", "La dirección de envio no puede estar vacío");
            }
            


            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }


            var usuario=_context.ApplicationUsers.FirstOrDefault(u=>u.Name==creaciondecompras.Name && u.Surname==creaciondecompras.Surname);
            if (usuario == null)
            {
                ModelState.AddModelError("Usuario", "El usuario no existe.");
            }
            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            var herramientasnombres = creaciondecompras.CompraItems.Select(n => n.Nombre).ToList<string>();

            var herramientas = _context.Herramientas
                .Where(h=> herramientasnombres.Contains(h.Nombre))
                .ToList();

            Compra compra = new Compra
            {
                DireccionEnvio = creaciondecompras.DireccionEnvio,
                FechaCompra = DateTime.Now,
                TipoMetodoPago = creaciondecompras.TipoMetodoPago,
                ApplicationUser = usuario,
                CompraItem = new List<CompraItem>()
            };

            compra.PrecioTotal = 0;


            foreach (var item in creaciondecompras.CompraItems)
            {
                if (item.Cantidad <= 0)
                {
                    ModelState.AddModelError("Cantidad", "La cantidad debe ser mayor que cero.");
                }
                if (string.IsNullOrEmpty(item.Descripcion))
                {
                    ModelState.AddModelError("Descripción", "La descripción no puede estar vacia");
                }
                if (ModelState.ErrorCount > 0)
                    return BadRequest(new ValidationProblemDetails(ModelState));
                var herramienta = herramientas.FirstOrDefault(h => h.Nombre == item.Nombre);
                if (herramienta == null)
                {
                    ModelState.AddModelError("Herramienta", $"La herramienta '{item.Nombre}' no existe.");
  
                }
                else
                {
                    compra.CompraItem.Add(new CompraItem
                    {
                        HerramientaId = herramienta.Id,
                        Cantidad = item.Cantidad,
                        Descripcion = item.Descripcion,
                        Precio = herramienta.Precio * item.Cantidad,
                        Herramienta=herramienta,
                        Compra=compra
                    });
                }
            }
            compra.PrecioTotal=compra.CompraItem.Sum(ci=>ci.Precio);


            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            _context.Compras.Add(compra);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Error al guardar la compra", "Ocurrió un error al guardar la compra en la base de datos.");
                return Conflict("Error" + ex.Message);
            }

            var detallesCompra = new DetallesCompraDTO(
                compra.ApplicationUser.Name,
                compra.ApplicationUser.Surname,
                compra.DireccionEnvio,
                compra.PrecioTotal,
                compra.FechaCompra,
                compra.CompraItem.Select(h => new CompraItemDTO(h.Herramienta.Nombre, h.Herramienta.Material, h.Cantidad, h.Descripcion, h.Precio)).ToList()

            );

            return CreatedAtAction("GetDetallesdeHerramientasCompradas", new { id = compra.Id }, detallesCompra);






        }
    }
}
