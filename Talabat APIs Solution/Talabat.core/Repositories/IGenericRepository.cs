using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Specification;

namespace Talabat.core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public  Task<IEnumerable<T>> GetAllWithSpecsAsync(ISpecification<T> specs);
        public Task<T> GetByIdWithSpecsAsync(ISpecification<T>specs);
        public Task<int> GetCountAsync(ISpecification<T> specs);

    }
}
