using TastyTales.Models;
using TastyTales.ViewModels;

namespace TastyTales.Views;

public partial class HomePage : ContentPage
{
	public HomePage(IMainPage vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    private async void OnImageTapped(object sender, EventArgs e)
    {
        var image = sender as Image;
        var recipe = image?.BindingContext as Recipe;

        if (recipe != null)
        {
            await Navigation.PushAsync(new RecipePage(recipe));
        }
    }
}