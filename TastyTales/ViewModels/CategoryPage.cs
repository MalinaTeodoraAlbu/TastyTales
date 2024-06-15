using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyTales.Models;
using TastyTales.Services;

namespace TastyTales.ViewModels
{
    public class CategoryPage : ICategoryPageViewModel
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
                OnPropertyChanged(nameof(Categories)); // Corrected property name here
            }
        }

        public CategoryPage(IDataServices service)
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
