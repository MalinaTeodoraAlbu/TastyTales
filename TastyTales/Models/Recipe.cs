using SQLite;

namespace TastyTales.Models
{
    [Table("recipes")]
    public class Recipe
    {
       
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string MealName { get; set; }
        public string Category { get; set; }
        public string Area { get; set; }
        public string Instructions { get; set; }
        public string MealThum { get; set; }

        public string Tags { get; set; }

        public string StrYoutube { get; set; }

        public List<String> Ingredients { get; set; }
        public List<String> Measure {  get; set; }


    }
}
