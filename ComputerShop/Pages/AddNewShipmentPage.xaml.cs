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
    /// Логика взаимодействия для AddNewShipmentPage.xaml
    /// </summary>
    public partial class AddNewShipmentPage : Page
    {
        public AddNewShipmentPage()
        {
            InitializeComponent();
            GetWarehouses();
            GetProducts();
            GetSuppliers();
        }

        public static int ID { get; set; } = 0;

        /// <summary>
        /// Назад
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ShipmentsPage());
        }

        /// <summary>
        /// Получаем склады из БД
        /// </summary>
        private async void GetWarehouses()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT ID, ShopName FROM Shops WHERE ID <> 0";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Tag = Convert.ToInt32(dataReader[0]);
                    item.Content = dataReader[1].ToString();
                    Shop.Items.Add(item);
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
                command.CommandText = "SELECT dbo.Products.ID, dbo.Products.ProductName FROM dbo.Products INNER JOIN dbo.Images ON dbo.Products.ID = dbo.Images.ID";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    ShipmentProductsList.Add(new Product(Convert.ToInt32(dataReader[0]), dataReader[1].ToString()));
                    ComboBoxItem item = new ComboBoxItem();
                    item.Tag = Convert.ToInt32(dataReader[0]);
                    item.Content = dataReader[1].ToString();
                    Products.Items.Add(item);
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

        private void Quan_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        /// <summary>
        /// Поиск товара в списке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrEmpty(SearchText.Text))
            {
                Products.Items.Clear();
                foreach(Product item in ShipmentProductsList.ProductList)
                {
                    ComboBoxItem comboitem = new ComboBoxItem();
                    comboitem.Tag = item.ID;
                    comboitem.Content = item.Name;
                    Products.Items.Add(comboitem);
                }
            }else
            {
                Products.Items.Clear();
                foreach (Product item in ShipmentProductsList.ProductList)
                {
                    if (item.Name.Contains(SearchText.Text))
                    {
                        ComboBoxItem comboitem = new ComboBoxItem();
                        comboitem.Tag = item.ID;
                        comboitem.Content = item.Name;
                        Products.Items.Add(comboitem);
                    }
                }
            }
        }

        private void ResetErrors()
        {
            ErrorProducts.Content = "";
            ErrorShop.Content = "";
            ErrorSupplier.Content = "";
        }

        /// <summary>
        /// Добавляем поставку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddShipment_Click(object sender, RoutedEventArgs e)
        {
            //Сбрасываем сообщения об ошибках
            ResetErrors();

            //Сначала проверочка
            if(!String.IsNullOrEmpty(Shop.Text) &&
               ShipmentProducts.Items.Count > 0 &&
               !String.IsNullOrEmpty(Supplier.Text))
            {
                AddShipments();
            }
            else
            {
                if (String.IsNullOrEmpty(Shop.Text))
                    ErrorShop.Content = "Выберите склад поставки";
                if (ShipmentProducts.Items.Count == 0)
                    ErrorProducts.Content = "Добавьте товары поставки";
                if (String.IsNullOrEmpty(Supplier.Text))
                    ErrorSupplier.Content = "Выберите поставщика";
            }
        }

        /// <summary>
        /// Добавление товара в список поставки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            //Сначала проверяем 
            if (!String.IsNullOrEmpty(Products.Text) && !String.IsNullOrEmpty(Quan.Text))
            {
                ListViewItem item = new ListViewItem();
                item.Content = new ShipmentProductElement(Products.Text, Convert.ToInt32(Quan.Text));
                ComboBoxItem comboitem = new ComboBoxItem();
                comboitem = (ComboBoxItem)Products.SelectedItem;
                item.Tag = ID;
                ShipmentsList.list.Add(new ShipmentsListElement(ID, Convert.ToInt32(comboitem.Tag), Convert.ToInt32(Quan.Text)));
                ShipmentProducts.Items.Add(item);
                ID++;
                Products.Text = "";
                Quan.Text = "";
            }
            else
            {
                //error
            }
        }

        /// <summary>
        /// Добавляем поставку в БД
        /// </summary>
        private async void AddShipments()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                ComboBoxItem comboitem = new ComboBoxItem();
                comboitem = (ComboBoxItem)Shop.SelectedItem;

                ComboBoxItem supplier = new ComboBoxItem();
                supplier = (ComboBoxItem)Supplier.SelectedItem;

                foreach (ShipmentsListElement item in ShipmentsList.list)
                {
                    command.CommandText += "IF (SELECT COUNT(ProductID) FROM Warehouse WHERE ProductID = "+item.ProductID+" AND ID = "+comboitem.Tag+") > 0 " +
                                           "BEGIN " +
                                           "UPDATE Warehouse " +
                                           "SET Quantity = Quantity + "+item.Quan+" " +
                                           "WHERE ID = "+ comboitem.Tag+ " AND ProductID = "+item.ProductID+" " +
                                           "END; " +
                                           "ELSE " +
                                           "BEGIN " +
                                           "INSERT INTO Warehouse VALUES("+ comboitem.Tag + ","+item.ProductID+","+item.Quan+") " +
                                           "END; " +
                                           "INSERT INTO Shipments VALUES((SELECT ISNULL(MAX(ID)+1,0) FROM Shipments)" +
                                           ","+ item.ProductID + ","+ item.Quan + ","+supplier.Tag+ ",GETDATE(),"+comboitem.Tag+");";
                }

                command.Connection = connection;

                command.ExecuteNonQuery();
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
                ShipmentsList.list.Clear();
                this.NavigationService.Navigate(new ShipmentsPage());                
            }
        }

        private async void GetSuppliers()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT ID, SupplierName FROM Suppliers";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Tag = Convert.ToInt32(dataReader[0]);
                    item.Content = dataReader[1].ToString();
                    Supplier.Items.Add(item);
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
