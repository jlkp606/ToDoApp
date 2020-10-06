using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Todoapp.Services;
using Todoapp.Views;
using Todoapp.Database;
using SQLite;

namespace Todoapp
{
    public partial class App : Application
    {
        static TodoDatabase database;
        public App()
        {
            InitializeComponent();

			DependencyService.Register<Services.ItemService>();
            //DependencyService.Register<User>();
            MainPage = new AppShell();
            //MainPage = new CompletedItemListPage();
            //MainPage = new LoginPage();
            //MainPage = new HomeListViewPage();
            //MainPage = new NewItemPage();
           //MainPage = new ItemListPage();
            //MainPage = new HomeNewTasks();
            //MainPage = new CompletedTasksPage();
            //MainPage = new CreateTaskPage();
            //MainPage = new TaskDetailPage();
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
