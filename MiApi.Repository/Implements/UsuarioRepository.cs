using Dapper;
using Dapper.Contrib.Extensions;
using MiApi.Models;
using MiApi.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MiApi.Repository.Implements
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnection _db;

        public UsuarioRepository(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task<int> Add(Usuario u)
        {
            try
            {
                var id = await _db.InsertAsync(u);
                return id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var usuario = await _db.GetAsync<Usuario>(id);
            if (usuario == null) return false;

            return await _db.DeleteAsync(usuario);
        }

        public async Task<bool> Update(Usuario u)
        {
            try
            {
               return await _db.UpdateAsync(u);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
