﻿using OnlineShop.Client.Common;
using OnlineShop.Client.Services;
using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private void SignInCommandExecute(object obj)
        {
            authService.SignIn(Login, Password);
            if(authService.AuthenticationToken == null)
            {
                messageService.ShowMessage("Unable to log in. Please check that you have entered your login and password correctly.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
            else
            {
                UserDto user = userService.GetUser(authService.AuthenticationToken.UserId);
                if(user.Rank == RankDto.Client)
                {
                    Messenger.Default.Send<WindowMessege, UserDto>(WindowMessege.OpenOrdersWindow, user);
                }

                Messenger.Default.Send<WindowMessege, object>(WindowMessege.CloseAuthenticationWindow, null);
            }
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
            Messenger.Default.Send<WindowMessege, object>(WindowMessege.CloseAuthenticationWindow, null);
        }
        #endregion
    }
}