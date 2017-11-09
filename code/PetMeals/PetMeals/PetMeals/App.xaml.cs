using PetMeals.Models;
using PetMeals.Storage;
using PetMeals.Views;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PetMeals
{
    public partial class App : Application
    {
        static PetItemDatabase database;

        public App()
        {
            InitializeComponent();

            SetMainPage();
        }

        public static void SetMainPage()
        {
            Current.MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new ItemsPage(Database))
                    {
                        Title = "Browse",
                        Icon = Device.OnPlatform("tab_feed.png",null,null)
                    },
                    new NavigationPage(new AboutPage())
                    {
                        Title = "About",
                        Icon = Device.OnPlatform("tab_about.png",null,null)
                    },
                }
            };
        }

        public static PetItemDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new PetItemDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("PetsSQLite.db3"));
                }

                var _items = new List<Item>
            {
                new Item { Text = "Buy some cat food", Description="The cats are hungry"},
                new Item { Text = "Learn F#", Description="Seems like a functional idea"},
                new Item { Text = "Learn to play guitar", Description="Noted"},
                new Item { Text = "Buy some new candles", Description="Pine and cranberry for that winter feel"},
                new Item { Text = "Complete holiday shopping", Description="Keep it a secret!"},
                new Item { Text = "Finish a todo list", Description="Done"},
            };

                foreach (Item item in _items)
                {
                    database.SaveItemAsync(item);
                }

                return database;
            }
        }
    }
}
