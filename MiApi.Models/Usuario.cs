using Dapper.Contrib.Extensions;

namespace MiApi.Models
{
    [Table("dbo.Usuario")]
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Contraseña { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;

        [Computed]
        public string DatosCompletos => $"({IdUsuario}){Nombre}";
    }
}
