using TastyTales.Data;
using TastyTales.Models;
using TastyTales.Services;
using TastyTales.ViewModels;

namespace TastyTales.Views;

public partial class CategoriesPageUI : ContentPage
{
	public CategoriesPageUI(ICategoriesPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    private async void OnImageTapped(object sender, EventArgs e)
    {
        var image = sender as View;
        var category = image?.BindingContext as Category;

        if (category != null)
        {
            await Navigation.PushAsync(new CategoryPageUI(new CategoryPage(new DataServices(new DatabaseRepository()), category)));
        }
    }
}