using System.ComponentModel;
using Xamarin.Forms;
using Todoapp.ViewModels;

namespace Todoapp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}