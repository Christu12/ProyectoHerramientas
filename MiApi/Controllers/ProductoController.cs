using MiApi.Models;
using MiApi.Query.Interfaces;
using MiApi.Repository.Implements;
using MiApi.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MiApi.Controllers
{
    /// <summary>
    /// Controlador para las acciones de productos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ILogger<ProductoController> _logger;
        private readonly IProductoQueries _productoQueries;
        private readonly IProductoRepository _productoRepository;
        public ProductoController(
            ILogger<ProductoController> logger, IProductoQueries productoQueries,
            IProductoRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _productoQueries = productoQueries ?? throw new ArgumentException(nameof(productoQueries));
            _logger.LogInformation("Entrando al constructor");
            _productoRepository = repository ?? throw new ArgumentException(nameof(repository));
        }
        /// <summary>
        /// Obtiene la lista completa de productos registrados.
        /// </summary>
        /// <response code="200">Lista de productos obtenida correctamente.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [ProducesResponseType(typeof(List<Producto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarProducto()
        {
            var rsDapper = await _productoQueries.GetAll();
            return Ok(rsDapper);
        }




        /// <summary>
        /// Crea un nuevo producto en el sistema.
        /// </summary>
        /// <param name="p">Objeto del producto a registrar. Debe contener nombre, descripción, tipo, precio y stock.</param>
        /// <response code="201">Producto creado correctamente.</response>
        /// <response code="400">Los datos enviados no son válidos.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AñadirProducto(Producto p)
        {
            try
            {
                if (p == null)
                    return BadRequest("El cuerpo de la solicitud no puede estar vacío.");
                var rs = await _productoRepository.Add(p);
                p.IdProducto = rs;
                return Ok(p);
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Actualiza los datos de un producto existente.
        /// </summary>
        /// <param name="id">Identificador del producto a actualizar.</param>
        /// <param name="p">Datos actualizados del producto.</param>
        /// <response code="200">Producto actualizado correctamente.</response>
        /// <response code="400">Los datos enviados no son válidos o el ID no coincide.</response>
        /// <response code="404">No se encontró el producto con el ID especificado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Actualizar([FromBody] Producto p, [FromRoute] int id)
        {
            try
            {
                p.IdProducto = id;
                bool rs = await _productoRepository.Update(p);
                return Ok(p);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        /// <summary>
        /// Elimina un producto del sistema por su ID.
        /// </summary>
        /// <param name="id">Identificador del producto a eliminar.</param>
        /// <response code="200">Producto eliminado correctamente.</response>
        /// <response code="404">No se encontró el producto con el ID especificado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Borrar(int id)
        {
            try
            {
                bool rs = await _productoRepository.Delete(id);
                if (!rs)
                {
                    return NotFound($"No se encontro el id: {id}.");
                }
                return Ok($"El producto con id {id} se elimino correctamente");
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
    }
}
