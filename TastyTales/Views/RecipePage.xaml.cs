using TastyTales.Models;

namespace TastyTales.Views;

public partial class RecipePage : ContentPage
{
    public Recipe Recipe { get; set; }
    public RecipePage(Recipe recipe)
    {
        InitializeComponent();
        Recipe = recipe;
        BindingContext = Recipe;
    }
}