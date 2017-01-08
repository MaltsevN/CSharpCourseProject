using OnlineShop.Client.Common;
using OnlineShop.Client.Exceptions;
using OnlineShop.Client.Services;
using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace OnlineShop.Client.ViewModels
{
    class UsersWindowViewModel : BaseViewModel
    {
        private readonly IMessegeManager messageService;
        private readonly IUserService userService;
        private ICollectionView collectionView;

        public UsersWindowViewModel(IUserService userService, IMessegeManager messageService)
        {
            this.userService = userService;
            this.messageService = messageService;
            Users = new ObservableCollection<UserDto>();
            collectionView = CollectionViewSource.GetDefaultView(Users);
            collectionView.Filter = SearchFilter;
        }

        private bool SearchFilter(object obj)
        {
            UserDto user = obj as UserDto;
            if (user == null)
                return false;

            if (string.IsNullOrEmpty(SearchString))
                return true;

            return user.Name.ToLower().Contains(SearchString.ToLower()) || user.Login.ToLower().Contains(SearchString.ToLower());
        }

        public ObservableCollection<UserDto> Users { get; set; }

        private string searchString = string.Empty;
        public string SearchString
        {
            get
            {
                return searchString;
            }
            set
            {
                searchString = value;
                OnPropertyChanged(nameof(SearchString));
                collectionView.Refresh();
            }
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

        private UserDto selectedUser;
        public UserDto SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

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

        private async void WindowLoadedCommandExecute(object obj)
        {
            IsBusy = true;
            Logger.For(this).Info("UsersWindow is loading");
            try
            {
                foreach (var order in await userService.GetUsersAsync())
                {
                    Users.Add(order);
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

            Messenger.Default.Register<WindowMessege, bool?>(this, WindowMessege.AddUserWindowClosed, AddUserWindowClosed);

            IsBusy = false;
        }

        private void AddUserWindowClosed(bool? dialogResult)
        {
            if (dialogResult == true)
            {
                Users.Add(SelectedUser);
            }
            else
            {
                SelectedUser = null;
            }
        }
        #endregion

        #region OpenAddUserWindowCommand
        private RelayCommand<object, object> openAddUserWindowCommand;
        public ICommand OpenAddUserWindowCommand
        {
            get
            {
                if (openAddUserWindowCommand == null)
                    openAddUserWindowCommand = new RelayCommand<object, object>(OpenAddUserWindowCommandExecute);
                return openAddUserWindowCommand;
            }
        }

        private void OpenAddUserWindowCommandExecute(object obj)
        {
            IsBusy = true;
            Logger.For(this).Info("Open AddUserWindow");
            UserDto newUser = new UserDto() { Rank = RankDto.Client };
            SelectedUser = newUser;
            IsBusy = false;
            Messenger.Default.Send<WindowMessege, UserDto>(WindowMessege.OpenAddUserWindow, newUser);
        }
        #endregion

    }
}
