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
    public class UserService : IDataStore<User>
    {
        static SQLiteAsyncConnection Database = App.Database.DatabaseInstance;
        public Task<List<User>> GetItemsAsync(string condition = "")
        {
            if (!string.IsNullOrWhiteSpace(condition))
                return Database.QueryAsync<User>($"SELECT * FROM [User] WHERE {condition}");
            return Database.Table<User>().ToListAsync();
        }
        public Task<User> GetItemAsync(int id)
        {
            return Database.Table<User>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<User> GetItemAsync(string username)
        {
            return Database.Table<User>().Where(i => i.UserName == username).FirstOrDefaultAsync();
        }
        public Task<User> GetItemAsync(string username, string password)
        {
            return Database.Table<User>().Where(i => i.UserName == username && i.Password == password).FirstOrDefaultAsync();
        }

        public Task<int> AddItemAsync(User user)
        {
            if (user.Id == 0)
            {
                return Database.InsertAsync(user);
            }
            return null;
        }

        public Task<int> UpdateItemAsync(User user)
        {
            if (user.Id != 0)
            {
                return Database.UpdateAsync(user);
            }
            return null;
        }
        public Task<int> DeleteItemAsync(User user)
        {
            return Database.DeleteAsync(user);
        }
    }
}
