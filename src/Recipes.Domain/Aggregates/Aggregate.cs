using Recipes.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.Domain.Aggregates
{
    public abstract class Aggregate
    {
        private List<Event> _pendingChanges;
        
        public Guid Id { get; protected set; }

        public IEnumerable<Event> PendingChanges => _pendingChanges;        
        
        public void MarkChangesCommitted()
        {
            if (_pendingChanges == null)
            {
                _pendingChanges = new List<Event>();
            }

            _pendingChanges.Clear();
        }

        protected virtual void ApplyEvent(Event @event)
        {
            if (_pendingChanges == null)
            {
                _pendingChanges = new List<Event>();
            }

            _pendingChanges.Add(@event);
        }        
    }
}