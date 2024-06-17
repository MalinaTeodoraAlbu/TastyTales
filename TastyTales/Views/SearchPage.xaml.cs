using TastyTales.Data;
using TastyTales.Models;
using TastyTales.Services;

namespace TastyTales.Views;

public partial class SearchPage : ContentPage
{
	public SearchPage(ViewModels.ISearchRecipeViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
	}

    private void ExitToolbarItem_Clicked(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }

    private async void SearchButton_Clicked(object sender, EventArgs e)
    {
        await (BindingContext as ViewModels.ISearchRecipeViewModel).SearchName();
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        (BindingContext as ViewModels.ISearchRecipeViewModel).ClearResults();
    }

    private async void OnTextTapped(object sender, EventArgs e)
    {
        var view = sender as View;
        var recipe = view?.BindingContext as Recipe;

        if (recipe != null)
        {
            await Navigation.PushAsync(new RecipePage(new ViewModels.RecipeVM(recipe, new DataServices(new DatabaseRepository()))));
        }
    }
}