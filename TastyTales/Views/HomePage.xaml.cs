using TastyTales.ViewModels;

namespace TastyTales.Views;

public partial class HomePage : ContentPage
{
	public HomePage(IMainPage vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}