﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Dictionary.Model;
using System.Collections.ObjectModel;
using Dictionary.Command;
using System.Collections;

namespace Dictionary.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private DatabaseEmulator emulator;

        private ObservableCollection<User> users;


        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new SubmitLoginCommand(this);
            emulator = new DatabaseEmulator();
            users = new ObservableCollection<User>(emulator.GetUsersFromFile());
        }

        public bool CanExecuteLogin => HasUsername && HasPassword && UserIsValid;
        private bool UserIsValid => users.Contains(new User(Username, Password));
        private bool HasUsername => !string.IsNullOrEmpty(Username);
        private bool HasPassword => !string.IsNullOrEmpty(Password);

    }
}