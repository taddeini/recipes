using System;

namespace Recipes.Domain.Queries
{
    public class RecipeQuery
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string UrlTitle { get; set; }

        public string Description { get; set; }
    }
}
