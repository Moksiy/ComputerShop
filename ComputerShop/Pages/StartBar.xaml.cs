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
    public partial class StartBar : Page
    {
        public StartBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Сообщения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Messages_Selected(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).Center.Navigate(new MessagePage());
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Authorization_Selected(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).Center.Navigate(new AuthorizationPage());
        }
    }
}
