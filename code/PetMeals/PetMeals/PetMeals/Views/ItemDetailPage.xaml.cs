
using System;
using PetMeals.Models;
using PetMeals.Storage;
using PetMeals.ViewModels;

using Xamarin.Forms;

namespace PetMeals.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        private PetItemDatabase _database;
        ItemDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ItemDetailPage()
        {
            InitializeComponent();
        }

        public ItemDetailPage(ItemDetailViewModel viewModel, 
        PetItemDatabase database)
        {
            _database = database;
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }


        async void Handle_FeedClicked(object sender, System.EventArgs e)
        {
            await _database.SaveFeedingAsync(new Feeding { PetId = viewModel.Item.ID, TimeFed = DateTime.Now });
            await viewModel.ExecuteLoadFeedingsCommand();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Feedings.Count == 0)
                viewModel.LoadFeedingsCommand.Execute(null);
        }
    }
}
