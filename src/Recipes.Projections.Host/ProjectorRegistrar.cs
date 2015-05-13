using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using System.Collections.Generic;

namespace Recipes.Projections.Projectors
{
    public class ProjectorRegistrar
    {
        private List<IProjector> _projectors;
        private IEventStoreConnection _connection;

        public ProjectorRegistrar(IEventStoreConnection connection)
        {
            _connection = connection;
            _projectors = new List<IProjector>();
        }        

        public void Register(IProjector projector)
        {
            _projectors.Add(projector);

            //TODO: change credentials, and add to (secret) config
            _connection.SubscribeToAllAsync(false,
                (sub, evt) => projector.HandleEvent(evt, sub),
                (sub, reason, ex) => projector.HandleEventDropped(sub, reason, ex), 
                new UserCredentials("admin", "changeit")).Wait();
        }

        public IEnumerable<IProjector> Projectors => _projectors;
    }
}
