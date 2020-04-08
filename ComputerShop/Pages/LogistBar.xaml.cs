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
    public partial class LogistBar : Page
    {
        public LogistBar()
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
        /// Товары
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Products_Selected(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).Center.Navigate(new ProductsPage());
        }

        /// <summary>
        /// Склады
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Warehouses_Selected(object sender, RoutedEventArgs e)
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
    }
}
