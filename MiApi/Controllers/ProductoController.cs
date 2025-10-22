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
        /// Metodo que lista todos los productos
        /// </summary>
        /// <response code="200">Lista de productos</response>
        /// <response code="500">Error procesando la peticion</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Producto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarProducto()
        {
            var rsDapper = await _productoQueries.GetAll();
            return Ok(rsDapper);
        }




        /// <summary>
        /// Metodo Que crea o añade los productos
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AñadirProducto(Producto p)
        {
            try
            {
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
        /// Metodo para actualizar los productos
        /// </summary>
        /// <param name="p"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
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
        /// Metodo para eliminar productos mediante el id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
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
