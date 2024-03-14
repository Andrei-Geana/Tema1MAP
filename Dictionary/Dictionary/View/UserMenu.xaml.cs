using Dictionary.Model;
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
    /// Interaction logic for UserMenu.xaml
    /// </summary>
    public partial class UserMenu : Window
    {
        public UserMenu()
        {
            InitializeComponent();
        }

        private void ComboBoxWords_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserMenuViewModel currentViewModel = (this.DataContext) as UserMenuViewModel;
            if (currentViewModel.SearchText.Length > 0)
            {
                (sender as ComboBox).IsDropDownOpen = true;
            }
            currentViewModel.OnPropertyChanged(nameof(currentViewModel.FilteredWords));
        }

        private void ComboBoxWords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserMenuViewModel currentViewModel = (this.DataContext) as UserMenuViewModel;
            currentViewModel.OnPropertyChanged(nameof(currentViewModel.FilteredWords));
            currentViewModel.OnPropertyChanged(nameof(currentViewModel.CurrentWord));
        }

        private void ComboBoxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserMenuViewModel currentViewModel = (this.DataContext) as UserMenuViewModel;
            string newCategory = (e.AddedItems[0] as string);
            currentViewModel.Category = newCategory;
            currentViewModel.OnPropertyChanged(nameof(currentViewModel.FilteredWords));

        }
    }
}
