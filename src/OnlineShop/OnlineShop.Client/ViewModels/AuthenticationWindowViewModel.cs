using OnlineShop.Client.Common;
using OnlineShop.Client.Exceptions;
using OnlineShop.Client.Services;
using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OnlineShop.Client.ViewModels
{
    class AuthenticationWindowViewModel : BaseViewModel
    {
        private readonly IAuthenticationService authService;
        private readonly IMessegeManager messageService;
        private readonly IUserService userService;

        public string Login { get; set; }
        public string Password { get; set; }

        public AuthenticationWindowViewModel(IAuthenticationService authService, IMessegeManager messageService, IUserService userService)
        {
            this.authService = authService;
            this.messageService = messageService;
            this.userService = userService;
            Logger.For(this).Info("Init AuthenticationWindow");
        }

        private bool isBusy;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }


        #region SignInCommand
        private RelayCommand<object, object> signInCommand;

        public ICommand SignInCommand
        {
            get
            {
                if (signInCommand == null)
                    signInCommand = new RelayCommand<object, object>(SignInCommandExecute, SignInCommandCanExecute);
                return signInCommand;
            }
        }

        private async void SignInCommandExecute(object obj)
        {
            IsBusy = true;
            Logger.For(this).Info("Sign in");
            try
            {
                await authService.SignIn(Login, Password);
                if (authService.AuthenticationToken == null)
                {
                    Logger.For(this).Info("Unable to log in.");
                    messageService.ShowMessage("Unable to log in. Please check that you have entered your login and password correctly.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
                else
                {
                    UserDto user = await userService.GetUserAsync(authService.AuthenticationToken.UserId);
                    if (user.Rank == RankDto.Client)
                    {
                        Logger.For(this).Info("Open OrdersWindow");
                        Messenger.Default.Send<WindowMessege, UserDto>(WindowMessege.OpenOrdersWindow, user);
                    }
                    if (user.Rank == RankDto.Admin)
                    {
                        Logger.For(this).Info("Open AdminOrderWindow");
                        Messenger.Default.Send<WindowMessege, object>(WindowMessege.OpenAdminOrderWindow, null);
                    }
                    Logger.For(this).Info("Close AuthenticationWindow");
                    Messenger.Default.Send<WindowMessege, object>(WindowMessege.CloseAuthenticationWindow, null);
                }
            }
            catch (NoInternetConnectionException ex)
            {
                Logger.For(this).Error(ex.Message, ex);
                messageService.ShowMessage(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            catch (HttpRequestException ex)
            {
                Logger.For(this).Error(ex.Message, ex);
                messageService.ShowMessage(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            
            IsBusy = false;
        }

        private bool SignInCommandCanExecute(object obj)
        {
            return !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password);
        }
        #endregion

        #region CloseCommand
        private RelayCommand<object, object> closeCommand;

        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                    closeCommand = new RelayCommand<object, object>(CloseCommandExecute);
                return closeCommand;
            }
        }

        private void CloseCommandExecute(object obj)
        {
            Logger.For(this).Info("Close AuthenticationWindow");
            Messenger.Default.Send<WindowMessege, object>(WindowMessege.CloseAuthenticationWindow, null);
        }
        #endregion
    }
}
