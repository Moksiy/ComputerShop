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
    /// Логика взаимодействия для LogisticianPage.xaml
    /// </summary>
    public partial class LogisticianPage : Page
    {
        public LogisticianPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Просмотр товаров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Products_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ProductsPage());
        }

        /// <summary>
        /// Просмотр складов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Warehouses_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Просмотр поставщиков
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Suppliers_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Просмотр поставок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shipments_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Выход из учетной записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Account.LogOut();
            this.NavigationService.Navigate(new AuthorizationPage());
        }
    }
}
