using OnlineShop.Client.Common;
using OnlineShop.Client.Exceptions;
using OnlineShop.Client.Services;
using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OnlineShop.Client.ViewModels
{
    class AddUserWindowViewModel : BaseViewModel
    {
        private readonly IMessegeManager messageService;
        private readonly IUserService userService;

        public AddUserWindowViewModel(IUserService userService, IMessegeManager messageService)
        {
            this.userService = userService;
            this.messageService = messageService;
            Ranks = new ObservableCollection<RankDto>();
        }

        public UserDto User { get; set; }

        public ObservableCollection<RankDto> Ranks { get; set; }

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

        #region AddUserCommand
        private RelayCommand<object, object> addUserCommand;

        public ICommand AddUserCommand
        {
            get
            {
                if (addUserCommand == null)
                    addUserCommand = new RelayCommand<object, object>(AddUserCommandExecute, AddUserCommandCanExecute);
                return addUserCommand;
            }
        }

        private async void AddUserCommandExecute(object obj)
        {
            IsBusy = true;
            Logger.For(this).Info("Adding new user");
            try
            {
                bool isExists = (await userService.GetUsersAsync()).Any(user => user.Login.Equals(User.Login));
                if (isExists)
                {
                    messageService.ShowMessage($"The user with the login \"{User.Login}\" already exists. Please choose a different login.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                }
                else
                {
                    UserDto newUser = await userService.CreateAsync(User);
                    User.Id = newUser.Id;
                    Messenger.Default.Send<WindowMessege, bool?>(WindowMessege.CloseAddUserWindow, true);
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

        private bool AddUserCommandCanExecute(object obj)
        {
            return User != null && !string.IsNullOrEmpty(User.Login) && !string.IsNullOrEmpty(User.Name) && !string.IsNullOrEmpty(User.Password);
        }
        #endregion

        #region WindowLoadedCommand
        private RelayCommand<object, object> windowLoadedCommand;

        public ICommand WindowLoadedCommand
        {
            get
            {
                if (windowLoadedCommand == null)
                    windowLoadedCommand = new RelayCommand<object, object>(WindowLoadedCommandExecute);
                return windowLoadedCommand;
            }
        }

        private void WindowLoadedCommandExecute(object obj)
        {
            IsBusy = true;
            Logger.For(this).Info("AddUserWindow is loading");
            OnPropertyChanged(nameof(User));
            foreach (var item in Enum.GetValues(typeof(RankDto)).Cast<RankDto>())
            {
                Ranks.Add(item);
            }
            IsBusy = false;
        }
        #endregion

        #region CloseCommand
        private RelayCommand<object, object> closeCommand;

        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                    closeCommand = new RelayCommand<object, object>(CancelCommandExecute);
                return closeCommand;
            }
        }

        private void CancelCommandExecute(object obj)
        {
            Logger.For(this).Info("AddUserWindow is close");
            Messenger.Default.Send<WindowMessege, bool?>(WindowMessege.CloseAddUserWindow, false);
        }
        #endregion
    }
}