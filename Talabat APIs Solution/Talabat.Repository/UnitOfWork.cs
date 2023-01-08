using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Repositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable repositories { get; set; }
        public StoreContext StoreContext { get; }

        public UnitOfWork(StoreContext StoreContext)
        {
            this.StoreContext = StoreContext;
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            if (repositories == null)
                repositories = new Hashtable();
            var type = typeof(T);
            if (!repositories.ContainsKey(type))
                repositories.Add(type, new GenericRepository<T>(StoreContext));
            return (IGenericRepository<T>)repositories[type];
        }

        public async Task<int> Complete()
        {
            return await this.StoreContext.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await this.StoreContext.DisposeAsync();
        }
    }
}
