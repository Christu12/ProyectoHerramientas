using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;

namespace MiApi.Models
{
    [Table("dbo.Venta")]
    public class Venta
    {
        [Dapper.Contrib.Extensions.Key]
        public int IdVenta { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        
        public int? IdUsuario { get; set; }
    }
}
