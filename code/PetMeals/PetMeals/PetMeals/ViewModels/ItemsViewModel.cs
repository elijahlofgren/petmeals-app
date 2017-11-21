using System;
using System.Diagnostics;
using System.Threading.Tasks;

using PetMeals.Helpers;
using PetMeals.Models;
using PetMeals.Storage;
using PetMeals.Views;

using Xamarin.Forms;

namespace PetMeals.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        private PetItemDatabase _database;

        public ItemsViewModel(PetItemDatabase database)
        {
            _database = database;
            Title = "Browse";
            Items = new ObservableRangeCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Item;
                //Items.Add(_item);
                //await DataStore.AddItemAsync(item);
                await _database.SaveItemAsync(_item);
                // Refresh list of items.
                await ExecuteLoadItemsCommand();
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                //var items = await DataStore.GetItemsAsync(true);
                var items = await _database.GetItemsAsync();
                Items.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}