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
        private IList<Recipe> allMeals;

        public IList<Recipe> PopularDeserts
        {
            get { return popularDeserts; }
            set
            {
                popularDeserts = value;
                OnPropertyChanged(nameof(PopularDeserts)); 
            }
        }

        public IList<Recipe> AllMeals
        {
            get { return allMeals; }
            set
            {
                allMeals = value;
                OnPropertyChanged(nameof(AllMeals));
            }
        }

        public MainPage(IDataServices service)
        {
            this.service = service;
            PopularDeserts = new List<Recipe>();
            AllMeals = new List<Recipe>();
            LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            PopularDeserts = await service.GetPopularDeserts();
            AllMeals = await service.GetAllMeals();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
