using Kompiuteriu_Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kompiuteriu_Web.Data.Repositories
{
    public interface IShopsRepository
    {
        Task DeleteAsync(Shop shop);
        Task<List<Shop>> GetAllAsync();
        Task<Shop> GetAsync(int shopId);
        Task InsertAsync(Shop shop);
        Task UpdateAsync(Shop shop);
    }

    public class ShopsRepository : IShopsRepository
    {
        private readonly RestContext _restContext;
        public ShopsRepository(RestContext restContext)
        {
            _restContext = restContext;
        }

        public async Task<Shop> GetAsync(int shopId)
        {
            return await _restContext.Shops.FirstOrDefaultAsync(shop => shop.id == shopId);
        }

        public async Task<List<Shop>> GetAllAsync()
        {
            return await _restContext.Shops.ToListAsync();
        }

        public async Task InsertAsync(Shop shop)
        {
            _restContext.Shops.Add(shop);
            await _restContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Shop shop)
        {
            _restContext.Shops.Update(shop);
            await _restContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Shop shop)
        {
            _restContext.Shops.Remove(shop);
            await _restContext.SaveChangesAsync();
        }
    }
}
