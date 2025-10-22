using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControladorDetallesAlquiler : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<ControladorDetallesAlquiler> _logger;

        public ControladorDetallesAlquiler(ApplicationDbContext context, ILogger<ControladorDetallesAlquiler> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<DetalleAlquilarDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetDetallesdeHerramientasAlquiladas(int id)
        {
            if (_context.Alquileres == null)
            {
                _logger.LogError("No se encontraron alquileres en la base de datos.");
                return NotFound();
            }

            var alquileres = await _context.Alquileres
            .Where(c => c.Id == id)
                .Include(c => c.ApplicationUser)
                .Include(c => c.AlquilarItems)
                    .ThenInclude(ci => ci.Herramienta)
                .Select(c => new DetalleAlquilarDTO(c.Id, c.ApplicationUser.Name, c.ApplicationUser.Surname, c.DireccionEnvio, c.FechaAlquiler, c.PrecioTotal, c.FechaInicio, c.FechaFin, c.AlquilarItems
                    .Select(ci => new AlquilarItemDTO(ci.Herramienta.Id, ci.Herramienta.Nombre, ci.Herramienta.Material, ci.Precio, ci.Cantidad)).ToList<AlquilarItemDTO>()))
                .FirstOrDefaultAsync();

            if (alquileres == null)
            {
                _logger.LogError("No se encontraron detalles de alquilar para el ID proporcionado: {Id}", id);
                return NotFound();
            }
            return Ok(alquileres);

        }
    }
}