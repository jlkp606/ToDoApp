using SQLite;
using System;
using System.Collections.Generic;
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
        
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string Resource { get; set; }

    }
}
