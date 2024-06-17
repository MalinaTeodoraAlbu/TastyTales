using SQLite;
using TastyTales.Models;

namespace TastyTales.Data
{
    public class DatabaseRepository : IRepository
    {
        private SQLiteAsyncConnection connection;

        public DatabaseRepository()
        {
            Initialize().Wait();
        }

        private async Task Initialize()
        {
            if (connection is null)
            {
               /* connection = new SQLiteAsyncConnection(
                    Path.Combine(FileSystem.AppDataDirectory, Utilities.Constants.DatabaseFile),
                    SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);
                await connection.CreateTableAsync<Models.Recipe>();*/

                string dbPath = Path.Combine(FileSystem.AppDataDirectory, Utilities.Constants.DatabaseFile);
                Console.WriteLine($"Database path: {dbPath}");

                var connection = new SQLiteAsyncConnection(dbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);
                await connection.CreateTableAsync<Models.Recipe>();
            }
        }
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

        // adaugat de Cosmin
        public async Task SaveFavoriteRecipe(Recipe recipe)
        {
            await Initialize();
            await connection.InsertAsync(recipe);
        }
        
    }
}
