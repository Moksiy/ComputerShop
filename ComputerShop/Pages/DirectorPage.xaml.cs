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
    /// Логика взаимодействия для DirectorPage.xaml
    /// </summary>
    public partial class DirectorPage : Page
    {
        public DirectorPage()
        {
            InitializeComponent();
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

        /// <summary>
        /// Просмотр персонала магазина
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Employees_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Просмотр склада магазина
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Warehouse_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Просмотр поставок в магазин
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shipments_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Просмотр покупок в магазине
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Buys_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Просмотр заказов в магазине
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Orders_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
