using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Todoapp.Services;
using Todoapp.Views;
using Todoapp.Database;
using SQLite;
using Todoapp.Models;
using Xamarin.Essentials;

namespace Todoapp
{
    public partial class App : Application
    {
        static TodoDatabase database;
        public App()
        {
            InitializeComponent();

            DependencyService.Register<Services.ItemService>();
            DependencyService.Register<Services.UserService>();
            MainPage = new AppShell();
        }


        public static TodoDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new TodoDatabase();
                }
                return database;
            }
        }
        public static User CurrentUser
        {
            get; set;
        }
		protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
