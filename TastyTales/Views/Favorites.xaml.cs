using TastyTales.ViewModels;
using Microsoft.Maui.Controls;
using TastyTales.Models;
using TastyTales.Services;

namespace TastyTales.Views
{
    public partial class Favorites : ContentPage
    {
        private readonly IFavoritesVM viewModel;

        public Favorites(IFavoritesVM vm)
        {
            InitializeComponent();
            BindingContext = viewModel = vm;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.LoadDataAsync();
        }

        private async void OnImageTapped(object sender, EventArgs e)
        {
            var image = sender as Image;
            var recipe = image?.BindingContext as Recipe;

            if (recipe != null)
            {
                await Navigation.PushAsync(new RecipePage(new RecipeVM(recipe, new DataServices(new Data.DatabaseRepository()))));
            }
        }
    }
}
