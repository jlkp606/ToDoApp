using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todoapp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPageTest : ContentPage
    {

        public ObservableCollection<string> Items { get; set; }

        public class DisplayTasks
        {
            public string TaskName { get; set; }
            //public string UnitName { get; set; }
            public string Status { get; set; }
        }

        public class TasksGroup : List<DisplayTasks>
        {
            public string Tittle { get; set; }
            public string ShortTitle { get; set; }

            public TasksGroup(string title, string shortTitle)
            {
                Tittle = title;
                ShortTitle = shortTitle;
            }

        }


        public ListViewPageTest()
        {
            InitializeComponent();

            //Items = new ObservableCollection<string>
            //{
            //    "Assignment 1 User Stories - CAB303",
            //    "Assignment 2 React App - IAB303",
            //    "Watch lecture",
            //};

            //MyListView.ItemsSource = Items;

            MyListView.ItemsSource = new List<DisplayTasks>
            {
                
                
                    new DisplayTasks { TaskName ="Assignment 1 User Stories - CAB303",  Status="Due: 2 days!!!"},
                    new DisplayTasks { TaskName ="React Apps - IAB303", Status="Due: 10 days!!!"},
                    new DisplayTasks { TaskName ="Watch lecture - CAB201", Status="N/A"},
                              
                    new DisplayTasks { TaskName ="Assignment 3 User Stories - CAB303",  Status="Due: 1 days!!!"},
                    new DisplayTasks { TaskName ="Create Apps - CAB330", Status="Due: 10 days!!!"},
                    new DisplayTasks { TaskName ="Worksheet - CAB230", Status="N/A"}
                

            };


        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
