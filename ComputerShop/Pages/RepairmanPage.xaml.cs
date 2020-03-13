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
    /// Логика взаимодействия для RepairmanPage.xaml
    /// </summary>
    public partial class RepairmanPage : Page
    {
        public RepairmanPage()
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
        /// Просмотр содержимого склада
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Warehouse_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Просмотр ремонтов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Repairs_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Просмотр сборок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Buildings_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
