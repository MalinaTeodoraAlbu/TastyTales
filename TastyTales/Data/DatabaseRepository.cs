
using SQLite;
using TastyTales.Data;
using TastyTales.Models;
using static SQLite.SQLite3;

namespace TastyTales.Data
{
    public class DatabaseRepository : IRepository
    {
        private SQLiteAsyncConnection connection;

        private async Task Initialize()
        {
            if (connection is null)
            {
                connection = new SQLiteAsyncConnection(
                    Path.Combine(FileSystem.AppDataDirectory, Utilities.Constants.DatabaseFile),
                    SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);
                await connection.CreateTableAsync<Models.Recipe>();
            }
        }


        public async Task SaveRecipeToDB(Recipe recipe)
        {
            await Initialize();
            await connection.InsertAsync(recipe);
        }

        public async Task<IList<Recipe>> GetAllRecipesFromDB()
        {
            await Initialize();
            return await connection.Table<Models.Recipe>().ToListAsync();
        }
    }
}