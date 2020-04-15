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
    /// Логика взаимодействия для BuildingsPage.xaml
    /// </summary>
    public partial class BuildingsPage : Page
    {
        public BuildingsPage()
        {
            InitializeComponent();
            GetBuildings();
        }

        public List<BuildingElement> list = new List<BuildingElement>();

        /// <summary>
        /// Выводим сборки
        /// </summary>
        private async void GetBuildings()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT        dbo.Buildings.ID, dbo.Shops.ShopName, dbo.Clients.LastName, SUBSTRING(dbo.Clients.Name, 1, 1) AS Expr4, SUBSTRING(dbo.Clients.Patronymic, 1, 1) AS Expr5, dbo.Employee.LastName AS Expr1, " +
                    " SUBSTRING(dbo.Employee.Name, 1, 1) AS Expr2, SUBSTRING(dbo.Employee.Patronymic, 1, 1) AS Expr3, dbo.BuildingStatuses.StatusName, dbo.Buildings.Total, dbo.Buildings.Date, dbo.Buildings.EndingDate, " +
                    "dbo.Buildings.Guarantee " +
                    "FROM            dbo.Buildings INNER JOIN " +
                    "dbo.BuildingStatuses ON dbo.Buildings.BuildingStatus = dbo.BuildingStatuses.ID INNER JOIN " +
                    "dbo.Clients ON dbo.Buildings.ClientID = dbo.Clients.ID INNER JOIN " +
                    "dbo.Employee ON dbo.Buildings.EmployeeID = dbo.Employee.ID INNER JOIN " +
                    "dbo.Shops ON dbo.Employee.ShopID = dbo.Shops.ID ";
                if (User.RoleID == "2" || User.RoleID == "5")
                    command.CommandText += "WHERE dbo.Shops.ID = " + CurrentShop.ID;

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    list.Add(new BuildingElement(dataReader[0].ToString(), dataReader[1].ToString(),
                        dataReader[2].ToString() + " " + dataReader[3].ToString() + "." + dataReader[4].ToString() + ".",
                        dataReader[5].ToString() + " " + dataReader[6].ToString() + "." + dataReader[7].ToString() + ".",
                        dataReader[8].ToString(), dataReader[10].ToString(), dataReader[11] is DBNull ? "-":dataReader[11].ToString(),
                        dataReader[12] is DBNull ? "-" : dataReader[12].ToString(), dataReader[9].ToString(), ""));
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
        /// Детали сборки
        /// </summary>
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
                    command.CommandText = "SELECT        dbo.Products.ProductName, dbo.BuildingCart.Quantity " +
                        "FROM            dbo.BuildingCart INNER JOIN " +
                        "dbo.Products ON dbo.BuildingCart.DetailID = dbo.Products.ID " +
                        "WHERE BuildingCart.ID = " + item.ID;

                    command.Connection = connection;

                    SqlDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        item.Cart += dataReader[0].ToString() + "   :  " + dataReader[1].ToString() + "\n";
                    }

                    Buildings.Items.Add(item);

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
