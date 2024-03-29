﻿using Dictionary.Model;
using Dictionary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dictionary.View
{
    /// <summary>
    /// Interaction logic for AdminMenu.xaml
    /// </summary>
    public partial class AdminMenu : Window
    {
        public AdminMenu(object dContext)
        {
            InitializeComponent();
            AdminMenuViewModel loginViewModel = new AdminMenuViewModel(dContext as DatabaseEmulator);
            DataContext = loginViewModel;
        }
    }
}
