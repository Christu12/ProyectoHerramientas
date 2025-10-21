using Dapper;
using MiApi.Models;
using MiApi.Query.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MiApi.Query.Implements
{
    public class UsuarioQueries : IUsuarioQueries
    {
        private readonly IDbConnection _db;

        public UsuarioQueries(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<IEnumerable<Usuario>> GetAll()
        {
            try
            {
                string sql = "SELECT * FROM Personas";
                var rs = await _db.QueryAsync<Usuario>(sql);
                return rs;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
