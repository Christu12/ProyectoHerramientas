using MiApi.Models;
using MiApi.Query.Interfaces;
using MiApi.Repository.Implements;
using MiApi.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MiApi.Controllers
{
    /// <summary>
    /// Controlador para las acciones del detalle de la venta
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleVentaController : ControllerBase
    {
        private readonly IDetalleVentaQueries _queries;
        private readonly IDetalleVentaRepository _repository;

        public DetalleVentaController(IDetalleVentaQueries queries, IDetalleVentaRepository repository)
        {
            _queries = queries;
            _repository = repository;
        }

        /// <summary>
        /// Obtiene todos los detalles de venta registrados en el sistema.
        /// </summary>
        /// <response code="200">Lista de detalles de venta obtenida correctamente</response>
        /// <response code="500">Error al procesar la solicitud</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<DetalleVenta>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var detalles = await _queries.GetAll();
                return Ok(detalles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener detalles de venta: {ex.Message}");
            }
        }

        /// <summary>
        /// Busca un detalle de venta específico por su Id.
        /// </summary>
        /// <param name="id">Id del detalle de venta</param>
        /// <response code="200">Detalle de venta encontrado correctamente</response>
        /// <response code="404">No se encontró el detalle de venta solicitado</response>
        /// <response code="500">Error al procesar la solicitud</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DetalleVenta), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var detalle = await _queries.GetById(id);
                if (detalle == null)
                    return NotFound($"No existe un detalle con id {id}");
                return Ok(detalle);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener detalle de venta: {ex.Message}");
            }
        }

        /// <summary>
        /// Registra un nuevo detalle de venta (asociado a una venta).
        /// </summary>
        /// <param name="detalle">Objeto DetalleVenta que debe contener IdProducto, IdVenta y Cantidad.</param>
        /// <response code="201">Detalle de venta creado correctamente.</response>
        /// <response code="400">Datos inválidos en la petición.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPost]
        [ProducesResponseType(typeof(DetalleVenta), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add(DetalleVenta detalle)
        {
            if (detalle == null)
                return BadRequest("Los datos del detalle no pueden estar vacíos.");

            try
            {
                var id = await _repository.Add(detalle);
                detalle.IdDetalle = id;
                return CreatedAtAction(nameof(GetById), new { id = detalle.IdDetalle },
                    $"Detalle de venta con id {detalle.IdDetalle} registrado correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al registrar detalle: {ex.Message}");
            }
        }
    }
}