using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using TastyTales.Models;
using TastyTales.Services;
using Microsoft.Maui.Graphics;


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

        private bool isRecipeSaved;
        public bool IsRecipeSaved
        {
            get => isRecipeSaved;
            set
            {
                if (isRecipeSaved != value)
                {
                    isRecipeSaved = value;
                    OnPropertyChanged(nameof(IsRecipeSaved));
                    OnPropertyChanged(nameof(SaveButtonBackgroundColor));
                }
            }
        }

        public Color SaveButtonBackgroundColor => IsRecipeSaved ? Colors.Red : Colors.White;

        public async Task SaveRecipeAsync()
        {
            await service.SaveRecipeToDb(recipe);
            isRecipeSaved = true;
            OnPropertyChanged(nameof(SaveButtonBackgroundColor));
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
