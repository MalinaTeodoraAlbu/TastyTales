namespace TastyTales.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private void ExitToolbarItem_Clicked(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }

    private async void SearchButton_Clicked(object sender, EventArgs e)
    {
        await (BindingContext as ViewModels.ISearchNameViewModel).SearchName();
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        (BindingContext as ViewModels.ISearchNameViewModel).ClearResults();
    }
}