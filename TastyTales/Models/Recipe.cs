using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyTales.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string MealName { get; set; }
        public Category Category { get; set; } = new Category();
    }
}
