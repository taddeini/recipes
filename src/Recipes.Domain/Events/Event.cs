using System;

namespace Recipes.Domain.Events
{
    public abstract class Event
    {
        public Event(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));
            Id = id;
        }

        public Guid Id { get; }
    }                
}
