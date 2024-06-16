using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyTales.ViewModels
{
    public interface ICategoryPageViewModel : INotifyPropertyChanged
    {
        IList<Models.Recipe> RecipesByCategory { get; }
        Models.Category SelectedCategory { get; }
    }
}