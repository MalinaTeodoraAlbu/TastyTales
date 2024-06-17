using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyTales.Models;

namespace TastyTales.ViewModels
{
     public interface IRecipeVM : INotifyPropertyChanged
    {
        Recipe Recipe { get; }

        Task SaveRecipeAsync();
    }
}
