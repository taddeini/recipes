using NSubstitute;
using Recipes.Domain.Commands;
using Recipes.Domain.Queries;
using Recipes.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Recipes.Domain.Tests.Commands
{
    public class RecipeCommandHandlerTests
    {
        IRepository<RecipeAggregate> _mockRecipeRepository;
        IQueryProvider<RecipeQuery> _mockRecipeQueryProvider;
        RecipeCommandHandler handler;

        public RecipeCommandHandlerTests()
        {
            _mockRecipeRepository = Substitute.For<IRepository<RecipeAggregate>>();
            _mockRecipeQueryProvider = Substitute.For<IQueryProvider<RecipeQuery>>();

            handler = new RecipeCommandHandler(_mockRecipeRepository, _mockRecipeQueryProvider);
        }

        [Fact]
        public void CreatingRecipeCommandHandler_WithInvalidArguments_ThrowsAnException()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new RecipeCommandHandler(null, _mockRecipeQueryProvider));
            Assert.Throws(typeof(ArgumentNullException), () => new RecipeCommandHandler(_mockRecipeRepository, null));
        }

        [Fact]
        public void HandleAddRecipeCommand_WithBasicCommandValues_AddsARecipe()
        {
            // Arrange
            var id = Guid.NewGuid();
            var title = "foo";
            var desc = "bar";
            var command = new AddRecipeCommand(id, title, desc);

            // Act
            handler.Handle(command);

            // Assert
            _mockRecipeRepository
                .Received(1)
                .Save(Arg.Is<RecipeAggregate>(rec => (rec.Id == id)));
        }

        [Fact]
        public void HandleAddRecipeCommand_WithTitleThatAlreadyExists_ThrowsAnInvalidOperationsException()
        {
            // Arrange
            var fakeRecipes = new List<RecipeQuery> { new RecipeQuery { Title = "foo" } };
            _mockRecipeQueryProvider
                .Find(Arg.Any<Expression<Func<RecipeQuery, bool>>>())
                .Returns(Task.FromResult(fakeRecipes.AsEnumerable()));

            // Act/Assert
            Assert.Throws(typeof(InvalidOperationException), () => {
                handler.Handle(new AddRecipeCommand(Guid.NewGuid(), "foo", "bar"));
            });                       
        }

        [Fact]
        public void HandleUpdateRecipeCommand_ForInvalidRecipe_Bails()
        {
            // Arrange
            _mockRecipeRepository
                .Get(Arg.Any<Guid>())
                .Returns(rec => Task.FromResult<RecipeAggregate>(null));

            // Act
            handler.Handle(new UpdateRecipeCommand(Guid.NewGuid(), "foo", "bar"));

            // Assert
            _mockRecipeRepository.DidNotReceive().Save(Arg.Any<RecipeAggregate>());
        }

        [Fact]
        public void HandleUpdateRecipeCommand_WithTitleThatAlreadyExists_DoesNotUpdateTheRecipe()
        {
            // Arrange
            _mockRecipeRepository
                .Get(Arg.Any<Guid>())
                .Returns(rec => Task.FromResult(RecipeAggregate.Create(Guid.NewGuid(), "foo", "bar")));

            var fakeRecipes = new List<RecipeQuery> { new RecipeQuery { Title = "foo" } };
            _mockRecipeQueryProvider
                .Find(Arg.Any<Expression<Func<RecipeQuery, bool>>>())                
                .Returns(Task.FromResult(fakeRecipes.AsEnumerable()));

            // Act
            handler.Handle(new UpdateRecipeCommand(Guid.NewGuid(), "foo", "bar"));

            // Assert
            _mockRecipeRepository.DidNotReceive().Save(Arg.Any<RecipeAggregate>());
        }

        [Fact]
        public void HandleDeleteRecipeCommand_WithBasicCommandValues_DeletesARecipe()
        {
            // Arrange
            var id = Guid.NewGuid();
            var recipe = RecipeAggregate.Create(id, "foo", "bar");
            var deleteCommand = new DeleteRecipeCommand(id);

            _mockRecipeRepository.Get(id).Returns(Task.FromResult(recipe));

            // Act
            handler.Handle(deleteCommand);

            // Assert
            _mockRecipeRepository
                .Received(1)
                .Save(Arg.Is<RecipeAggregate>(rec => (rec.Id == id)));
        }

        [Fact]
        public void HandleDeleteRecipeCommand_ForInvalidRecipe_Bails()
        {
            // Arrange
            _mockRecipeRepository
                .Get(Arg.Any<Guid>())
                .Returns(rec => Task.FromResult<RecipeAggregate>(null));

            // Act
            handler.Handle(new DeleteRecipeCommand(Guid.NewGuid()));

            // Assert
            _mockRecipeRepository.DidNotReceive().Save(Arg.Any<RecipeAggregate>());
        }

        [Fact]
        public void HandleUpdateRecipeCommand_WithBasicCommandValues_UpdatesARecipe()
        {
            // Arrange
            var id = Guid.NewGuid();
            var recipe = RecipeAggregate.Create(id, "foo", "bar");

            var newTitle = "fizz";
            var newDesc = "bin";
            var updateCommand = new UpdateRecipeCommand(id, newTitle, newDesc);

            _mockRecipeRepository.Get(id).Returns(Task.FromResult(recipe));

            // Act
            handler.Handle(updateCommand);

            // Assert
            _mockRecipeRepository
                .Received(1)
                .Save(Arg.Is<RecipeAggregate>(rec => (rec.Id == id)));
        }
    }
}
