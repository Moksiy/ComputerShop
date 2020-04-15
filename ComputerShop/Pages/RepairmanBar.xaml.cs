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
    public partial class RepairmanBar : Page
    {
        public RepairmanBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Сборки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Buildings_Selected(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).Center.Navigate(new BuildingsPage());
        }

        /// <summary>
        /// Ремонты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Repairs_Selected(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).Center.Navigate(new RepairsPage());
        }

        /// <summary>
        /// Новый ремонт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddRepair_Selected(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Новая сборка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBuilding_Selected(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).Center.Navigate(new AddNewBuilding());
        }

        /// <summary>
        /// Выход из учетной записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Selected(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Вы уверены, что хотите выйти из учетной записи?", "Выход из учетной записи", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).Center.Navigate(new AuthorizationPage());
                this.NavigationService.Navigate(new StartBar());
            }
        }

        /// <summary>
        /// Клиенты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clients_Selected(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).Center.Navigate(new ClientsPage());
        }
    }
}
