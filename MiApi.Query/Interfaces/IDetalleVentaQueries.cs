using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MiApi.Models;

namespace MiApi.Query.Interfaces
{
    public interface IDetalleVentaQueries
    {
        Task<IEnumerable<DetalleVenta>> GetAll();
        Task<DetalleVenta> GetById(int id);
    }
}
