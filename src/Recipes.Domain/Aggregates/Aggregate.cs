using Recipes.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.Domain.Aggregates
{
    public abstract class Aggregate
    {
        private readonly Dictionary<Type, Action<Event>> _handlers = new Dictionary<Type, Action<Event>>();

        private readonly List<Event> _pendingChanges = new List<Event>();

        public Guid Id { get; protected set; }

        //TODO: Implement
        public int Version { get; private set; }

        public IEnumerable<Event> PendingChanges => _pendingChanges;

        protected void Handles<TEvent>(Action<TEvent> handler) where TEvent : Event
        {
            _handlers.Add(typeof(TEvent), @event => handler((TEvent)@event));
        }

        public void MarkChangesCommitted()
        {            
            _pendingChanges.Clear();
        }

        protected virtual void ApplyEvent<TEvent>(TEvent @event) where TEvent : Event
        {            
            _pendingChanges.Add(@event);

            if (_handlers.ContainsKey(@event.GetType()))
            {
                var handler = _handlers[@event.GetType()];                
                handler.Invoke(@event);                
            }            
        }

        public static TAggregate LoadFromHistory<TAggregate>(IEnumerable<Event> history) where TAggregate : Aggregate
        {
            var aggregate = (TAggregate)Activator.CreateInstance(typeof(TAggregate), true);
            foreach (var @event in history)
            {
                aggregate.ApplyEvent(@event);
            }
            aggregate.MarkChangesCommitted();
            return aggregate;
        }
    }
}