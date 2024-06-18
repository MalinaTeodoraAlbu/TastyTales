using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyTales.ViewModels
{
    public interface IFavoritesVM : INotifyPropertyChanged
    {
        IList<Models.Recipe> FavRecipes { get; }

        Task LoadDataAsync();
    }
}
