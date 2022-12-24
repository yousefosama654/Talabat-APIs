using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Specification;

namespace Talabat.Repository.Data
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        // the class has only one function that builds the query and return with it
        public static IQueryable<T> BuildQuery(IQueryable<T>EntryPoint,ISpecification<T>Specs)
        {
            var query = EntryPoint;
            // to do filteration on the query
            if (Specs.Criteria!=null)
            {
                query = query.Where(Specs.Criteria);
            }
            // to do includes on the query based on giving currquery initial value =query
            query = Specs.Includes.Aggregate(query,(currquery, specsInclude) => currquery.Include(specsInclude));
            return query;
        }
    }
}
