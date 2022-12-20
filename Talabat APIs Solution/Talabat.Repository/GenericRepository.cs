using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Repositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public StoreContext StoreContext { get; }
        public GenericRepository(StoreContext StoreContext)
        {
            this.StoreContext = StoreContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await this.StoreContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await this.StoreContext.Set<T>().Where(p => p.Id == id).FirstOrDefaultAsync();
        }
    }
}
