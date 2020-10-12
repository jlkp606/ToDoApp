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
		private IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
		private Item _selectedItem;
		private string status;

		public ObservableCollection<Item> Items { get; set; }
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
        public ItemListViewModel()
        {
			Title = "Task List";
			Items = new ObservableCollection<Item>();
			LoadItemsCurrentCommand = new Command(async () => await ExecuteLoadItemsCurrentCommand());
			LoadItemsDoneCommand = new Command(async () => await ExecuteLoadItemsDoneCommand());
			LoadItemsDueCommand = new Command(async () => await ExecuteLoadItemsDueCommand());
			LoadItemsInProgressCommand = new Command(async () => await ExecuteLoadItemsInProgressCommand());
			LoadItemsBacklogCommand = new Command(async () => await ExecuteLoadItemsBacklogCommand());
			MoveToDoneCommand = new Command<Item>(OnMoveToDone);
			MoveToInProgressCommand = new Command<Item>(OnMoveToInProgress);
			DeleteItemCommand = new Command<Item>(DeleteItem);

			//ItemTapped = new Command<Item>(OnItemSelected);

			AddItemCommand = new Command(OnAddItem);
		}

		async Task ExecuteLoadItemsCurrentCommand()
		{
			IsBusy = true;

			try
			{
				Items.Clear();
				var items = await (DataStore as ItemService).GetItemsCurrentAsync();
				foreach (var item in items)
				{
					Items.Add(item);
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
				var items = await (DataStore as ItemService).GetItemsDoneAsync();
				foreach (var item in items)
				{
					Items.Add(item);
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
				var items = await (DataStore as ItemService).GetItemsDueAsync();
				foreach (var item in items)
				{
					Items.Add(item);
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
				var items = await (DataStore as ItemService).GetItemsInProgressAsync();
				foreach (var item in items)
				{
					Items.Add(item);
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
				var items = await (DataStore as ItemService).GetItemsBacklogAsync();
				foreach (var item in items)
				{
					Items.Add(item);
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


		private async void OnAddItem(object obj)
		{
			await Shell.Current.GoToAsync(nameof(AddItemPage));
		}

		private void OnMoveToDone(object obj)
		{
			Item item = obj as Item;			
			item.Status = ((int)ItemService.ItemStatus.Done).ToString();
			ExecuteUpdateItem(item);
			Item.RemoveItem(Items, item);
		}

		private void OnMoveToInProgress(object obj)
		{
			Item item = obj as Item;
			item.Status = ((int)ItemService.ItemStatus.InProgress).ToString();
			ExecuteUpdateItem(item);
			Item.RemoveItem(Items, item);
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

		


		//async void OnItemSelected(Item item)
		//{
		//	if (item == null)
		//		return;

		//	// This will push the ItemDetailPage onto the navigation stack
		//	await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
		//}
	}
}
