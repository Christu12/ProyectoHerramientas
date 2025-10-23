using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using MiApi.Models;
using MiApi.Repository.Interfaces;

namespace MiApi.Repository.Implements
{
    public class DetalleVentaRepository : IDetalleVentaRepository
    {
        private readonly IDbConnection _db;

        public DetalleVentaRepository(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<int> Add(DetalleVenta d)
        {
            try
            {
                var id = await _db.InsertAsync(d);
                return id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                await _db.ExecuteAsync("DELETE FROM DetalleVenta WHERE IdDetalle = @id", new { id });
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(DetalleVenta d)
        {
            try
            {
                return await _db.UpdateAsync(d);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
