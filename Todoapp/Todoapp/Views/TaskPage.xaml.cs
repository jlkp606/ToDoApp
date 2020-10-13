using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todoapp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        private int numButtons = 4;
        private string buttonText = "My Name Jeff";

        public Page1()
        {
            InitializeComponent();
        }

        StackLayout parent;
        public void Addbutton(object sender, EventArgs e)
        {
            Button newButton = new Button { Text = buttonText };
            newButton.SetBinding(Button.CommandProperty, new Binding("ViewModelProperty"));
            newButton.BindingContext = viewModel;
            parent = layout;
            parent.Children.Add(newButton);
        }

    }
}