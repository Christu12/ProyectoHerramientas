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
    public class ProductoRepository : IProductoRepository
    {
        private readonly IDbConnection _db;

        public ProductoRepository(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<int> Add(Producto p)
        {
            var id = await _db.InsertAsync(p);
            return id;
        }
        public async Task<bool> Update(Producto p)
        {
           return await _db.UpdateAsync(p);
           
        }
        public async Task<bool> Delete(int id)
        {
            var producto = await _db.GetAsync<Producto>(id);
            if (producto == null) return false;
            return await _db.DeleteAsync(producto);
        }
    }
}
