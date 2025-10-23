using System;
using System.Collections.Generic;
using System.Text;
using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;

namespace MiApi.Models
{
    [Table("dbo.Producto")]
    public class Producto
    {
        [Dapper.Contrib.Extensions.Key]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El tipo de producto es obligatorio.")]
        public string Tipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(1, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
        public decimal Precio { get; set; } = 0;

        [Required(ErrorMessage = "El stock es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
        public int Stock { get; set; } = 0;
    }
}
