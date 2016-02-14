using System;
using System.Threading.Tasks;

namespace Recipes.Domain.Repositories
{
    public interface IRepository<TAggregate> where TAggregate : Aggregate
    {
        void Save(TAggregate aggregate);

        Task<TAggregate> Get(Guid id);
    }
}