using Recipes.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.Domain.Aggregates
{
    public abstract class Aggregate
	{
        private List<Event> _pendingChanges;

        public Aggregate()
        {
            _pendingChanges = new List<Event>();
        }

        public Guid Id { get; protected set; }

        public IEnumerable<Event> PendingChanges => _pendingChanges;

        public void MarkChangesCommitted()
        {
            _pendingChanges.Clear();
        }
        
        protected void Apply(Event @event)
        {
            _pendingChanges.Add(@event);
        }        
    }
}