using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;

namespace Talabat.core.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public  Task<IEnumerable<T>> GetAllAsync();
        public  Task<T> GetByIdAsync(int id);
    }
}
