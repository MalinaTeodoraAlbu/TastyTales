using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyTales.ViewModels
{
    public interface IMainPage : INotifyPropertyChanged
    {
        IList<Models.Recipe> PopularDeserts { get; }
        IList<Models.Recipe> AllMeals { get; }
    }
}
