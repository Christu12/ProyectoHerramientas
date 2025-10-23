using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using MiApi.Models;
using MiApi.Repository.Interfaces;

namespace MiApi.Repository.Implements
{
    public class VentaRepository : IVentaRepository
    {
        private readonly IDbConnection _db;
        public VentaRepository(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public async Task<int> Add(Venta v)
        {
            try
            {
                var id = await _db.InsertAsync(v);
                return (int)id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar la venta", ex);
            }
        }
    }
}
