using AuthorizationApp.src.Entities;
using AuthorizationApp.src.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AuthorizationApp.src.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand Authorize { get; set; }
        public ICommand ChangePasswordVisibility { get; set; }
        private readonly RxLoyaltyService _rxLoyalty;

        private string title = "Добро пожаловать!";
        private string login = string.Empty;
        private string password = string.Empty;
        private bool isPassword = true;

        public string Login
        {
            get { return login; }
            set
            {
                if (login == value)
                    return;
                login = value;
                OnPropertyChanged("Login");
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                if (password == value)
                    return;
                password = value;
                OnPropertyChanged("Password");
            }
        }
        public bool IsPassword
        {
            get { return isPassword; }
            set
            {
                if (isPassword == value)
                    return;
                isPassword = value;
                OnPropertyChanged("IsPassword");
            }
        }
        public string Title
        {
            get { return title; }
            set
            {
                if (title == value)
                    return;
                title = value;
                OnPropertyChanged("Title");
            }
        }

        public MainViewModel(RxLoyaltyService rxLoyalty) 
        {
            _rxLoyalty = rxLoyalty;
            Authorize = new Command(execute: () => PrintABOBA());
            ChangePasswordVisibility = new Command(execute: ()=> IsPassword = !IsPassword );
        }

        public async void PrintABOBA()
        {
            var account = new Account()
            {
                Login = Login,
                Password = Password
            };
            try
            {
                await _rxLoyalty.Authorize(account);
                Title = $"Добро пожаловать {_rxLoyalty.accountInfo.Client.FullName}";
                
                
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка!", ex.Message, "Ок");
            }
        }

        public void OnPropertyChanged([CallerMemberName] string prop ="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
