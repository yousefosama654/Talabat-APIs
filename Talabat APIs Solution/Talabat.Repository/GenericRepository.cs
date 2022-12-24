using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Repositories;
using Talabat.core.Specification;
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
        public async Task<IEnumerable<T>> GetAllWithSpecsAsync(ISpecification<T> specs)
        {
            return await this.ApplyQuery(specs).ToListAsync();
        }
        public async Task<T> GetByIdWithSpecsAsync(ISpecification<T> specs)
        {
            return await this.ApplyQuery(specs).FirstOrDefaultAsync();
        }
        private IQueryable<T> ApplyQuery(ISpecification<T>specs)
        {
            return SpecificationEvaluator<T>.BuildQuery(this.StoreContext.Set<T>(), specs);
        }
    }
}
