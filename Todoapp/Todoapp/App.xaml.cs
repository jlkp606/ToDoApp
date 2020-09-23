using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Todoapp.Services;
using Todoapp.Views;

namespace Todoapp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            //MainPage = new AppShell();
            //MainPage = new RegisterPage();
            //MainPage = new LoginPage();
            MainPage = new ListViewPageTest();
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
