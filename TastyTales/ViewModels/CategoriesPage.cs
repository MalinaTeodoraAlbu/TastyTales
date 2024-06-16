using System.ComponentModel;
using TastyTales.Models;
using TastyTales.Services;

namespace TastyTales.ViewModels
{
    public class CategoriesPage : ICategoriesPageViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IDataServices service;
        
        private IList<Category> categories;

        public IList<Category> Categories
        {
            get { return categories; }
            set
            {
                categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        public CategoriesPage(IDataServices service)
        {
            this.service = service;
            Categories = new List<Category>();

            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            Categories = await service.GetCategories();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
