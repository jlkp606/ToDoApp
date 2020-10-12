using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Todoapp.Models
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string UnitName { get; set; }
        public string TaskName { get; set; }
        //public string UserId { get; set; }
        
        public string DueDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Resource { get; set; }

        public static bool RemoveItem(ObservableCollection<Item> collection, Item instance)
        {
            if(collection.Count>0)
                return collection.Remove(collection.Where(i => i.Id == instance.Id).Single());
            return false;
        }

    }
}
