using MiApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiApi.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<int> Add(Usuario u);
        Task<bool> Update(Usuario u);
        Task<bool> Delete(int id);
    }
}
