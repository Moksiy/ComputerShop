﻿using System;
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
    public partial class GeneralDirectorBar : Page
    {
        public GeneralDirectorBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Товары
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Products_Selected(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).Center.Navigate(new ProductsPage());
        }

        /// <summary>
        /// Персонал
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Employee_Selected(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).Center.Navigate(new EmployeesPage());
        }

        /// <summary>
        /// Магазины
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shops_Selected(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).Center.Navigate(new ShopsPage());
        }

        /// <summary>
        /// Поставщики
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Suppliers_Selected(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).Center.Navigate(new SuppliersPage());
        }

        /// <summary>
        /// Поставки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shipments_Selected(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).Center.Navigate(new ShipmentsPage());
        }

        /// <summary>
        /// Покупки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Buyes_Selected(object sender, RoutedEventArgs e)
        {
            //((MainWindow)System.Windows.Application.Current.MainWindow).Center.Navigate();
        }

        /// <summary>
        /// Заказы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Orders_Selected(object sender, RoutedEventArgs e)
        {
            //
        }

        /// <summary>
        /// Клиенты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clients_Selected(object sender, RoutedEventArgs e)
        {
            //
        }

        /// <summary>
        /// Выход из учетки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Selected(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).Center.Navigate(new AuthorizationPage());
            this.NavigationService.Navigate(null);
        }
    }
}
