﻿using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using TastyTales.Models;

namespace TastyTales.Services
{
    public class DataServices : IDataServices
    {
        private Data.IRepository repository;

        public Task<IList<Recipe>> GetLatestMeals()
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Recipe>> GetPopularDeserts()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"https://www.themealdb.com/api/json/v1/1/filter.php?c=Dessert";
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
                string url = $"https://www.themealdb.com/api/json/v1/1/search.php?s={name}";
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
