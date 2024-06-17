using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using TastyTales.Models;
using static SQLite.SQLite3;

namespace TastyTales.Services
{
    public class DataServices : IDataServices
    {
        private Data.IRepository repository;

        public DataServices(Data.IRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IList<Category>> GetCategories()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = Utilities.Constants.baseURL + "categories.php";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(json);

                if (!data.ContainsKey("categories") || data["categories"].Type == JTokenType.Null)
                {
                    return new List<Category>();
                }
                JArray categoriesJ = (JArray)data["categories"];
                List<Category> categories = new List<Category>();
                if (categoriesJ != null)
                {
                    foreach (var category in categoriesJ)
                    {
                        Category newCategory = new Category
                        {
                            Id = (int)category["idCategory"],
                            CategoryName = (string)category["strCategory"],
                            Thumbnail = (string)category["strCategoryThumb"]
                        };

                        categories.Add(newCategory);
                    }
                }
                return categories;
            }
        }

        public Task<IList<Recipe>> GetLatestMeals()
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Recipe>> GetPopularDeserts()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = Utilities.Constants.baseURL + "filter.php?c=Dessert";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(json);

                if (!data.ContainsKey("meals") || data["meals"].Type == JTokenType.Null)
                {
                    return new List<Recipe>();
                }

                JArray meals = (JArray)data["meals"];
                List<Recipe> recipes = new List<Recipe>();

                int limit = 12;
                int count = 0;

                if (meals != null)
                {
                    foreach (var meal in meals)
                    {
                        if (count >= limit)
                        {
                            break;
                        }

                        Recipe recipe = new Recipe
                        {
                           
                            MealName = (string)meal["strMeal"],
                            MealThum = (string)meal["strMealThumb"],
                        };
                        recipes.Add(recipe);
                        count++;
                    }
                }
                
                return recipes;
            }
        }

        public async Task<Recipe> GetRecipe(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = $"https://www.themealdb.com/api/json/v1/1/lookup.php?i={id}";
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();
                    JObject data = JObject.Parse(json);

                    if (!data.ContainsKey("meals") || data["meals"].Type == JTokenType.Null || data["meals"].Count() == 0)
                    {
                        return null; 
                    }

                    JToken meal = data["meals"][0]; 

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

                    return recipe;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching recipe: {ex.Message}");
                    return null;
                }
            }
        }

        public async Task<IList<Recipe>> GetRecipeByCategory(Category category)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = Utilities.Constants.baseURL + $"filter.php?c={category.CategoryName}";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(json);

                if (!data.ContainsKey("meals") || data["meals"].Type == JTokenType.Null)
                {
                    return new List<Recipe>();
                }

                JArray meals = (JArray)data["meals"];
                List<Recipe> recipes = new List<Recipe>();

                int limit = 12;
                int count = 0;

                if (meals != null)
                {
                    foreach (var meal in meals)
                    {
                        if (count >= limit)
                        {
                            break;
                        }

                        Recipe recipe = new Recipe
                        {
                            Id = (int)meal["idMeal"],
                            MealName = (string)meal["strMeal"],
                            MealThum = (string)meal["strMealThumb"],
                        };
                        recipes.Add(recipe);
                        count++;
                    }
                }
                return recipes;
            }
        }

        public async Task<IList<Recipe>> GetRecipesByName(string name)
        {
            var recipies = await SearchName(name);
            return recipies;
        }

        public Task<IList<Recipe>> GetRecommendedMeals()
        {
            throw new NotImplementedException();
        }

        private async Task<IList<Recipe>> SearchName(string name)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = Utilities.Constants.baseURL + $"search.php?s={name}";
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(json);

                if (!data.ContainsKey("meals") || data["meals"].Type == JTokenType.Null)
                {
                    return new List<Recipe>(); 
                }
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
                return recipes;
            }
        }

    }
}
