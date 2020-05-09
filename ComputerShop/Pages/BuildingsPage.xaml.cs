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
                        dataReader[8].ToString(), dataReader[10].ToString(), dataReader[11] is DBNull ? "-" : dataReader[11].ToString(),
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
        /// Обработчик нажатия правой кнопкой мыши для вызова контекстного меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            CurrentBuilding.ID = item.Tag.ToString();
        }

        private async void CheckStatus()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT BuildingStatus FROM Buildings WHERE ID = " + CurrentBuilding.ID;

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    switch (dataReader[0].ToString())
                    {
                        case "0":
                        case "1":
                        case "2":
                            this.NavigationService.Navigate(new UpdateBuilding());
                            break;

                        default:
                            break;
                    }
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

                    ListViewItem build = new ListViewItem();
                    build.Content = item;
                    build.Tag = item.ID;

                    switch (item.Status)
                    {
                        case "Принято":
                            build.Background = Brushes.LightYellow;
                            break;
                        case "Отмена":
                            build.Background = Brushes.IndianRed;
                            break;
                        case "Готово":
                            build.Background = Brushes.LightGreen;
                            break;
                        default:
                            build.Background = Brushes.PaleTurquoise;
                            break;
                    }

                    Buildings.Items.Add(build);

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
        /// Редактирование сборки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            CheckStatus();            
        }

        /// <summary>
        /// Сформировать отчеты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MenuItem_Click_1(object sender, RoutedEventArgs e)
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
                command.CommandText = "EXEC GetBuilding @id = " + CurrentBuilding.ID;

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    CurrentCheque.Type = 2;
                    CurrentCheque.ID = dataReader[0].ToString();
                    CurrentCheque.Client = dataReader[1].ToString() +" "+ dataReader[2].ToString()[0] + ". " + dataReader[3].ToString()[0] + ".";
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

        private async void GetCarts()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "EXEC GetBuildingCart @id = " + CurrentBuilding.ID;

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
