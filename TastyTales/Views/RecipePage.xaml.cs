using TastyTales.Models;
using TastyTales.ViewModels;

namespace TastyTales.Views;

public partial class RecipePage : ContentPage
{
    private readonly IRecipeVM vm;
    public RecipePage(IRecipeVM vm)
    {
        InitializeComponent();
        this.vm = vm;
        BindingContext = vm;
    }


}