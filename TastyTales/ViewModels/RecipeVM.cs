using System.ComponentModel;
using System.Threading.Tasks;
using TastyTales.Models;
using TastyTales.Services;

namespace TastyTales.ViewModels
{
    public class RecipeVM : IRecipeVM
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IDataServices service;
        private Recipe recipe;

        public RecipeVM(Recipe recipe, IDataServices service)
        {
            this.recipe = recipe;    
            this.service = service;
            LoadDataAsync(recipe.Id);
        }

        public Recipe Recipe
        {
            get { return recipe; }
            set
            {
                recipe = value;
                OnPropertyChanged(nameof(Recipe));
            }
        }

        private async Task LoadDataAsync(int id)
        {
            Recipe = await service.GetRecipe(id); 
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
