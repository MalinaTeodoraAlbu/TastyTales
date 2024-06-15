using SQLite;
using TastyTales.Models;

namespace TastyTales.Data
{
    public class DatabaseRepository : IRepository
    {
        private SQLiteAsyncConnection connection;

        public async Task<IList<Recipe>> GetRecipesByCategory(string name)
        {
            await Initialize();
            return await connection.Table<Models.Recipe>()
                .Where(item => item.Category.ToLower() == name.ToLower()).ToListAsync();
        }

        public async Task<IList<Recipe>> GetRecipesByName(string name)
        {
            await Initialize();
            return await connection.Table<Models.Recipe>()
                .Where(item => item.MealName.ToLower() == name.ToLower()).ToListAsync();
        }

        public async Task SaveRecipes(IList<Recipe> items)
        {
            await Initialize();
            await connection.InsertAllAsync(items);
        }

        private async Task Initialize()
        {
            if (connection == null)
            {
                connection = new SQLiteAsyncConnection(
                    Path.Combine(FileSystem.AppDataDirectory, Utilities.Constants.DatabaseFile),
                    SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);
                await connection.CreateTableAsync<Models.Recipe>();
            }
        }
    }
}
