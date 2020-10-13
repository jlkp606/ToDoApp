using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todoapp.Models;
using Todoapp.ViewModels;
using Todoapp.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todoapp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItemPage : ContentPage
    {
        public AddItemPage()
        {
            InitializeComponent();
            BindingContext = new AddItemViewModel();
        }
    }
}