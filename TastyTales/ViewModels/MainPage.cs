using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using TastyTales.Models;
using TastyTales.Services;

namespace TastyTales.ViewModels
{
    public class MainPage : IMainPage
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IDataServices service;
        private IList<Recipe> popularDeserts;

        public IList<Recipe> PopularDeserts
        {
            get { return popularDeserts; }
            set
            {
                popularDeserts = value;
                OnPropertyChanged(nameof(PopularDeserts)); // Corrected property name here
            }
        }

        public MainPage(IDataServices service)
        {
            this.service = service;
            PopularDeserts = new List<Recipe>();

            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            PopularDeserts = await service.GetPopularDeserts();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
