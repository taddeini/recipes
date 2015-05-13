using NSubstitute;
using Recipes.Domain.Aggregates;
using Recipes.Domain.Commands;
using Recipes.Domain.Repositories;
using System;
using Xunit;

namespace Recipes.Domain.Tests.Commands
{
    public class RecipeCommandHandlerTests
    {
        IRepository<Recipe> _mockRecipeRepository;
        RecipeCommandHandler handler;

        public RecipeCommandHandlerTests()
        {
            _mockRecipeRepository = Substitute.For<IRepository<Recipe>>();
            handler = new RecipeCommandHandler(_mockRecipeRepository);
        }

        [Fact]
        public void CreatingRecipeCommandHandler_WithInvalidArguments_ThrowsAnException()
        {
            Assert.Throws(typeof(ArgumentNullException), () => new RecipeCommandHandler(null));
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
                .Save(Arg.Is<Recipe>(rec => (rec.Id == id && rec.Title == title && rec.Description == desc)));
        }

        [Fact]
        public void HandleUpdateRecipeCommand_ForInvalidRecipe_Bails()
        {
            _mockRecipeRepository.Get(Arg.Any<Guid>()).Returns(rec => null);
            handler.Handle(new UpdateRecipeCommand(Guid.NewGuid(), "foo", "bar"));
            _mockRecipeRepository.DidNotReceive().Save(Arg.Any<Recipe>());
        }

        [Fact]
        public void HandleUpdateRecipeCommand_WithBasicCommandValues_UpdatesARecipe()
        {
            // Arrange
            var id = Guid.NewGuid();
            var recipe = new Recipe(id, "foo", "bar");

            var newTitle = "fizz";
            var newDesc = "bin";
            var updateCommand = new UpdateRecipeCommand(id, newTitle, newDesc);

            _mockRecipeRepository.Get(id).Returns(recipe);

            // Act
            handler.Handle(updateCommand);

            // Assert
            _mockRecipeRepository
                .Received(1)
                .Save(Arg.Is<Recipe>(rec => (rec.Id == id && rec.Title == newTitle && rec.Description == newDesc)));
        }
    }
}
