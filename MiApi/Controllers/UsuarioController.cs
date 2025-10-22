using MiApi.Models;
using MiApi.Query.Interfaces;
using MiApi.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MiApi.Controllers
{
    /// <summary>
    /// Controlador para las acciones de personas
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuarioQueries _usuarioQueries;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(
            ILogger<UsuarioController> logger, IUsuarioQueries usuarioQueries,
            IUsuarioRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _usuarioQueries = usuarioQueries ?? throw new ArgumentException(nameof(usuarioQueries));
            _logger.LogInformation("Entrando al constructor");
            _usuarioRepository = repository ?? throw new ArgumentException(nameof(repository));
        }

        /// <summary>
        /// Metodo que lista todos los usuarios
        /// </summary>
        /// <response code="200">Lista de usuarios</response>
        /// <response code="500">Error procesando la peticion</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Usuario>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarPersona()
        {
            
            var rsDapper = await _usuarioQueries.GetAll();
            return Ok(rsDapper);
        }

        /// <summary>
        /// Buscar usuario por id
        /// </summary>
        /// <param name="id">Id usuario a buscar</param>
        /// <response code="200">Cuando se encuentra el usuario</response>
        /// <response code="404">El Usuario no existe</response>
        /// <response code="500">Error procesando la peticion</response>

        [HttpGet("ById/{id}")]
        [ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
     

        /// <summary>
        /// Add persona con dapper
        /// </summary>
        /// <param name="p">Body</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Crear(Usuario u)
        {
            try
            {
                var rs = await _usuarioRepository.Add(u);
                u.IdUsuario = rs;
                return Ok(u);
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Borrar persona
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Borrar(int id)
        {
            
            {
                bool rs = await _usuarioRepository.Delete(id);
                if (!rs)
                    return NotFound($"No se encontró el usuario con ID {id}.");

                return Ok($"Usuario con ID {id} eliminado correctamente.");
            }
            
        }


        /// <summary>
        /// Actualizar persona
        /// </summary>
        /// <param name="p"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar([FromBody] Usuario u, [FromRoute] int id)
        {
            try
            {
                u.IdUsuario = id;
                bool rs = await _usuarioRepository.Update(u);
                return Ok(u);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
