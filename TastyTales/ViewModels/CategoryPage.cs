using System.ComponentModel;
using TastyTales.Models;
using TastyTales.Services;

namespace TastyTales.ViewModels
{
    public class CategoryPage : ICategoryPageViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IDataServices service;

        private IList<Recipe> recipes;

        private Category category;

        private bool busy = false;

        public IList<Recipe> RecipesByCategory
        {
            get { return recipes; }
            set
            {
                recipes = value;
                OnPropertyChanged(nameof(RecipesByCategory)); // Corrected property name here
            }
        }

        public Category SelectedCategory
        {
            get { return category; }
            set
            {
                category = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        public bool Busy
        {
            get
            {
                return busy;
            }
            private set
            {
                busy = value;
                OnPropertyChanged(nameof(Busy));
            }
        }

        public CategoryPage(IDataServices service, Category category)
        {
            this.service = service;
            this.category = category;
            RecipesByCategory = new List<Recipe>();
            
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            Busy = true;
            RecipesByCategory = await service.GetRecipeByCategory(SelectedCategory);
            Busy = false;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
