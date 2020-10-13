using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Todoapp.Models;
using Todoapp.Services;
using Xamarin.Forms;

namespace Todoapp.ViewModels
{
    public class AddItemViewModel : BaseViewModel
    {
		private string unitName;
		private string taskName;
		private string description;
		private string status;
		private string dueDate = DateTime.Now.Date.AddDays(10.0).ToString("yyyy-MM-dd");
		private string resource;
		private IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
		public AddItemViewModel()
        {
			SaveCommand = new Command(OnSave, ValidateSave);
			CancelCommand = new Command(OnCancel);
			this.PropertyChanged +=
				(_, __) => SaveCommand.ChangeCanExecute();
		}

		private bool ValidateSave()
		{
			return !String.IsNullOrWhiteSpace(unitName)
				&& !String.IsNullOrWhiteSpace(taskName);
		}

		public string TaskName
		{
			get => taskName;
			set => SetProperty(ref taskName, value);
		}

		public string UnitName
		{
			get => unitName;
			set => SetProperty(ref unitName, value);
		}

		public string Description
		{
			get => description;
			set => SetProperty(ref description, value);
		}
		public string Status
		{
			get => status;
			set => SetProperty(ref status, value);
		}

		public String DueDate
		{
			get => dueDate;
			set => SetProperty(ref dueDate, value);
		}

		public string Resource
		{
			get => resource;
			set => SetProperty(ref resource, value);
		}

		public Command SaveCommand { get; }
		public Command CancelCommand { get; }

		private async void OnCancel()
		{
			// This will pop the current page off the navigation stack
			await Shell.Current.GoToAsync("..");
		}

		private async void OnSave()
		{
			int userId = 1;
			if(App.CurrentUser != null)
				userId = App.CurrentUser.Id;
			Item newItem = new Item()
			{
				UnitName = UnitName,
				TaskName = TaskName,
				Status = "0",
				DueDate = DueDate,
				Description = Description,
				Resource =Resource,
				UserId = userId
			};

			await DataStore.AddItemAsync(newItem);

			// This will pop the current page off the navigation stack
			await Shell.Current.GoToAsync("..");
		}
	}
}