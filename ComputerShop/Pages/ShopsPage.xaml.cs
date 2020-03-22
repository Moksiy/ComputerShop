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
using System.Data.SqlClient;

namespace ComputerShop
{
    /// <summary>
    /// Логика взаимодействия для ShopsPage.xaml
    /// </summary>
    public partial class ShopsPage : Page
    {
        public ShopsPage()
        {
            InitializeComponent();
            GetShops();
        }

        /// <summary>
        /// Назад
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        /// <summary>
        /// Нажатие правой кнопкой мыши по элементу listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            object obj = item.Tag;
            ContextMenu cm = this.FindName("CONTEXT") as ContextMenu;
            cm.IsOpen = true;
            CurrentShop.ID = Convert.ToInt32(obj);
        }

        /// <summary>
        /// Поставки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MoreInfoShopPage());
        }

        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Добавить новый магазин
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddShop_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddNewShopPage());
        }

        /// <summary>
        /// получаем магазины и заполняем listview
        /// </summary>
        private async void GetShops()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT * FROM GetShops";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.Tag = dataReader[0];
                    item.Content = (new ShopElement(Convert.ToInt32(dataReader[0]),
                    dataReader[1].ToString(), dataReader[2].ToString(), dataReader[3].ToString(),
                    Convert.ToString(dataReader[4].ToString() + " " +
                    dataReader[5].ToString() + " " + dataReader[6].ToString())));
                    ShopList.Items.Add(item);
                }
            }
            catch (SqlException ex)
            {
                SynchronizationErrors.New(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                //В любом случае закрываем подключение
                connection.Close();
            }
        }

        /// <summary>
        /// Склад (посмотреть товары в наличии)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
