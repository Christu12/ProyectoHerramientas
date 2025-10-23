using MiApi.Models;
using MiApi.Query.Interfaces;
using MiApi.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace MiApi.Controllers
{
    /// <summary>
    /// Controlador para las acciones de venta
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : ControllerBase
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IVentaQueries _ventaQueries;

        public VentaController(IVentaRepository ventaRepository, IVentaQueries ventaQueries)
        {
            _ventaRepository = ventaRepository;
            _ventaQueries = ventaQueries;
        }

        /// <summary>
        /// Obtiene todas las ventas registradas en el sistema.
        /// </summary>
        /// <response code="200">Retorna la lista de ventas.</response>
        /// <response code="500">Error al obtener los datos desde la base de datos.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Venta>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var ventas = await _ventaQueries.GetAll();
                return Ok(ventas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las ventas: {ex.Message}");
            }
        }

        /// <summary>
        /// Busca una venta específica por su ID.
        /// </summary>
        /// <param name="id">Id de la venta a buscar.</param>
        /// <response code="200">Venta encontrada y devuelta correctamente.</response>
        /// <response code="404">No existe una venta con ese ID.</response>
        /// <response code="500">Error interno al procesar la solicitud.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Venta), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var venta = await _ventaQueries.GetById(id);
                if (venta == null)
                    return NotFound($"No existe una venta con el id {id}");

                return Ok(venta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al buscar la venta: {ex.Message}");
            }
        }

        /// <summary>
        /// Registra una nueva venta en el sistema.
        /// </summary>
        /// <param name="venta">Datos de la venta a registrar.</param>
        /// <response code="201">Venta registrada correctamente.</response>
        /// <response code="400">Los datos enviados no son válidos.</response>
        /// <response code="500">Error interno al procesar la solicitud.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Venta), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Add(Venta venta)
        {
            if (venta == null)
                return BadRequest("Los datos de la venta no pueden estar vacíos.");

            try
            {
                var id = await _ventaRepository.Add(venta);
                venta.IdVenta = id;

                return CreatedAtAction(nameof(GetById), new { id = venta.IdVenta },
                    $"La venta con id {venta.IdVenta} ha sido registrada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al registrar la venta: {ex.Message}");
            }
        }
    }
}