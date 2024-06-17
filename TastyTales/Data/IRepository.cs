using TastyTales.Models;

namespace TastyTales.Data
{
    public interface IRepository
    {

        Task SaveRecipeToDB(Recipe recipe);
        Task<IList<Recipe>> GetAllRecipesFromDB();
    }
}
