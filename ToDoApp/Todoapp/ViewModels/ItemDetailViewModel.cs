using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Todoapp.Models;
using Todoapp.Services;
using Todoapp.Views;
using Xamarin.Forms;


namespace Todoapp.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private static IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
        private ItemService itemService = DataStore as ItemService;
        private string taskName = "Assignment 1 User Stories";
        private string unitName = "IFB295";
        private string id = "0";
        private string dueDate = "03/10/2020";
        private string status = "0";
        private int estHours = 40;
        private string description = "Create a React App";
        private string resource = "https://docs.microsoft.com/en-us/xamarin/android/data-cloud/data-access/using-sqlite-orm";



        public string TaskName
        {
            get => taskName;
            set => SetProperty(ref taskName, value);
        }

        public string UnitName
        {
            get => unitName;
            set => SetProperty(ref unitName, value);
        }

        public string DueDate
        {
            get => dueDate;
            set => SetProperty(ref dueDate, value);
        }
        public string Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }
        public int EstHours
        {
            get => estHours;
            set => SetProperty(ref estHours, value);
        }
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public string Resource
        {
            get => resource;
            set => SetProperty(ref resource, value);
        }
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                LoadItemId(int.Parse(value));
            }
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await itemService.GetItemAsync(itemId);
                Id = item.Id.ToString();
                UnitName = item.UnitName;
                TaskName = item.TaskName;
                Status = item.Status;
                DueDate = item.DueDate;
                EstHours = item.EstHours;
                Description = item.Description;
                Resource = item.Resource;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}