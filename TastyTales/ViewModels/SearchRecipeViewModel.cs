
using System.ComponentModel;
using TastyTales.Services;

namespace TastyTales.ViewModels
{
    public class SearchRecipeViewModel : ISearchRecipeViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IDataServices service;
        private string name;
        private IList<Models.Recipe> recipes = new List<Models.Recipe>();
        private bool busy = false;
        private bool resultsFound = false;
        private bool noResultsFound = false;

        public SearchRecipeViewModel(IDataServices service)
        {
            this.service = service;
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public IList<Models.Recipe> Recipes
        {
            get
            {
                return recipes;
            }
            set
            {
                recipes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Recipes)));
            }
        }

        public bool Busy
        {
            get
            {
                return busy;
            }
            private set
            {
                busy = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Busy)));
            }
        }

        public bool ResultsFound
        {
            get
            {
                return resultsFound;
            }
            private set
            {
                resultsFound = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ResultsFound)));
            }
        }

        public bool NoResultsFound
        {
            get
            {
                return noResultsFound;
            }
            private set
            {
                noResultsFound = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NoResultsFound)));
            }
        }

        public async Task SearchName()
        {
            Busy = true;
            Recipes = await service.GetRecipesByName(Name);
            Busy = false;
            NoResultsFound = !(ResultsFound = Recipes.Count > 0);
        }

        public void ClearResults()
        {
            Recipes.Clear();
            NoResultsFound = ResultsFound = false;
        }
    }
}
