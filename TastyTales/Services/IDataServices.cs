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
        Task<IList<Models.Recipe>> GetLatestMeals();
        Task<IList<Models.Recipe>> GetPopularDeserts();
        Task<IList<Models.Recipe>> GetRecommendedMeals();
        Task<IList<Models.Category>> GetCategories();
        Task<IList<Models.Recipe>> GetRecipeByCategory(Models.Category category);
        Task<Recipe> GetRecipe(int id);
    }
}
