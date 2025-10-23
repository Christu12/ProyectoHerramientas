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
    public class DetalleVentaQueries : IDetalleVentaQueries
    {
        private readonly IDbConnection _db;

        public DetalleVentaQueries(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task<IEnumerable<DetalleVenta>> GetAll()
        {
            var sql = "SELECT * FROM DetalleVenta";
            return await _db.QueryAsync<DetalleVenta>(sql);
        }

        public Task<DetalleVenta?> GetById(int id)
        {
            var sql = "SELECT * FROM DetalleVenta WHERE IdDetalle = @id";
            return _db.QueryFirstOrDefaultAsync<DetalleVenta>(sql, new { id });
        }
    }
}
