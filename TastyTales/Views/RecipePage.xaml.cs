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


}