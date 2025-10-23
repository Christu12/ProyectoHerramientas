using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MiApi.Models;

namespace MiApi.Query.Interfaces
{
    public interface IVentaQueries
    {
        Task<IEnumerable<Venta>> GetAll();
        Task<Venta?> GetById(int id);     
    }
}
