using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.Models;
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

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(DetalleRepararDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CrearReparacion(CreacionReparacionDTO creacionReparacion)
        {
            // Comprobamos validaciones
            if(creacionReparacion.FechaEntrega < DateTime.Today)
            {
                ModelState.AddModelError("FechaEntrega", "La fecha de recogida no puede ser anterior a hoy.");
                return ValidationProblem(ModelState);
            }

            //preguntar que es un reparacionItem
            if (creacionReparacion.RepararItem.Count == 0)
            {
                ModelState.AddModelError("RepararItem", "La reparacion debe contener al menos un item a reparar.");
            }

            var usuario = _context.ApplicationUsers.FirstOrDefault(au => au.Name == creacionReparacion.Name);
            if (usuario == null)
                ModelState.AddModelError("ApplicationUsers", "Error! El usuario no está registrado");

            if(ModelState.ErrorCount > 0)
                return BadRequest(new ValidationProblemDetails(ModelState));
            
            var nombreHerramientas = creacionReparacion.RepararItem.Select(ri => ri.Nombre).ToList();

            
            var herramientas = _context.Herramientas
                .Include(f => f.Fabricante)
                .Where(h => nombreHerramientas.Contains(h.Nombre))
                .ToList();

            Reparacion reparacion = new Reparacion
            {
                ApplicationUser = usuario,
                TiposMetodoPago = creacionReparacion.TiposMetodoPago,
                ReparacionItems = new List<ReparacionItem>(),
                FechaEntrega = creacionReparacion.FechaEntrega
            };

            reparacion.PrecioTotal = 0m;

            int numDiasReparacion = 0;

            foreach (var item in creacionReparacion.RepararItem)
            {
                var herr = herramientas.FirstOrDefault(h => h.Nombre == item.Nombre);
                if(herr == null)
                {
                    ModelState.AddModelError("Herramienta", $"La herramienta {item.Nombre} no existe.");
                }
                else
                {
                    string descripcion = null;
                    if (item.Descripcion.Length > 0)
                    {
                        descripcion = item.Descripcion;
                    }
                    
                    if(herr.TiempoReparacion > numDiasReparacion)
                    {
                        numDiasReparacion = herr.TiempoReparacion;
                    }
                    reparacion.ReparacionItems.Add(new ReparacionItem
                    {
                        Precio = herr.Precio * item.Cantidad,
                        Descripcion = descripcion,
                        Cantidad = item.Cantidad,
                        Herramienta = herr,
                        Reparacion = reparacion

                    });
                }
            }

            reparacion.PrecioTotal = reparacion.ReparacionItems.Sum(ri => ri.Precio);
            reparacion.FechaRecogida = reparacion.FechaEntrega.AddDays(numDiasReparacion);

            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }

            _context.Add(reparacion);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                ModelState.AddModelError("Reparacion", $"Error! ha habido un error guardando tu reparacion, por favor prueba mas tarde.");
                return Conflict("Error" + ex.Message);
            }


            var detalleReparacion = new DetalleRepararDTO(
                reparacion.Id,
                reparacion.FechaEntrega,
                reparacion.FechaRecogida,
                reparacion.PrecioTotal,
                usuario.Name,
                usuario.Surname,
                reparacion.ReparacionItems
                    .Select(ri => new RepararItemDTO(
                        ri.Herramienta.Id,
                        ri.Herramienta.Nombre,
                        ri.Precio,
                        ri.Descripcion,
                        ri.Cantidad)
                    ).ToList()
                );

            return CreatedAtAction("CrearReparacion", new { id = reparacion.Id }, detalleReparacion);
            







        }
    }
}
