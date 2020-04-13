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

            SqlConnection connection2 = new SqlConnection();

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
                                      "                         dbo.Shops ON dbo.Purchases.ShopID = dbo.Shops.ID";

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

                    Purchases.Items.Add(item);

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
    }
}
