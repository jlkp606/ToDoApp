using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
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
        int userId = App.CurrentUser!= null? App.CurrentUser.Id: 0;
        public Task<List<Item>> GetItemsAsync(string condition = "")
        {
            if (!string.IsNullOrWhiteSpace(condition))
                return Database.QueryAsync<Item>($"SELECT * FROM [Item] WHERE ([UserId] = {userId} or {userId} = 0) AND {condition}");
            return Database.Table<Item>().ToListAsync();
        }
        public Task<List<Item>> GetItemsBacklogAsync(string unit)
        {
            return Database.QueryAsync<Item>($"SELECT * FROM [Item] WHERE ([UserId] = {userId} or {userId} = 0) AND [UnitName]= '{unit}' AND [Status] = 0");
        }
        public Task<List<Item>> GetItemsInProgressAsync(string unit)
        {
            return Database.QueryAsync<Item>($"SELECT * FROM [Item] WHERE ([UserId] = {userId} or {userId} = 0) AND [UnitName]= '{unit}' AND [Status] = 1");
        }
        public Task<List<Item>> GetItemsDueAsync(string unit)
        {
            return Database.QueryAsync<Item>($"SELECT * FROM [Item] WHERE ([UserId] = {userId} or {userId} = 0) AND [UnitName]= '{unit}' AND [DueDate] <= '{DateTime.Now.Date.AddDays(3.0):yyyy-MM-dd}'");
        }
        public Task<List<Item>> GetItemsDoneAsync(string unit)
        {
            return Database.QueryAsync<Item>($"SELECT * FROM [Item] WHERE ([UserId] = {userId} or {userId} = 0) AND [UnitName]= '{unit}' AND [Status] = 2");
        }
        public Task<List<Item>> GetItemsCurrentAsync(string unit)
        {
            return Database.QueryAsync<Item>($"SELECT * FROM [Item] WHERE ([UserId] = {userId} or {userId} = 0) AND [UnitName]= '{unit}' AND [Status] != 2 AND [Status] IS NOT NULL");
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
        public Task<List<String>> GetUnitAsync()
        {
            return Database.QueryScalarsAsync<String>("SELECT DISTINCT [UnitName] FROM [Item]");
        }

        public enum ItemStatus
        {
            BackLog = 0,
            InProgress = 1,
            Done = 2
        }
    }

    public class DueDateConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var dueDate = ((string)value);
            if (dueDate != null &&
                string.IsNullOrWhiteSpace(dueDate))
            {
                return "Not Specified!";
            }
            else
            {
                return $"{(DateTime.Parse(dueDate).Date - DateTime.Now.Date).TotalDays} days due!";
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StatusConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var status = ((string)value);
            if (status != null &&
                string.IsNullOrWhiteSpace(status))
            {
                return "Not Specified!";
            }
            else
            {
                ItemService.ItemStatus statusValue;
                string strStatus = "";
                if (Enum.TryParse(status, out statusValue))
                     strStatus = GetEnumDescription(statusValue);
                return $"{strStatus}";
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
