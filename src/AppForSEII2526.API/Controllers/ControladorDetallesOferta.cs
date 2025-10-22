using AppForSEII2526.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppForSEII2526.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControladorDetallesOferta : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ControladorDetallesOferta> _logger;

        public ControladorDetallesOferta(ApplicationDbContext context, ILogger<ControladorDetallesOferta> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(IList<DetalleOfertaDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetDetallesdeOfertasCreadas(int id)
        {
            if (_context.Compras == null)
            {
                _logger.LogError("No se encontraron compras en la base de datos.");
                return NotFound();
            }

            var oferta = await _context.Ofertas
                .Where(o => o.Id == id)
                .Include(o => o.ApplicationUser)
                .Include(o => o.OfertaItems)
                    .ThenInclude(oi => oi.Herramienta)
                        .ThenInclude(h => h.Fabricante)
                .Select(o => new DetalleOfertaDTO(

                    o.FechaInicio,
                    o.FechaFinal,
                    o.TiposMetodoPago.ToString(),
                    o.TiposDirigdaOferta.ToString(),
                    o.OfertaItems.Select(oi => new OfertaItemDTO(
                        oi.Herramienta.Nombre,
                        oi.Herramienta.Material,
                        oi.Herramienta.Fabricante.Nombre,
                        oi.Herramienta.Precio,
                        oi.PrecioFinal,
                        oi.Herramienta.Id
                    )).ToList(),
                    o.FechaOferta,
                    o.Id
                ))
                .FirstOrDefaultAsync();

            if (oferta == null)
            {
                _logger.LogError("No se encontraron detalles de oferta para el ID proporcionado: {Id}", id);
                return NotFound();
            }
            return Ok(oferta);
        }
    }
}
