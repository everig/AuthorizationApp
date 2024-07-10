using AuthorizationApp.src.Services;
using AuthorizationApp.src.ViewModels;

namespace AuthorizationApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var rxLoyalty = new RxLoyaltyService();
            var viewModel = new MainViewModel(rxLoyalty);
            MainPage = new MainPage(viewModel);
        }
    }
}
