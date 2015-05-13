using Recipes.Domain.Aggregates;
using System;

namespace Recipes.Domain.Repositories
{
    public interface IRepository<TAggregate> where TAggregate : Aggregate
    {
        void Save(TAggregate aggregate);

        TAggregate Get(Guid id);
    }
}