using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MiApi.Models;

namespace MiApi.Repository.Interfaces
{
    public interface IVentaRepository
    {
        Task<int> Add(Venta v);
    }
}
