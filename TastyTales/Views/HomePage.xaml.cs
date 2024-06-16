
using TastyTales.Models;
using TastyTales.Services;
using TastyTales.ViewModels;

namespace TastyTales.Views
{
    public partial class HomePage : ContentPage
    {
        private readonly IMainPage mainPage;

        public HomePage(IMainPage mainPage)
        {
            InitializeComponent();
            this.mainPage = mainPage;
            BindingContext = this.mainPage;
        }

        private async void OnImageTapped(object sender, EventArgs e)
        {
            var image = sender as Image;
            var recipe = image?.BindingContext as Recipe;

            if (recipe != null)
            {
                await Navigation.PushAsync(new RecipePage(new RecipeVM(recipe, new DataServices())));
            }
        }
    }
}
