using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todoapp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todoapp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeNewTasks : ContentPage
    {
        public HomeNewTasks()
        {
            InitializeComponent();
            BindingContext = new HomeNewTasksViewModel();
        }

       async private void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ItemListPage());
        }

        async private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushModalAsync(new TaskDetailPage());
        }
    }
}