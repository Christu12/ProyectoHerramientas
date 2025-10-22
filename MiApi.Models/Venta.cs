using System;
using System.Collections.Generic;
using System.Text;

namespace MiApi.Models
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public int? IdUsuario { get; set; }
    }
}
