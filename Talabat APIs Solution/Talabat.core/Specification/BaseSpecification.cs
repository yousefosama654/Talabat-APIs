using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;

namespace Talabat.core.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderByAscending { get; set; }
        public Expression<Func<T, object>> OrderByDescending { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
        public bool IsPaginationEnabled { get; set; }=false;

        // to get all without any criteria
        public BaseSpecification()
        {

        }
        // to get  with some criteria
        public BaseSpecification(Expression<Func<T, bool>> Criteria)
        {
            this.Criteria = Criteria;
        }
        public void AddOrderByAscending(Expression<Func<T, object>> sort)
        {
            this.OrderByAscending = sort;
        }
        public void AddOrderByDescending(Expression<Func<T, object>> sort)
        {
            this.OrderByDescending = sort;
        }

        public void ApplyPagination(int skip, int take)
        {
            this.IsPaginationEnabled = true;
            this.take = take;
            this.skip = skip;
        }
    }
}
