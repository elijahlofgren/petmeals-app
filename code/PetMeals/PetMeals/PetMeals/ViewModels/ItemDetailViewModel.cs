using System;
using System.Diagnostics;
using System.Threading.Tasks;
using PetMeals.Helpers;
using PetMeals.Models;
using PetMeals.Storage;
using Xamarin.Forms;

namespace PetMeals.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        private PetItemDatabase _database;

        public ObservableRangeCollection<Feeding> Feedings { get; set; }
        public Command LoadFeedingsCommand { get; set; }

        public Item Item { get; set; }
        public ItemDetailViewModel(Item item, PetItemDatabase database)
        {
            _database = database;
            Feedings = new ObservableRangeCollection<Feeding>();
            Title = item.Text;
            Item = item;
            LoadFeedingsCommand = new Command(async () => await ExecuteLoadFeedingsCommand());

        }

        int quantity = 1;
        public int Quantity
        {
            get { return quantity; }
            set { SetProperty(ref quantity, value); }
        }


        public async Task ExecuteLoadFeedingsCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Feedings.Clear();
                var feedings = await _database.GetFeedingsAsync(Item.ID);
                Feedings.ReplaceRange(feedings);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load feedings.",
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