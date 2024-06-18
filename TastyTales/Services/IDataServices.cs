using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyTales.Models;

namespace TastyTales.Services
{
    public interface IDataServices
    {
        Task<IList<Models.Recipe>> GetRecipesByName(string name);
        Task<IList<Models.Recipe>> GetPopularDeserts();
        Task<IList<Models.Category>> GetCategories();
        Task<IList<Models.Recipe>> GetRecipeByCategory(Models.Category category);
        Task<Recipe> GetRecipe(int id);
        Task<bool> isFavorite(int id);
        Task SaveRecipeToDb(Recipe recipe);
        Task DeleteRecipe(int id);
        Task<IList<Models.Recipe>> GetAllRecipesDB();
    }
}
