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
using Todoapp.ViewModels;
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
            BindingContext = new LoginViewModel();
        }

    }
}