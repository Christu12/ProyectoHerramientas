using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;


namespace MiApi.Models
{
    [Table("dbo.DetalleVenta")]
    public class DetalleVenta
    {
        [Dapper.Contrib.Extensions.Key]
        public int IdDetalle { get; set; }

        [Required(ErrorMessage = "El ID de la venta es obligatorio.")]
        public int IdVenta { get; set; }

        [Required(ErrorMessage = "El ID del producto es obligatorio.")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "Debe especificar la cantidad.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0.")]
        public int Cantidad { get; set; }
    }
}
