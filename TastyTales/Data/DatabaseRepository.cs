﻿using SQLite;
using System.Xml.Linq;
using TastyTales.Models;

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


        public async Task SaveRecipes(IList<Recipe> items)
        {
            await Initialize();
            await connection.InsertAllAsync(items);
        }

        public async Task SaveRecipe(Recipe recipe)
        {
            await Initialize();
            await connection.InsertAsync(recipe);
        }

        public async Task Delete(int id)
        {
            await Initialize();
            await connection.DeleteAsync(id);
        }

        public async Task<IList<Recipe>> GetAllRecipesFromDB()
        {
            await Initialize();
            return await connection.Table<Models.Recipe>().ToListAsync();
        }
    }
}
