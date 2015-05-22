using Recipes.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Recipes.Domain.Queries
{
    public interface IQueryProvider<TQuery>
    {
        Task<IEnumerable<TQuery>> Find(Expression<Func<TQuery, bool>> predicate);

        Task<IEnumerable<TQuery>> GetAll();

        Task<TQuery> Get(Guid id);        
    }
}
