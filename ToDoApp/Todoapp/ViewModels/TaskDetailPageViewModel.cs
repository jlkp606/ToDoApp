using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Todoapp.ViewModels
{
    public class TaskDetailPageViewModel : ContentPage
    {
        public string TaskName { get; set; } = "Assignment 1 User Stories";
        public string UnitName { get; set; } = "IFB295";
        public string DueDate { get; set; } = "03/10/2020";
        public string Status { get; set; } = "Due: 1 days!!!";
        public string EstHours { get; set; } = "40 hours";
        public string Description { get; set; } = "Create a React App";
        public string Resource { get; set; } = "https://docs.microsoft.com/en-us/xamarin/android/data-cloud/data-access/using-sqlite-orm";



        public TaskDetailPageViewModel()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Welcome to Xamarin.Forms!" }
                }
            };
        }
    }
}