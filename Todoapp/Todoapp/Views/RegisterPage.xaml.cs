using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Todoapp.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todoapp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            //var db = new SQLiteConnection(dbpath);
            //db.CreateTable<User>();

            //var item = new User()
            //{
            //    UserName = EntryUserName.Text,
            //    Email = EntryEmail.Text,
            //   Password = EntryPassword.Text
            //};

            //db.Insert(item);
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Congratulation", "User Registration Successful", "Yes","Cancel");

                if (result)
                    await Navigation.PushModalAsync(new LoginPage());
            });
        }
    }
}