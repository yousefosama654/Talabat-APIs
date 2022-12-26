using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;

namespace Talabat.core.Specification
{
    // I know that the all specs will be done a class which is a table in the database 
    public interface ISpecification<T> where T : BaseEntity
    {
        // the func is a delegate that has one ouput and zero or more inputs
        public Expression<Func<T,bool>> Criteria { get; set; }
        public List<Expression<Func<T,object>>> Includes { get; set; }
        public Expression<Func<T, object>> OrderByAscending { get; set; }
        public Expression<Func<T, object>> OrderByDescending { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
        public bool IsPaginationEnabled { get; set; }

        public void AddOrderByAscending(Expression<Func<T, object>> sort);
        public void AddOrderByDescending(Expression<Func<T, object>> sort);
        public void ApplyPagination(int skip,int take);


    }
}
