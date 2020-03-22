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
    /// Логика взаимодействия для MoreInfoShopPage.xaml
    /// </summary>
    public partial class MoreInfoShopPage : Page
    {
        public MoreInfoShopPage()
        {
            InitializeComponent();
            GetShipments();
        }

        /// <summary>
        /// назад
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private async void GetShipments()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT dbo.Shipments.ID, dbo.Products.ProductName," +
                    " dbo.Shipments.Quantity, dbo.Suppliers.SupplierName, dbo.Shipments.Date" +
                    " FROM     dbo.Shipments INNER JOIN dbo.Products ON dbo.Shipments.ProductID = dbo.Products.ID" +
                    " INNER JOIN dbo.Suppliers ON dbo.Shipments.SupplierID = dbo.Suppliers.ID " +
                    " WHERE dbo.Shipments.WarehouseID = " + CurrentShop.ID;

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.Tag = dataReader[0];
                    item.Content = (new ShipmentElement(Convert.ToInt32(dataReader[0]),
                    dataReader[1].ToString(), Convert.ToInt32(dataReader[2]),
                    dataReader[3].ToString(), Convert.ToDateTime(dataReader[4])));
                    ShipmentsList.Items.Add(item);
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
    }
}
