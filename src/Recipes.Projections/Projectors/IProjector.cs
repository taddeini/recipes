using EventStore.ClientAPI;
using System;

namespace Recipes.Projections.Projectors
{
    public interface IProjector
    {
        void HandleEvent(ResolvedEvent @event, EventStoreSubscription subscription);        
    }
}
