using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppForSEII2526.API.DTOs;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HerramientasController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<HerramientasController> _logger;

        public HerramientasController(ApplicationDbContext context, ILogger<HerramientasController> logger)
        {
            _context = context;
            _logger = logger;
        }

      
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

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientasParaComprarDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientasParaComprar(decimal? filtroPrecio, string? filtroMaterial)
        {
            var herramientas = await _context.Herramientas
                .Include(h => h.Fabricante)
                .Where(h => (h.Precio <= filtroPrecio || filtroPrecio == null)
                    && (h.Material.Contains(filtroMaterial) || filtroMaterial == null)
                 )
                .OrderBy(h => h.Nombre)
                .Select(h => new HerramientasParaComprarDTO(h.Id, h.Nombre, h.Material, h.Precio, h.Fabricante.Nombre))
                .ToListAsync();
            return Ok(herramientas);

        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<HerramientasParaOfertaDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetHerramientasParaOferta(decimal? precio, string? fabricante)
        {
            var herramientas = await _context.Herramientas
                .Include(h => h.Fabricante)
                .Where(h =>
                    (precio == null || h.Precio <= precio) &&
                    (fabricante == null || h.Fabricante.Nombre.Contains(fabricante))
                )
                .Select(h => new HerramientasParaOfertaDTO
                (h.Id, h.Nombre, h.Material, h.Fabricante.Nombre, h.Precio))
                .ToListAsync();
            return Ok(herramientas);
        }
    }
}
