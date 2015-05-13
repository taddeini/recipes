namespace Recipes.Domain.Events
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent @event);
    }
}
