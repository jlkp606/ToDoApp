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
        public int EstHours { get; set; }
        
        public string DueDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Resource { get; set; }
        public int UserId { get; set; }


        public static bool RemoveItem(ObservableCollection<ItemGroup> collection, Item instance)
        {
            if (collection.Count > 0)
            {
                ItemGroup itemGroup = collection.Where(i =>i.UnitName == instance.UnitName).FirstOrDefault();
                if(itemGroup!= null&&itemGroup.Count > 0)
                {
                    itemGroup.Remove(itemGroup.Where(i => i.Id == instance.Id).Single());
                    collection.Remove(collection.Where(i => i.UnitName == instance.UnitName).FirstOrDefault());
                    if (itemGroup!= null&&itemGroup.Count > 0)                        
                        collection.Add(itemGroup);
                }
            }
            return false;
        }
    }

    public class ItemGroup : List<Item>
    {
        public string UnitName { get; set; }

        public ItemGroup(string unitname, List<Item> items) : base(items)
        {
            UnitName = unitname;
        }
    }
}
