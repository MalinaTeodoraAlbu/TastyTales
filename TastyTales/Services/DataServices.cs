using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyTales.Models;

namespace TastyTales.Services
{
    internal class DataServices : IDataServices
    {
        private Data.IRepository _repository;
        public async Task<IList<Recipe>> GetRecipesByName(string name)
        {
            var recipies = await _repository.GetRecipesByName(name);
            if (recipies.Count == 0)
            {
                recipies = await SearchName(name);
            }
            return recipies;
        }
        private async Task<IList<Models.Recipe>> SearchName(string name)
        {
            using (HttpClient client = new HttpClient())
            {

                List<Recipe> recipes = new List<Recipe>();
                return recipes;
            }
        }
    
}
}
