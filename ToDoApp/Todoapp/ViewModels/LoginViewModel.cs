using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Todoapp.Models;
using Todoapp.Services;
using Todoapp.Views;
using Xamarin.Forms;


namespace Todoapp.ViewModels
{
	public class LoginViewModel : BaseViewModel
	{
		private static IDataStore<User> DataStore => DependencyService.Get<IDataStore<User>>();
		private UserService userService = DataStore as UserService;
		private User currentUser;
		private string userName;
		private string password;
		private string email;
		public Command LoginCommand { get; }
		public Command RegisterCommand { get; }
		public Command GotoRegisterCommand { get; }
		public Command BackToLoginCommand { get; }
		public LoginViewModel()
		{
			Title = "User Login";
			currentUser = new User();
			RegisterCommand = new Command(OnRegister);

			LoginCommand = new Command(OnLogin);
			GotoRegisterCommand = new Command(GotoRegister);
			BackToLoginCommand = new Command(BackToLogin);
		}
		private bool ValidateRegister()
		{
			return !String.IsNullOrWhiteSpace(userName)
				&& !String.IsNullOrWhiteSpace(password)
				&& !String.IsNullOrWhiteSpace(email);
		}
		private bool ValidateLogin()
		{
			return !String.IsNullOrWhiteSpace(userName)
				&& !String.IsNullOrWhiteSpace(password);
		}

		public string UserName
		{
			get => userName;
			set => SetProperty(ref userName, value);
		}

		public string Password
		{
			get => password;
			set => SetProperty(ref password, value);
		}

		public string Email
		{
			get => email;
			set => SetProperty(ref email, value);
		}

		private async void OnLogin()
		{

			User user = await userService.GetItemAsync(userName, password);
			if (user != null)
			{
				Application.Current.Properties.Clear();
				Application.Current.Properties["user"] = user;
				App.CurrentUser = user;
				Application.Current.MainPage = new AppShell();
			}
			else
			{

				await App.Current.MainPage.DisplayAlert("Error", "Wrong User Name or Password!", "Yes");
				await App.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
			}
		}
		private async void GotoRegister()
		{
			await App.Current.MainPage.Navigation.PushModalAsync(new RegisterPage());
		}
		private async void BackToLogin()
        {
			await App.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
		}
		private async void OnRegister()
		{
			User newUser = new User()
			{
				UserName = UserName,
				Password = Password,
				Email = Email
			};

			var msg = "";
			bool success = false;
			User user = await userService.GetItemAsync(newUser.UserName);

			if (user != null)
			{
				msg = $"The user {user.UserName} existed in the application, please login directly!";
				success = true;
			}

			else
			{
				try
				{
					int result = await userService.AddItemAsync(newUser);
					if(result > 0)
					{
						msg = "User Registration Successful!";
						success = true;
					}
					else
					{
						msg = "Error happens when trying to register user!";
						success = false;
					}
					
				}
				catch (Exception)
				{
					success = false;
					msg = "Registration failed!";
				}
			}

			
			if (success)
			{
				await App.Current.MainPage.DisplayAlert("Message", msg, "Yes");
				
				await App.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
			}
			else
			{
				await App.Current.MainPage.DisplayAlert("Sorry", msg, "Yes");
				await App.Current.MainPage.Navigation.PushModalAsync(new RegisterPage());
			}
		}
	}
}
