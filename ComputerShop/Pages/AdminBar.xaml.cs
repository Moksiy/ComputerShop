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

namespace ComputerShop
{
    /// <summary>
    /// Логика взаимодействия для GeneralDirectorBar.xaml
    /// </summary>
    public partial class AdminBar : Page
    {
        public AdminBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Выход из учетки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Selected(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Вы уверены, что хотите выйти из учетной записи?", "Выход из учетной записи", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                User.Clear();
                ((MainWindow)System.Windows.Application.Current.MainWindow).Center.Navigate(new AuthorizationPage());
                this.NavigationService.Navigate(new StartBar());
            }
        }

        /// <summary>
        /// Ошибки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Errors_Selected(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).Center.Navigate(new AdminPage());
        }

        /// <summary>
        /// Сообщения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Messages_Selected(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).Center.Navigate(new MessagesPage());
        }
    }
}
