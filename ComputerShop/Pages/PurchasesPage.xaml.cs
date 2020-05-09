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
    /// Логика взаимодействия для PurchasesPage.xaml
    /// </summary>
    public partial class PurchasesPage : Page
    {
        public PurchasesPage()
        {
            InitializeComponent();
            GetPurchases();
        }

        public List<PurchasesElement> list = new List<PurchasesElement>();


        private async void GetPurchases()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT        dbo.Purchases.ID, dbo.Clients.LastName, dbo.Clients.Name, dbo.Clients.Patronymic, dbo.Employee.LastName AS Expr1, dbo.Employee.Name AS Expr2, dbo.Employee.Patronymic AS Expr3, dbo.Shops.ShopName, dbo.Purchases.Date, dbo.Purchases.Cost " +
                                      "FROM            dbo.Clients INNER JOIN" +
                                      "                         dbo.Purchases ON dbo.Clients.ID = dbo.Purchases.ClientID INNER JOIN " +
                                      "                         dbo.Employee ON dbo.Purchases.EmployeeID = dbo.Employee.ID INNER JOIN " +
                                      "                         dbo.Shops ON dbo.Purchases.ShopID = dbo.Shops.ID ";
                if (User.RoleID == "2" || User.RoleID == "5")
                    command.CommandText += "WHERE dbo.Shops.ID = " + CurrentShop.ID;

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {       
                        list.Add(new PurchasesElement(dataReader[0].ToString(), dataReader[1].ToString() + " " + dataReader[2].ToString() + " " + dataReader[3].ToString(),
                        dataReader[4].ToString() + " " + dataReader[5].ToString() + " " + dataReader[6].ToString(), dataReader[7].ToString(), dataReader[8].ToString(), dataReader[9].ToString(), ""));
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
                GetCart();
            }
        }

        /// <summary>
        /// Обработчик нажатия правой кнопкой мыши для вызова контекстного меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            CurrentPurchase.ID = item.Tag.ToString();
        }

        private async void GetCart()
        {
            SqlConnection connection = new SqlConnection();

            try
            {                
                foreach (var item in list)
                {
                    connection.ConnectionString = MainWindow.ConnectionSrting;

                    //Открываем подключение
                    await connection.OpenAsync();

                    SqlCommand command = new SqlCommand();

                    //Запрос
                    command.CommandText = "SELECT        dbo.Products.ProductName, dbo.ProductCart.Quantity " +
                        "FROM            dbo.ProductCart INNER JOIN " +
                        "dbo.Products ON dbo.ProductCart.ProductID = dbo.Products.ID " +
                        "WHERE ProductCart.ID = " + item.ID;

                    command.Connection = connection;

                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        item.ProductCart += dataReader[0].ToString() + " - " + dataReader[1].ToString() + "\n";
                    }

                    ListViewItem view = new ListViewItem();
                    view.Content = item;
                    view.Tag = item.ID;
                    Purchases.Items.Add(view);

                    connection.Close();
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
        /// Получаем данные для чека
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            CurrentCheque.Cart.Clear();

            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "EXEC GetPurchase @id =" + CurrentPurchase.ID;

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    CurrentCheque.Type = 0;
                    CurrentCheque.ID = dataReader[0].ToString();
                    CurrentCheque.Client = dataReader[1].ToString() + " " + dataReader[2].ToString()[0] + ". " + dataReader[3].ToString()[0] + ".";
                    CurrentCheque.Employee = dataReader[4].ToString() + " " + dataReader[5].ToString()[0] + ". " + dataReader[6].ToString()[0] + ".";
                    CurrentCheque.Date = dataReader[7].ToString();
                    CurrentCheque.Cost = dataReader[8].ToString();
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
                GetCarts();
            }
        }

        public async void GetCarts()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "EXEC GetPurchaseCart @id =" + CurrentPurchase.ID;

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    CurrentCheque.Cart.Add(new ProductItem(dataReader[0].ToString(), dataReader[1].ToString(), dataReader[2].ToString()));
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
                this.NavigationService.Navigate(new ChequePage());
            }
        }
    }
}
