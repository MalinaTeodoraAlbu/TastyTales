using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyTales.ViewModels
{
    public interface ISearchRecipeViewModel : INotifyPropertyChanged
    {
        string Name { get; set; }

        IList<Models.Recipe> Recipes { get; }

        bool Busy { get; }

        bool ResultsFound { get; }

        bool NoResultsFound { get; }

        Task SearchName();

        void ClearResults();
    }
}
