using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Todoapp.Database;
using Todoapp.Models;
using Xamarin.Forms;

namespace Todoapp.Services
{
	public class ItemService : IDataStore<Item>
	{
        static SQLiteAsyncConnection Database = App.Database.DatabaseInstance;        
        public Task<List<Item>> GetItemsAsync(string condition ="")
        {
            if(!string.IsNullOrWhiteSpace(condition))
                return Database.QueryAsync<Item>($"SELECT * FROM [Item] WHERE {condition}");
            return Database.Table<Item>().ToListAsync();
        }
        public Task<List<Item>> GetItemsBacklogAsync()
        {
            return Database.QueryAsync<Item>("SELECT * FROM [Item] WHERE [Status] = 0");
        }
        public Task<List<Item>> GetItemsInProgressAsync()
        {
            return Database.QueryAsync<Item>("SELECT * FROM [Item] WHERE [Status] = 1");
        }
        public Task<List<Item>> GetItemsDueAsync()
        {
            return Database.QueryAsync<Item>($"SELECT * FROM [Item] WHERE [DueDate] <= '{DateTime.Now.Date.AddDays(3.0):yyyy-MM-dd}'");
        }
        public Task<List<Item>> GetItemsDoneAsync()
        {
            return Database.QueryAsync<Item>("SELECT * FROM [Item] WHERE [Status] = 2");
        }
        public Task<List<Item>> GetItemsCurrentAsync()                                                                                                                                                                                                                                                                                  
        {
            return Database.QueryAsync<Item>("SELECT * FROM [Item] WHERE [Status] != 2 AND [Status] IS NOT NULL");
        }

        public Task<Item> GetItemAsync(int id)
        {                               
            return Database.Table<Item>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> AddItemAsync(Item item)
        {
            if (item.Id == 0)
            {
                return Database.InsertAsync(item);
            }
            return null;
        }

        public Task<int> UpdateItemAsync(Item item)
        {
            if (item.Id != 0)
            {
                return Database.UpdateAsync(item);
            }
            return null;
        }
        public Task<int> DeleteItemAsync(Item item)
        {
            return Database.DeleteAsync(item);
        }

        public enum ItemStatus
		{
            BackLog = 0,
            InProgress = 1,
            Done =2
		}
    }
}
