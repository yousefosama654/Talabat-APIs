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
        public List<Expression<Func<T, object>>> Includes { get; set; }=new List<Expression<Func<T, object>>>();
        // to get all without any criteria
        public BaseSpecification()
        {
            //Includes = new List<Expression<Func<T, object>>>();
        }
        // to get  with some criteria
        public BaseSpecification(Expression<Func<T, bool>> Criteria)
        {
            this.Criteria = Criteria;
            //Includes = new List<Expression<Func<T, object>>>();
        }
    }
}
