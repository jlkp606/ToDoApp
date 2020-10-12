﻿using System;
using System.Collections.Generic;
using Todoapp.ViewModels;
using Todoapp.Views;
using Xamarin.Forms;

namespace Todoapp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(ItemListPage), typeof(ItemListPage));
            Routing.RegisterRoute(nameof(AddItemPage), typeof(AddItemPage));
            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
        }

    }
}
