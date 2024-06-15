using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TastyTales.Services
{
    public interface IDataServices
    {
        Task<IList<Models.Recipe>> GetRecipesByName(string name);
    }
}
