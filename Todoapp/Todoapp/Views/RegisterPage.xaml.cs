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
    public partial class RegisterPage : ContentPage
    {
        private static IDataStore<User> DataStore => DependencyService.Get<IDataStore<User>>();
        private UserService userService = DataStore as UserService;
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            User user = new User
            {
                UserName = EntryUserName.Text,
                Email = EntryEmail.Text,
                Password = EntryPassword.Text

            };
            var msg = "";
            bool success = false;
            User user1 = await userService.GetItemAsync(user.UserName);

            if (user1 != null)
            {
                msg = $"The user {user.UserName} existed in the application, please login directly!";
                success = false;
            }

            else
            {
                try
                {
                    await userService.AddItemAsync(user);
                    msg = "User Registration Successful";
                    success = true;
                }
                catch (Exception)
                {
                    success = false;
                    msg = "Registration failed!";
                }
            }


            Device.BeginInvokeOnMainThread(async () =>
            {

                var result = false;
                if(success)
                    result = await this.DisplayAlert("Congratulations", msg, "Yes", "Cancel");
                else
                    result = await this.DisplayAlert("Sorry", msg, "Yes", "Cancel");

                if (result)
                    await Shell.Current.GoToAsync("//LoginPage");
            });
        }
    }
}