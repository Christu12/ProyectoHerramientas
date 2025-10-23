using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;

namespace MiApi.Models
{
    [Table("dbo.Usuario")]
    public class Usuario
    {
        [Dapper.Contrib.Extensions.Key]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ser un correo electrónico válido.")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "La contraseña debe tener al menos 4 caracteres.")]
        public string Contraseña { get; set; } = string.Empty;

        [Required(ErrorMessage = "El rol es obligatorio.")]
        [RegularExpression("^(Cliente|Trabajador|Admin)$", ErrorMessage = "El rol solo puede ser Cliente, Trabajador o Admin.")]
        public string Rol { get; set; } = string.Empty;

        
    }
}
