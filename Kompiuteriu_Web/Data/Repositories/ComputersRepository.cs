using Kompiuteriu_Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kompiuteriu_Web.Data.Repositories
{
    public interface IComputersRepository
    {
        Task DeleteAsync(Computer computer);
        Task<List<Computer>> GetAllAsync(int shopId);
        Task<Computer> GetAsync(int shopId, int computerId);
        Task InsertAsync(Computer computer);
        Task UpdateAsync(Computer computer);
    }

    public class ComputersRepository : IComputersRepository
    {
        private readonly RestContext _restContext;
        public ComputersRepository(RestContext restContext)
        {
            _restContext = restContext;
        }

        public async Task<Computer> GetAsync(int shopId, int computerId)
        {
            return await _restContext.Computers.FirstOrDefaultAsync(computer => computer.ShopId == shopId && computer.id == computerId);
        }

        public async Task<List<Computer>> GetAllAsync(int shopId)
        {
            return await _restContext.Computers.Where(computer => computer.ShopId == shopId).ToListAsync();
        }

        public async Task InsertAsync(Computer computer)
        {
            _restContext.Computers.Add(computer);
            await _restContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Computer computer)
        {
            _restContext.Computers.Update(computer);
            await _restContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Computer computer)
        {
            _restContext.Computers.Remove(computer);
            await _restContext.SaveChangesAsync();
        }
    }
}
