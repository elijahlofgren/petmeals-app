using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using PetMeals.Models;
using System.Diagnostics;

namespace PetMeals.Storage
{
    public class PetItemDatabase
    {
        readonly SQLiteAsyncConnection database;

        public PetItemDatabase(string dbPath)
        {
            Debug.WriteLine("Preparing to create DB:");
            try
            {
                database = new SQLiteAsyncConnection(dbPath);
                database.CreateTableAsync<Item>().Wait();
            }
            catch (SQLiteException ex)
            {
                Debug.WriteLine("Error creating DB:");
                Debug.WriteLine(ex.Message);
            }
        }

        public Task<List<Item>> GetItemsAsync()
        {
            return database.Table<Item>().ToListAsync();
        }

        public Task<List<Item>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Item>("SELECT * FROM [Item]");
            //return database.QueryAsync<Item>("SELECT * FROM [Item] WHERE [Done] = 0");
        }

        /*
        public Task<Item> GetItemAsync(int id)
        {
            return database.Table<Item>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        */

        public Task<int> SaveItemAsync(Item item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Item item)
        {
            return database.DeleteAsync(item);
        }
    }
}
