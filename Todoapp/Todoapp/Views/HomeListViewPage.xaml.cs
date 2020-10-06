﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Todoapp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Todoapp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeListViewPage : TabbedPage
    {
        public HomeListViewPage()
        {
            InitializeComponent();
            BindingContext = new HomeListViewPageViewModel();
            
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ItemListPage());
        }

        async private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushModalAsync(new TaskDetailPage());
        }
    }
}
