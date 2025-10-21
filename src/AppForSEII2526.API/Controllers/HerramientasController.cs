using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppForSEII2526.API.DTOs;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HerramientasController : ControllerBase
    {
        //used to enable your controller to access to the database
        private readonly ApplicationDbContext _context;
        //used to log any information when your system is running
        private readonly ILogger<HerramientasController> _logger;

        public HerramientasController(ApplicationDbContext context, ILogger<HerramientasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Devuelve la información usando DTOs para el caso de reparar una herramienta CU2
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientaParaRepararDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientasParaReparar(string? filtroNombre, int? filtroTiempoReparacion)
        {
            var herramientas = await _context.Herramientas
                .Include(h => h.Fabricante)
                .Where(h => (h.Nombre.Contains(filtroNombre) || filtroNombre == null)
                    && (h.TiempoReparacion <= filtroTiempoReparacion || filtroTiempoReparacion == null))
                .OrderBy(h => h.TiempoReparacion)
                .Select(h => new HerramientaParaRepararDTO(
                    h.Id,
                    h.Nombre,
                    h.Material,
                    h.Precio,
                    h.TiempoReparacion,
                    h.Fabricante.Nombre
                )).ToListAsync();
            return Ok(herramientas);
        }


    }
}