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
    /// Логика взаимодействия для RepairsPage.xaml
    /// </summary>
    public partial class RepairsPage : Page
    {
        public RepairsPage()
        {
            InitializeComponent();
            GetRepairs();
        }

        public List<RepairElement> list = new List<RepairElement>();

        /// <summary>
        /// Выводим сборки
        /// </summary>
        private async void GetRepairs()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT        dbo.Repairs.ID, dbo.Shops.ShopName, dbo.Clients.LastName, SUBSTRING(dbo.Clients.Name, 1, 1) AS Expr1, SUBSTRING(dbo.Clients.Patronymic, 1, 1) AS Expr2, dbo.Employee.LastName AS Expr3, " +
                                      "SUBSTRING(dbo.Employee.Name, 1, 1) AS Expr4, SUBSTRING(dbo.Employee.Patronymic, 1, 1) AS Expr5, dbo.RepairStatuses.StatusName, dbo.Repairs.Cost, dbo.Repairs.Date, dbo.Repairs.EndingDate " +
                                      "FROM            dbo.Repairs INNER JOIN " +
                                      "dbo.RepairStatuses ON dbo.Repairs.Status = dbo.RepairStatuses.ID INNER JOIN " +
                                      "dbo.Clients ON dbo.Repairs.ClientID = dbo.Clients.ID INNER JOIN " +
                                      "dbo.Shops ON dbo.Repairs.ShopID = dbo.Shops.ID INNER JOIN " +
                                      "dbo.Employee ON dbo.Repairs.EmployeeID = dbo.Employee.ID ";
                if (User.RoleID == "2" || User.RoleID == "5")
                    command.CommandText += "WHERE dbo.Shops.ID = " + CurrentShop.ID;

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    list.Add(new RepairElement(dataReader[0].ToString(), dataReader[1].ToString(),
                        dataReader[2].ToString() + " " + dataReader[3].ToString() + "." + dataReader[4].ToString() + ".",
                        dataReader[5].ToString() + " " + dataReader[6].ToString() + "." + dataReader[7].ToString() + ".",
                        dataReader[8].ToString(), dataReader[10].ToString(), dataReader[11] is DBNull ? "-" : dataReader[11].ToString(),
                        dataReader[9].ToString(), ""));
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
                            this.NavigationService.Navigate(new UpdateRepair());
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
                    command.CommandText = "SELECT        dbo.Products.ProductName, dbo.RepairCart.Quantity " +
                        "FROM            dbo.RepairCart INNER JOIN " +
                        "dbo.Products ON dbo.RepairCart.DetailID = dbo.Products.ID " +
                        "WHERE RepairCart.ID = " + item.ID;

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
    }
}
