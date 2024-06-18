using TastyTales.ViewModels;
using Microsoft.Maui.Controls;

namespace TastyTales.Views
{
    public partial class Favorites : ContentPage
    {
        private readonly IFavoritesVM viewModel;

        public Favorites(IFavoritesVM vm)
        {
            InitializeComponent();
            BindingContext = viewModel = vm;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.LoadDataAsync();
        }
    }
}
