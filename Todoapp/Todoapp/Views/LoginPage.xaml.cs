using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Todoapp.Database;
using Todoapp.Models;
using Todoapp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todoapp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private static IDataStore<User> DataStore => DependencyService.Get<IDataStore<User>>();
        private UserService userService = DataStore as UserService;
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
           
            Content = new StackLayout
            {
                Children = {
                    new Entry { Keyboard = Keyboard.Create(KeyboardFlags.None) }
                    }
            };
            await Navigation.PushModalAsync(new RegisterPage());
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            User user = null;
            Content = new StackLayout
            {
                Children = {
                    new Entry { Keyboard = Keyboard.Create(KeyboardFlags.None) }
                    }
            };
            if (!String.IsNullOrWhiteSpace(EntryUserName.Text) && !String.IsNullOrWhiteSpace(EntryPassword.Text))
            {
                user = await userService.GetItemAsync(EntryUserName.Text, EntryPassword.Text);                
            }
            if(user!= null)
            {
                Application.Current.Properties.Clear();
                Application.Current.Properties["user"] = user;
                App.CurrentUser = new User
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                };
                
                await Shell.Current.GoToAsync("///ItemListPage");
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await this.DisplayAlert("Error", "Failed User Name and Password", "Yes", "Cancel");
                });
                await Navigation.PushModalAsync(new LoginPage());
            }
        }
    }
   
}