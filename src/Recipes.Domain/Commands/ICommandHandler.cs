using System.Threading.Tasks;

namespace Recipes.Domain.Commands
{
    public interface ICommandHandler<TCommand> where TCommand : Command
    {
        void Handle(TCommand command);
    }
}
