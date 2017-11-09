namespace PetMeals.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            LoadApplication(new PetMeals.App());
        }
    }
}