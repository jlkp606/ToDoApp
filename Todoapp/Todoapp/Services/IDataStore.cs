using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todoapp.Services
{
    public interface IDataStore<T>
    {       
        Task<int> AddItemAsync(T item);
        Task<int> UpdateItemAsync(T item);
        Task<int> DeleteItemAsync(T item);
        Task<T> GetItemAsync(int id);
        Task<List<T>> GetItemsAsync(string condition = "");
    }
}
