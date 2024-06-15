using Newtonsoft.Json.Linq;
using TastyTales.Models;

namespace TastyTales.Services
{
    internal class DataServices : IDataServices
    {
        private Data.IRepository repository;
        public async Task<IList<Recipe>> GetRecipesByName(string name)
        {
            var recipies = await repository.GetRecipesByName(name);
            if (recipies.Count == 0)
            {
                recipies = await SearchName(name);
            }
            return recipies;
        }
        private async Task<IList<Recipe>> SearchName(string name)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"https://www.themealdb.com/api/json/v1/1/search.php?s={name}";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(json);
                JArray meals = (JArray)data["meals"];

                List<Recipe> recipes = new List<Recipe>();
                if (meals != null)
                {
                    foreach (var meal in meals)
                    {
                        Recipe recipe = new Recipe
                        {
                            Id = (int)meal["idMeal"],
                            MealName = (string)meal["strMeal"],
                            Category = (string)meal["strCategory"],
                            Area = (string)meal["strArea"],
                            Instructions = (string)meal["strInstructions"],
                            MealThum = (string)meal["strMealThumb"],
                            Tags = (string)meal["strTags"],
                            StrYoutube = (string)meal["strYoutube"],
                            Ingredients = new List<string>(),
                            Measure = new List<string>()
                        };

                        for (int i = 1; i <= 20; i++)
                        {
                            string ingredient = (string)meal[$"strIngredient{i}"];
                            string measure = (string)meal[$"strMeasure{i}"];
                            if (!string.IsNullOrEmpty(ingredient))
                            {
                                recipe.Ingredients.Add(ingredient);
                            }
                            if (!string.IsNullOrEmpty(measure))
                            {
                                recipe.Measure.Add(measure);
                            }
                        }

                        recipes.Add(recipe);
                    }
                }
                await repository.SaveRecipes(recipes);
                return recipes;
            }
        }

    }
}
