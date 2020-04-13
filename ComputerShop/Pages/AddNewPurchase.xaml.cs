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
    /// Логика взаимодействия для AddNewPurchase.xaml
    /// </summary>
    public partial class AddNewPurchase : Page
    {
        public AddNewPurchase()
        {
            InitializeComponent();
            GetClients();
            GetProducts();
        }

        public List<Clients> List = new List<Clients>();
        public List<Costs> CostList = new List<Costs>();

        public static double Cost { get; set; } = 0;

        public static int ID { get; set; } = 0;

        /// <summary>
        /// Поиск
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            Clients.Items.Clear();
            if (String.IsNullOrEmpty(Search.Text))
            {                
                foreach(var item in List)
                {
                    ComboBoxItem combo = new ComboBoxItem();
                    combo.Tag = item.ID;
                    combo.Content = item.FIO;
                    Clients.Items.Add(combo);
                }
            }else
            {
                foreach(var item in List)
                {
                    if(item.Phone.Contains(Search.Text))
                    {
                        ComboBoxItem combo = new ComboBoxItem();
                        combo.Tag = item.ID;
                        combo.Content = item.FIO;
                        Clients.Items.Add(combo);
                    }
                }
            }
        }

        /// <summary>
        /// Получаем клиентов из БД
        /// </summary>
        private async void GetClients()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT ID, CardID, LastName, [Name], Patronymic, PhoneNumber FROM Clients";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Tag = Convert.ToInt32(dataReader[0]);
                    item.Content = dataReader[2].ToString() + " " + dataReader[3].ToString() + " " + dataReader[4].ToString();
                    Clients.Items.Add(item);
                    List.Add(new Clients(dataReader[0].ToString(), dataReader[1].ToString(), dataReader[2].ToString() + " " +
                        dataReader[3].ToString()+ " " + dataReader[4].ToString(), dataReader[5].ToString()));
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

        /// <summary>
        /// Разрешаем вводить в поле количества товара только цифры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Quan_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        /// <summary>
        /// Добавляем товар в потребительскую корзину
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProduct_Click(object sender, RoutedEventArgs e)
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
                Cost += Convert.ToInt32(Quan.Text) * GetPrice(Convert.ToInt32(comboitem.Tag));
                Products.Text = "";
                Quan.Text = "";
            }
        }

        public double GetPrice(int id)
        {
            foreach(var item in CostList)
            {
                if (item.ID == id)
                    return item.Cost;
            }
            return 0;
        }

        /// <summary>
        /// Поиск товара в списке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchProduct_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(SearchText.Text))
            {
                Products.Items.Clear();
                foreach (Product item in ShipmentProductsList.ProductList)
                {
                    ComboBoxItem comboitem = new ComboBoxItem();
                    comboitem.Tag = item.ID;
                    comboitem.Content = item.Name;
                    Products.Items.Add(comboitem);
                }
            }
            else
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


        /// <summary>
        /// Сбрасываем ошибки
        /// </summary>
        private void ResetErrors()
        {
            ErrorClient.Content = "";
            ErrorProducts.Content = "";
        }

        /// <summary>
        ///  Получаем товары из БД
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
                command.CommandText = "SELECT dbo.Products.ID, dbo.Products.ProductName, dbo.Products.Cost FROM dbo.Products INNER JOIN dbo.Images ON dbo.Products.ID = dbo.Images.ID";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    ShipmentProductsList.Add(new Product(Convert.ToInt32(dataReader[0]), dataReader[1].ToString()));
                    ComboBoxItem item = new ComboBoxItem();
                    item.Tag = Convert.ToInt32(dataReader[0]);
                    item.Content = dataReader[1].ToString();
                    Products.Items.Add(item);
                    CostList.Add(new Costs(Convert.ToInt32(dataReader[0]), Convert.ToDouble(dataReader[2])));
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

        /// <summary>
        /// Добавляем покупку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddShipment_Click(object sender, RoutedEventArgs e)
        {
            //Сбрасываем сообщения об ошибках
            ResetErrors();

            //Сначала проверочка
            if (!String.IsNullOrEmpty(Clients.Text) &&
               ShipmentProducts.Items.Count > 0)
            {
                AddPurch();
            }
            else
            {
                if (String.IsNullOrEmpty(Clients.Text))
                    ErrorClient.Content = "Выберите клиента";
                if (ShipmentProducts.Items.Count == 0)
                    ErrorProducts.Content = "Добавьте товары поставки";
            }
        }

        /// <summary>
        ///  Добавляем в БД покупки
        /// </summary>
        private async void AddPurch()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                ComboBoxItem client = new ComboBoxItem();
                client = (ComboBoxItem)Clients.SelectedItem;

                command.CommandText = "INSERT INTO Purchases VALUES((SELECT ISNULL(MAX(Purchases.ID),0) FROM Purchases) + 1," +
                                      " "+client.Tag+", "+User.ID+", "+CurrentShop.ID+", GETDATE(), "+Cost+") ";

                foreach (ShipmentsListElement item in ShipmentsList.list)
                {
                    command.CommandText += "INSERT INTO ProductCart VALUES((SELECT ISNULL(MAX(Purchases.ID),0) FROM Purchases), " +
                                           ""+item.ProductID+", "+item.Quan+") ";
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
                ShipmentsList.list.Clear();
                connection.Close();
                this.NavigationService.Navigate(new LogoPage());
            }
        }
    }
}
