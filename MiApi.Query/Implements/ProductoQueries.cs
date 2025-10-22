using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MiApi.Models;
using MiApi.Query.Interfaces;

namespace MiApi.Query.Implements
{
    public class ProductoQueries : IProductoQueries
    {
        private readonly IDbConnection _db;

        public ProductoQueries(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task<IEnumerable<Producto>> GetAll()
        {
            string sql = "SELECT * FROM Producto";
            return await _db.QueryAsync<Producto>(sql);
        }
    }
}
