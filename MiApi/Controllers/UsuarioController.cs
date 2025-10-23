using MiApi.Models;
using MiApi.Query.Interfaces;
using MiApi.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

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
        /// Buscar usuario por id
        /// </summary>
        /// <param name="id">Id del usuario a buscar</param>
        /// <response code="200">Cuando se encuentra el usuario</response>
        /// <response code="404">El usuario no existe</response>
        /// <response code="500">Error procesando la petición</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarPersona()
        {
            
            var rsDapper = await _usuarioQueries.GetAll();
            return Ok(rsDapper);
        }






        /// <summary>
        /// Crea un nuevo usuario en el sistema.
        /// </summary>
        /// <param name="u">Datos del usuario a registrar.</param>
        /// <response code="201">Usuario creado correctamente.</response>
        /// <response code="400">Datos inválidos en la solicitud.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Crear(Usuario u)
        {
            try
            {
                if (u == null)
                    return BadRequest("El cuerpo de la solicitud no puede estar vacío.");
                var rs = await _usuarioRepository.Add(u);
                u.IdUsuario = rs;
                return CreatedAtAction(nameof(ListarPersona), new { id = u.IdUsuario }, new {Message =$"El usuario con id {u.IdUsuario} ha sido creado"});
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error con el servidor: {ex.Message}");
            }
        }


        /// <summary>
        /// Elimina un usuario del sistema por su ID.
        /// </summary>
        /// <param name="id">ID del usuario a eliminar.</param>
        /// <response code="200">Usuario eliminado correctamente.</response>
        /// <response code="404">No se encontró el usuario con el ID especificado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        /// Actualiza los datos de un usuario existente.
        /// </summary>
        /// <param name="id">ID del usuario a actualizar.</param>
        /// <param name="u">Datos actualizados del usuario.</param>
        /// <response code="200">Usuario actualizado correctamente.</response>
        /// <response code="400">Los datos enviados no son válidos.</response>
        /// <response code="404">No se encontró el usuario con el ID especificado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        /// <summary>
        /// Buscar usuario por id
        /// </summary>
        /// <param name="id">Id usuario a buscar</param>
        /// <response code="200">Cuando se encuentra el usuario</response>
        /// <response code="404">El Usuario no existe</response>
        /// <response code="500">Error procesando la peticion</response>
    }
}
