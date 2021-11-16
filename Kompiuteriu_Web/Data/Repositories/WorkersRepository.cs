using Kompiuteriu_Web.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kompiuteriu_Web.Data.Repositories
{
    public interface IWorkersRepository
    {
        Task DeleteAsync(Worker worker);
        Task<List<Worker>> GetAllAsync(int shopId);
        Task<Worker> GetAsync(int shopId, int workerId);
        Task InsertAsync(Worker worker);
        Task UpdateAsync(Worker worker);
    }

    public class WorkersRepository : IWorkersRepository
    {
        private readonly RestContext _restContext;
        public WorkersRepository(RestContext restContext)
        {
            _restContext = restContext;
        }

        public async Task<Worker> GetAsync(int shopId, int workerId)
        {
            return await _restContext.Workers.FirstOrDefaultAsync(worker => worker.ShopId == shopId && worker.id == workerId);
        }

        public async Task<List<Worker>> GetAllAsync(int shopId)
        {
            return await _restContext.Workers.Where(worker => worker.ShopId == shopId).ToListAsync();
        }

        public async Task InsertAsync(Worker worker)
        {
            _restContext.Workers.Add(worker);
            await _restContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Worker worker)
        {
            _restContext.Workers.Update(worker);
            await _restContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Worker worker)
        {
            _restContext.Workers.Remove(worker);
            await _restContext.SaveChangesAsync();
        }
    }
}
