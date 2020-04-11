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
    public partial class DirectorBar : Page
    {
        public DirectorBar()
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
            ((MainWindow)System.Windows.Application.Current.MainWindow).Center.Navigate(new AuthorizationPage());
            this.NavigationService.Navigate(new StartBar());
        }

        /// <summary>
        /// Сотрудники
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Employee_Selected(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Склад
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Warehouse_Selected(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).Center.Navigate(new WarehouseDirectorPage());
        }

        /// <summary>
        /// Поставки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shipments_Selected(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Покупки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Buyes_Selected(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Ремонты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Repairs_Selected(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Сборки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Builds_Selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
