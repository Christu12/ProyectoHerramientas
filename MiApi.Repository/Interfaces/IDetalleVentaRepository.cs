using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MiApi.Models;

namespace MiApi.Repository.Interfaces
{
    public interface IDetalleVentaRepository
    {
        Task<int> Add(DetalleVenta d);
        Task<bool> Update(DetalleVenta d);
        Task<bool> Delete(int id);
    }
}
