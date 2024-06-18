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
    public class FavoritesVM : IFavoritesVM
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IDataServices service;

        private IList<Recipe> favRecipes;

        public IList<Recipe> FavRecipes
        {
            get { return favRecipes; }
            set
            {
                favRecipes = value;
                OnPropertyChanged(nameof(FavRecipes));
            }
        }

        public FavoritesVM(IDataServices service)
        {
            this.service = service;
            FavRecipes = new List<Recipe>();

            LoadDataAsync();
        }

        public async Task LoadDataAsync()
        {
            FavRecipes = await service.GetAllRecipesDB();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
