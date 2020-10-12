using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todoapp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using Todoapp.Database;

namespace Todoapp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        async private void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterPage());
        }

         async private void Button_Clicked_login(object sender, EventArgs e)
        {
            //var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            //var db = new SQLiteConnection(dbpath);
            //var myquery = db.Table<User>().Where(u =>
            //u.UserName.Equals(EntryUserName.Text) && u.Password.Equals(EntryPassword.Text)).FirstOrDefault();

            var myquery = "";

            if (myquery!=null)
            {
                Content = new StackLayout
                {
                    
                    Children = {
                    new Entry { Keyboard = Keyboard.Create(KeyboardFlags.None) }
                }
                };
                //App.Current.MainPage = new NavigationPage(new HomeListViewPage());
                //await Navigation.PushModalAsync(new HomeListViewPage());

            }

            else
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await this.DisplayAlert("Error", "Failed User Name and Password", "Yes", "Cancel");
                    if (result)
                        await Navigation.PushModalAsync(new LoginPage());
                    else
                    {
                        await Navigation.PushModalAsync(new LoginPage());
                    }
                });
            }

        }
    }
   
}