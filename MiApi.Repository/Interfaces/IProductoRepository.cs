using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MiApi.Models;

namespace MiApi.Repository.Interfaces
{
    public interface IProductoRepository
    {
        Task<int> Add(Producto p);
        Task<bool> Update(Producto p);
        Task<bool> Delete(int id);
    }
}
