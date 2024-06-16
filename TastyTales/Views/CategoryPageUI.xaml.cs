using TastyTales.ViewModels;

namespace TastyTales.Views;

public partial class CategoryPageUI : ContentPage
{
	public CategoryPageUI(ICategoryPageViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
    }
}