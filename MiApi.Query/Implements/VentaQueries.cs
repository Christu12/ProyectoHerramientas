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
    public class VentaQueries : IVentaQueries
    {
        private readonly IDbConnection _db;

        public VentaQueries(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task<IEnumerable<Venta>> GetAll()
        {   
            string sql = "SELECT * FROM Venta";
            return await _db.QueryAsync<Venta>(sql);
        }
        public async Task<Venta?> GetById(int id)
        {
            string sql = "SELECT * FROM Venta WHERE IdVenta = @id";
            return await _db.QueryFirstOrDefaultAsync<Venta>(sql, new { id });
        }
    }
}

