using TastyTales.Models;
using TastyTales.ViewModels;

namespace TastyTales.Views;

public partial class RecipePage : ContentPage
{
    public RecipePage(IRecipeVM vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private async void SaveRecipeAsync(object sender, EventArgs e)
    {
        await (BindingContext as ViewModels.IRecipeVM).SaveRecipeAsync();
    }

}