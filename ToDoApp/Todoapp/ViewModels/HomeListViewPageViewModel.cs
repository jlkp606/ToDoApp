using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Todoapp.ViewModels
{
    public class DisplayTask
    {
        public string TaskName { get; set; }
        public string Status { get; set; }
      

        public DisplayTask(string taskName, string status)
        {
            TaskName = taskName;
            Status = status;
         
        }

    }
    class HomeListViewPageViewModel : ContentPage
    {
        



        public ICommand AddEmployeeCommand => new Command(AddEmployee);

        public ICommand RemoveEmployeeCommand => new Command(RemoveEmployee);

        public ICommand UpdateEmployeeCommand => new Command(UpdateEmployee);

        

        

        public ObservableCollection<DisplayTask> DisplayTasks { get; set; }
        public string UnitName { get; set; } = "CAB403";

        public HomeListViewPageViewModel()
        {
            DisplayTasks = new ObservableCollection<DisplayTask>();

            DisplayTasks.Add(new DisplayTask("React Apps - IAB303","Due: 10 days!!!"));
            

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