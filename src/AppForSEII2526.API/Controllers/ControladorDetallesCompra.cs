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
                .Select(c => new DetallesCompraDTO(c.Id,c.ApplicationUser.Name,c.ApplicationUser.UserName,c.ApplicationUser.Email,c.PrecioTotal,c.FechaCompra,c.CompraItem
                    .Select(ci => new CompraItemDTO(ci.Herramienta.Id,ci.Herramienta.Nombre,ci.Herramienta.Material,ci.Cantidad,ci.Descripcion,ci.Precio)).ToList<CompraItemDTO>()))
                .FirstOrDefaultAsync();
            
            if (compras == null)
            {
                _logger.LogError("No se encontraron detalles de compra para el ID proporcionado: {Id}", id);
                return NotFound();
            }
                return Ok(compras);

        }
    }
}
