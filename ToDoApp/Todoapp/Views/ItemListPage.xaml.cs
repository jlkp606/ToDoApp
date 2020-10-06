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
    public partial class ItemListPage :TabbedPage
    {
        ItemListViewModel _viewModel;
        public ItemListPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ItemListViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        //async private void Button_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushModalAsync(new HomeListViewPage());
        //}

        //async private void Button_Clicked_create(object sender, EventArgs e)
        //{
            //await Navigation.PushModalAsync(new AddItemPage());
        //}

        async private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushModalAsync(new TaskDetailPage());
        }
    }
}