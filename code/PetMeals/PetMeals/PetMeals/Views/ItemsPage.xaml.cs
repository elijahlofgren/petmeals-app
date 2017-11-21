using System;

using PetMeals.Models;
using PetMeals.ViewModels;

using Xamarin.Forms;
using PetMeals.Storage;

namespace PetMeals.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;
        PetItemDatabase _database;

        public ItemsPage(PetItemDatabase database)
        {
            _database = database;
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel(database);
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item), _database));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewItemPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}
