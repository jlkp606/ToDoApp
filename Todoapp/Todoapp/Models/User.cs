
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Todoapp.Models
{

    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }

}
