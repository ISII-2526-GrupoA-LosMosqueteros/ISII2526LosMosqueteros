using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReparacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReparacionesController> _logger;

        public ReparacionesController(ApplicationDbContext context, ILogger<ReparacionesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(DetalleRepararDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetDetalleReparar(int id)
        {
            if (_context.Reparaciones == null)
            {
                _logger.LogError("Error: Reparaciones table does not exist");
                return NotFound();
            }

            var reparacion = await _context.Reparaciones
             .Where(r => r.Id == id)
                 .Include(r => r.ReparacionItems) //join table ReparacionItem
                    .ThenInclude(ri => ri.Herramienta) //then join table Herramienta
                        .ThenInclude(h => h.Fabricante) //then join table Fabricante
             .Select(r => new DetalleRepararDTO(
                 r.Id,
                 r.FechaEntrega,
                 r.FechaRecogida,
                 r.PrecioTotal,
                 r.ApplicationUser.Name,
                 r.ApplicationUser.Surname,
                 r.ReparacionItems
                    .Select(ri => new RepararItemDTO(
                        ri.Herramienta.Id,
                        ri.Herramienta.Nombre,
                        ri.Precio,
                        ri.Descripcion,
                        ri.Cantidad)
                    ).ToList<RepararItemDTO>()))
             .FirstOrDefaultAsync();


            if (reparacion == null)
            {
                _logger.LogError($"Error: Rental with id {id} does not exist");
                return NotFound();
            }


            return Ok(reparacion);
        }
    }
}
