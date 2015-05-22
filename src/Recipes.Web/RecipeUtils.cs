using System.Globalization;

namespace Recipes.Web
{
    public class RecipeUtils
    {
        public static string ConvertUrlToTitle(string urlTitle)
        {
            var textInfo = new CultureInfo("en-US").TextInfo;
            var title = urlTitle.Replace("-", " ");
            return textInfo.ToTitleCase(title);
        }

        public static string ConvertTitleToUrl(string title) => title.Replace(" ", "-").ToLower();
    }
}
