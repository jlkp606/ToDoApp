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
    public class ItemListViewModel : BaseViewModel
    {
		private static IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
		private ItemService itemService = DataStore as ItemService;
		private Item _selectedItem;
		private string status;
		private List<Item> itemList = new List<Item>();
		
		public ObservableCollection<ItemGroup> Items { get; set; }
		public ObservableCollection<Item> UnitItems { get; set; }
		public Command LoadItemsCurrentCommand { get; }
		public Command LoadItemsDoneCommand { get; }
		public Command LoadItemsDueCommand { get; }
		public Command LoadItemsInProgressCommand { get; }
		public Command LoadItemsBacklogCommand { get; }
		public Command AddItemCommand { get; }
		public Command DeleteItemCommand { get; }
		public Command<Item> ItemTapped { get; }

		public Command<Item> MoveToDoneCommand { get; }
		public Command<Item>  MoveToInProgressCommand { get; }
		public Command SignOutCommand { get; }
		public ItemListViewModel()
        {
			Title = "Task List";
			Items = new ObservableCollection<ItemGroup>();
			LoadItemsCurrentCommand = new Command(async () => await ExecuteLoadItemsCurrentCommand());
			LoadItemsDoneCommand = new Command(async () => await ExecuteLoadItemsDoneCommand());
			LoadItemsDueCommand = new Command(async () => await ExecuteLoadItemsDueCommand());
			LoadItemsInProgressCommand = new Command(async () => await ExecuteLoadItemsInProgressCommand());
			LoadItemsBacklogCommand = new Command(async () => await ExecuteLoadItemsBacklogCommand());
			MoveToDoneCommand = new Command<Item>(OnMoveToDone);
			MoveToInProgressCommand = new Command<Item>(OnMoveToInProgress);
			DeleteItemCommand = new Command<Item>(DeleteItem);
			SignOutCommand = new Command(async () => await ExecuteSignOutCommand());
			ItemTapped = new Command<Item>(OnItemSelected);

			AddItemCommand = new Command(OnAddItem);
		}
		public Item SelectedItem
		{
			get => _selectedItem;
			set
			{
				SetProperty(ref _selectedItem, value);
				//OnItemSelected(value);
			}
		}

		public string Status
		{
			get => status;
			set
			{
				SetProperty(ref status, value);
				//OnItemSelected(value);
			}
		}

		
		async Task ExecuteLoadItemsCurrentCommand()
		{
			IsBusy = true;

			try
			{
				Items.Clear();
				var units = await itemService.GetUnitAsync();
				foreach (var unit in units)
				{

					var unititems = await itemService.GetItemsCurrentAsync(unit);
					itemList.Clear();
					foreach (var item in unititems)
					{
						itemList.Add(item);
					}
                    if (itemList.Count > 0)
                    {
						ItemGroup unitItems = new ItemGroup(unit, itemList);
						Items.Add(unitItems);
					}
					
				}

			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

		async Task ExecuteLoadItemsDoneCommand()
		{
			IsBusy = true;

			try
			{
				Items.Clear();
				var units = await itemService.GetUnitAsync();
				foreach (var unit in units)
				{
					var unititems = await itemService.GetItemsDoneAsync(unit);
					itemList.Clear();
					foreach (var item in unititems)
					{
						itemList.Add(item);
					}
					if (itemList.Count > 0)
					{
						ItemGroup unitItems = new ItemGroup(unit, itemList);
						Items.Add(unitItems);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}
		async Task ExecuteLoadItemsDueCommand()
		{
			IsBusy = true;

			try
			{
				Items.Clear();
				var units = await itemService.GetUnitAsync();
				foreach (var unit in units)
				{
					var unititems = await itemService.GetItemsDueAsync(unit);
					itemList.Clear();
					foreach (var item in unititems)
					{
						itemList.Add(item);
					}
					if (itemList.Count > 0)
					{
						ItemGroup unitItems = new ItemGroup(unit, itemList);
						Items.Add(unitItems);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}
		async Task ExecuteLoadItemsInProgressCommand()
		{
			IsBusy = true;

			try
			{
				Items.Clear();				
				var units = await itemService.GetUnitAsync();
				foreach (var unit in units)
				{
					var unititems = await itemService.GetItemsInProgressAsync(unit);
					itemList.Clear();
					foreach (var item in unititems)
					{
						itemList.Add(item);
					}
					if (itemList.Count > 0)
					{
						ItemGroup unitItems = new ItemGroup(unit, itemList);
						Items.Add(unitItems);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}
		async Task ExecuteLoadItemsBacklogCommand()
		{
			IsBusy = true;

			try
			{
				Items.Clear();
				var units = await itemService.GetUnitAsync();
				foreach (var unit in units)
				{
					var unititems = await itemService.GetItemsBacklogAsync(unit);
					itemList.Clear();
					foreach (var item in unititems)
					{
						itemList.Add(item);
					}
					if (itemList.Count > 0)
					{
						ItemGroup unitItems = new ItemGroup(unit, itemList);
						Items.Add(unitItems);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}
		public void OnAppearing()
		{
			IsBusy = true;
			SelectedItem = null;
		}

	


		private async void OnAddItem(object obj)
		{
			await Shell.Current.GoToAsync(nameof(AddItemPage));
		}

		private void OnMoveToDone(object obj)
		{
			Item item = obj as Item;			
			item.Status = ((int)ItemService.ItemStatus.Done).ToString();
			ExecuteUpdateItem(item);
		}

		private void OnMoveToInProgress(object obj)
		{
			Item item = obj as Item;
			item.Status = ((int)ItemService.ItemStatus.InProgress).ToString();
			ExecuteUpdateItem(item);
		}
		private async void ExecuteUpdateItem(object obj)
		{
			IsBusy = true;
			Item item = obj as Item;
			try
			{
				await (DataStore as ItemService).UpdateItemAsync(item);
				Item.RemoveItem(Items, item);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			finally
			{
				IsBusy = false;
			}
		}

		private async void DeleteItem(object obj)
		{
			bool result = await App.Current.MainPage.DisplayAlert("Warning", "Do you really want to delete this item?", "Yes", "Cancel");
			if (result)
			{
				IsBusy = true;
				Item item = obj as Item;
				try
				{
					await (DataStore as ItemService).DeleteItemAsync(item);
					Item.RemoveItem(Items, item);
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex);
				}
				finally
				{
					IsBusy = false;
				}
			}
		}

		private async Task ExecuteSignOutCommand()
        {
			Application.Current.Properties.Clear();
			App.CurrentUser = null;
			await Shell.Current.GoToAsync(nameof(LoginPage));
		}

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.Id)}={item.Id}");
        }
    }
}
