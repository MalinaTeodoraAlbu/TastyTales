using TastyTales.Models;

namespace TastyTales.Data
{
    public interface IRepository
    {
        Task<IList<Recipe>> GetAllRecipesFromDB();
        Task SaveRecipes(IList<Models.Recipe> items);

        Task SaveRecipe(Recipe recipe);

        Task Delete(int id);

        Task<bool> IsFav(int id);
    }
}
