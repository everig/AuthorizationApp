using AuthorizationApp.src.Services;
using AuthorizationApp.src.ViewModels;

namespace AuthorizationApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel view)
        {
            InitializeComponent();
            BindingContext = view;
        }
    }
}
