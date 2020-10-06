﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todoapp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todoapp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskDetailPage : ContentPage
    {
        public TaskDetailPage()
        {
            InitializeComponent();
            BindingContext = new TaskDetailPageViewModel();

        }
    }
}