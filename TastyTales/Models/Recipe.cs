using Newtonsoft.Json;
using SQLite;

namespace TastyTales.Models
{
    public class Recipe
    {

        [PrimaryKey]
        public int Id { get; set; }
        public string MealName { get; set; }
        public string Category { get; set; }
        public string Area { get; set; }
        public string Instructions { get; set; }
        public string MealThum { get; set; }

        public string Tags { get; set; }

        public string StrYoutube { get; set; }

        [Ignore]
        public List<string> Ingredients { get; set; } = new List<string>();

        [Ignore]
        public List<string> Measure { get; set; } = new List<string>();

        public string IngredientsJson
        {
            get => JsonConvert.SerializeObject(Ingredients);
            set => Ingredients = string.IsNullOrEmpty(value) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(value);
        }

        public string MeasureJson
        {
            get => JsonConvert.SerializeObject(Measure);
            set => Measure = string.IsNullOrEmpty(value) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(value);
        }



    }
}