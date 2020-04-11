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
    /// Логика взаимодействия для WarehousePage.xaml
    /// </summary>
    public partial class WarehousePage : Page
    {
        public WarehousePage()
        {
            InitializeComponent();
            GetProducts();
        }

        /// <summary>
        /// Назад
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ShopsPage());
        }

        /// <summary>
        /// Товары на складе
        /// </summary>
        private async void GetProducts()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT        dbo.Images.ImageData, dbo.Products.ProductName, dbo.Products.Artikul, dbo.Warehouse.Quantity " +
                    "FROM            dbo.Warehouse INNER JOIN " +
                    "                         dbo.Products ON dbo.Warehouse.ProductID = dbo.Products.ID INNER JOIN " +
                    "                         dbo.Images ON dbo.Products.ID = dbo.Images.ID " +
                    "WHERE        (dbo.Warehouse.ID = "+CurrentShop.ID+")";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    ProductList.Items.Add(new WarehouseElement((byte[])dataReader[0], dataReader[1].ToString(), dataReader[2].ToString(), Convert.ToInt32(dataReader[3])));
                }
            }
            catch (SqlException ex)
            {
                connection.Close();
                SynchronizationErrors.New(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                //В любом случае закрываем подключение
                connection.Close();
            }
        }
    }
}
