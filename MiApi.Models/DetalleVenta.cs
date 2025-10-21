using System;
using System.Collections.Generic;
using System.Text;

namespace MiApi.Models
{
    internal class DetalleVenta
    {
        public int IdDetalle { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
    }
}
