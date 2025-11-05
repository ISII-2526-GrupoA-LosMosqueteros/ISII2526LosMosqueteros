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
                    o.TiposMetodoPago,
                    o.TiposDirigdaOferta,
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

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(DetalleOfertaDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreacionOferta(CreacionOfertaDTO creaciondeoferatas)
        {
            if (creaciondeoferatas.FechaInicio <= DateTime.Today)
                ModelState.AddModelError("FechaInicio", "Error! La fecha de inicio de tu oferta debe ser posterior a hoy");

            if (creaciondeoferatas.FechaInicio >= creaciondeoferatas.FechaFinal)
                ModelState.AddModelError("FechaInicio&FechaFinal", "Error! Tu oferta debe terminar después de que empiece");

            if (creaciondeoferatas.FechaInicio == DateTime.MinValue)
                ModelState.AddModelError("FechaInicio", "Error! Fecha Inicio es un campo obligatorio");

            if (creaciondeoferatas.OfertaItem.Count() == 0 || creaciondeoferatas.OfertaItem == null)
                ModelState.AddModelError("OfertaItems", "Error! Tienes que incluir al menos una herramienta para aplicar una oferta");

            if (creaciondeoferatas.FechaFinal == DateTime.MinValue)
                ModelState.AddModelError("FechaFinal", "Error! Fecha Final es un campo obligatorio");

            if (creaciondeoferatas.TiposMetodoPago == null)
                ModelState.AddModelError("TiposMetodoPago", "Error! El tipo de método de pago es un campo obligatorio");
           
            //Si se ha producido alguno de los errores anteriores, terminamos la ejecucion del metodo 
            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));


            var herramientasnombres = creaciondeoferatas.OfertaItem.Select(n => n.Nombre).ToList<string>();

            var herramientas = _context.Herramientas
                .Include(f => f.Fabricante) 
                .Where(h => herramientasnombres.Contains(h.Nombre))
                .ToList();

            Oferta oferta = new Oferta
            {
                FechaInicio = creaciondeoferatas.FechaInicio,
                FechaFinal = creaciondeoferatas.FechaFinal,
                TiposMetodoPago = creaciondeoferatas.TiposMetodoPago,
                TiposDirigdaOferta = creaciondeoferatas.TiposDirigdaOferta,
                FechaOferta =  DateTime.Now,
                OfertaItems = new List<OfertaItem>()
            };

            
            foreach (var item in creaciondeoferatas.OfertaItem)
            {
                var herramienta = herramientas.FirstOrDefault(h => h.Nombre == item.Nombre);

                if (creaciondeoferatas.Porcentaje < 0 || creaciondeoferatas.Porcentaje > 100)
                    ModelState.AddModelError("Porcentaje", "Error: Introduce un valor entre 0 y 100");
                else
                {
                    decimal precioFinal = herramienta.Precio * (1 - (creaciondeoferatas.Porcentaje / 100m));
                    oferta.OfertaItems.Add(new OfertaItem { Porcentaje = creaciondeoferatas.Porcentaje, PrecioFinal = precioFinal, Oferta = oferta, Herramienta = herramienta });
                }
            }

            if (ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));

            _context.Ofertas.Add(oferta);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Oferta", $"Error! Ha habido un problema al guardar la nueva Oferta");
                return Conflict("Error" + ex.Message);

            }

            var ofertaCreada = new DetalleOfertaDTO(
                oferta.FechaInicio,
                oferta.FechaFinal,
                oferta.TiposMetodoPago,
                oferta.TiposDirigdaOferta,
                oferta.OfertaItems.Select(oi => new OfertaItemDTO(
                    oi.Herramienta.Nombre,
                    oi.Herramienta.Material,
                    oi.Herramienta.Fabricante.Nombre,
                    oi.Herramienta.Precio,
                    oi.PrecioFinal,
                    oi.HerramientaId
                )).ToList(),
                oferta.FechaOferta,
                oferta.Id
            );

            return CreatedAtAction("GetDetallesdeOfertasCreadas", new { id = oferta.Id }, ofertaCreada);
        }


    }
}
