using Recipes.Domain.Aggregates;
using System;
using System.Collections.Generic;

namespace Recipes.Domain.Queries
{
    public interface IQueryProvider<TAggregate> where TAggregate : Aggregate
    {
        IEnumerable<TAggregate> Find(Func<Recipe, bool> predicate);

        IEnumerable<TAggregate> GetAll();

        TAggregate Get(Guid id);        
    }
}
