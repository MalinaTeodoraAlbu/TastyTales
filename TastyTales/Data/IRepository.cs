using TastyTales.Models;

namespace TastyTales.Data
{
    public interface IRepository
    {
        Task<IList<Models.Recipe>> GetRecipesByName(string name);
        Task<IList<Models.Recipe>> GetRecipesByCategory(string name);
        Task SaveRecipes(IList<Models.Recipe> items);

        Task SaveRecipe(Recipe recipe);
    }
}
