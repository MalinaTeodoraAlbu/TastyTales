using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
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

            SaveRecipeCommand = new Command(async () => await service.SaveRecipeAsync(recipe));

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

        public ICommand SaveRecipeCommand { get; private set; }

        private async Task SaveRecipeAsync()
        {
            await service.SaveRecipeAsync(recipe);
            Console.WriteLine("SaveRecipeAsync method executed successfully.");
            await Application.Current.MainPage.DisplayAlert("Save", "Recipe saved successfully!", "OK");
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
