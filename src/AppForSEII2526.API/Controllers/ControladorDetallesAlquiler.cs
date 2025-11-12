using AppForSEII2526.API.DTOs;
using AppForSEII2526.API.DTOs.AlquilerDTOs;
using AppForSEII2526.API.Models;
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

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(DetalleAlquilarDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Conflict)]
        public async Task<ActionResult> CreacionAlquiler(CreacionAlquilerDTO creacionAlquiler)
        {
            if (creacionAlquiler.FechaInicio < DateTime.Today)
            {
                ModelState.AddModelError("Fecha Inicio", "¡Error! Tu alquiler no debe empezar antes que hoy");
            }

            if (creacionAlquiler.FechaInicio > creacionAlquiler.FechaFin)
            {
                ModelState.AddModelError("FechaInicio&FechaFin", "¡Error! Tu alquiler debe acabar después de cuando empezó");
            }
            
            if (creacionAlquiler.AlquilerItems.Count == 0)
            {
                ModelState.AddModelError("AlquilarItems", "¡Error! Tienes que incluir al menos una herramienta para alquilar");
            }

            var usuario = _context.ApplicationUsers.FirstOrDefault(au => au.Name == creacionAlquiler.Name);
            if (usuario == null)
            {
                ModelState.AddModelError("AplicacionAlquilerUsuario", "¡Error! Usuario no registrado");
            }

            if (creacionAlquiler.Name == null)
            {
                ModelState.AddModelError("Nombre", "¡Error! El nombre es un campo obligatorio");
            }

            if (creacionAlquiler.Surname == null)
            {
                ModelState.AddModelError("Apellidos", "¡Error! Los apellidos son un campo obligatorio");
            }

            if (creacionAlquiler.DireccionEnvio == null)
            {
                ModelState.AddModelError("DireccionEnvio", "¡Error! La direccion de envio es un campo obligatorio");
            }

          

            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
            var nombreHerramientas = creacionAlquiler.AlquilerItems.Select(ri => ri.Nombre).ToList<String>();
            var herramientas = _context.Herramientas
                .Where(h => nombreHerramientas.Contains(h.Nombre))
                .ToList();

            Alquiler alquiler = new Alquiler
            {
                ApplicationUser = usuario,
                DireccionEnvio = creacionAlquiler.DireccionEnvio,
                TiposMetodoPago = creacionAlquiler.MetodoPago,
                FechaAlquiler = DateTime.Today,
                FechaFin = creacionAlquiler.FechaFin,
                FechaInicio = creacionAlquiler.FechaInicio,
                AlquilarItems = new List<AlquilarItem>()
            };

            alquiler.PrecioTotal = 0;
            var numeroDias = (decimal) (creacionAlquiler.FechaFin - creacionAlquiler.FechaInicio).TotalDays;

            foreach (var item in creacionAlquiler.AlquilerItems)
            {
                if (item.Cantidad <= 0)
                {
                    ModelState.AddModelError("Cantidad", "La cantidad debe ser mayor que cero.");
                }
                var herramienta = herramientas.FirstOrDefault(h => h.Nombre == item.Nombre);
                if (herramienta == null)
                {
                    ModelState.AddModelError("Herramienta", $"La herramienta '{item.Nombre}' no existe.");
                }
                else
                {
                    alquiler.PrecioTotal += (herramienta.Precio * item.Cantidad)*numeroDias;
                    alquiler.AlquilarItems.Add(new AlquilarItem
                    {
                        HerramientaId = herramienta.Id,
                        Cantidad = item.Cantidad,
                        Precio = item.Precio,
                        Herramienta = herramienta,
                        Alquiler = alquiler
                    });
                }
            }
            alquiler.PrecioTotal = alquiler.AlquilarItems.Sum(ci => ci.Precio);

            if (ModelState.ErrorCount > 0)
            {
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
            
            _context.Alquileres.Add(alquiler);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                ModelState.AddModelError("Error al guardar el alquiler", "Ocurrió un error al guardar el alquiler en la base de datos.");
                return Conflict("Error" + ex.Message);
            }

            var detallesAlquiler = new DetalleAlquilarDTO(
                 alquiler.Id,
                 alquiler.ApplicationUser.Name,
                 alquiler.ApplicationUser.Surname,
                 alquiler.DireccionEnvio,
                 alquiler.FechaAlquiler,
                 alquiler.PrecioTotal,
                 alquiler.FechaInicio,
                 alquiler.FechaFin,
                 alquiler.AlquilarItems.Select(h => new AlquilarItemDTO(h.Herramienta.Id, h.Herramienta.Nombre, h.Herramienta.Material, h.Precio, h.Cantidad)).ToList()

     );

            return CreatedAtAction("GetDetallesdeHerramientasAlquiladas", new { id = alquiler.Id }, detallesAlquiler);


        }
    }
}