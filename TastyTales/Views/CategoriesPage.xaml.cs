using TastyTales.ViewModels;

namespace TastyTales.Views;

public partial class CategoriesPage : ContentPage
{
	public CategoriesPage(ICategoryPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}