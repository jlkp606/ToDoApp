using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Todoapp.Database;
using Xamarin.Forms;

namespace Todoapp.ViewModels
{
    public class HomeNewTasksViewModel : ContentPage
    {
        public ICommand AddEmployeeCommand => new Command(AddEmployee);

        public ICommand RemoveEmployeeCommand => new Command(RemoveEmployee);

        public ICommand UpdateEmployeeCommand => new Command(UpdateEmployee);

        public string UnitName { get; set; } = "CAB403";

        public ObservableCollection<DisplayTask> DisplayTasks { get; set; }
        public static void DoSomeDataAccess()
        {
            //var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            //var db = new SQLiteConnection(dbpath);
            //db.CreateTable<Item>();
            //var newTaskTable = new Item();

            //abc = newTaskTable.TaskName;

        }

        public HomeNewTasksViewModel()
        {
            DisplayTasks = new ObservableCollection<DisplayTask>();
            DisplayTasks.Add(new DisplayTask("Assignment 3 User Stories", "Due: 1 days!!!"));

        }
        public void AddEmployee()
        {

        }

        public void UpdateEmployee()
        {

        }

        public void RemoveEmployee()
        {

        }
    }
}
    






        


        



       
        





