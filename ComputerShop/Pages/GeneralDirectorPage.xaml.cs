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
    /// Логика взаимодействия для GeneralDirectorPage.xaml
    /// </summary>
    public partial class GeneralDirectorPage : Page
    {
        public GeneralDirectorPage()
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
        /// Посмотреть штат сотрудников
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Employees_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Посмотреть магазины
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shops_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Посмотреть поставщиков
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Suppliers_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Посмотреть поставки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shipments_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Посмотреть покупки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Buys_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Посмотреть заказы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Orders_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Посмотреть клиентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clients_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Посмотреть товары
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Products_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ProductsPage());
        }
    }
}
