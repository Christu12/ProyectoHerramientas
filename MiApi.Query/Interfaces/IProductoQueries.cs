using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MiApi.Models;

namespace MiApi.Query.Interfaces
{
    public interface IProductoQueries
    {
        Task<IEnumerable<Producto>> GetAll();
    }
}
