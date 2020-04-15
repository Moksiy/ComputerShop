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
    /// Логика взаимодействия для EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage2 : Page
    {
        public EmployeesPage2()
        {
            InitializeComponent();
            GetEmployee();
        }

        public static byte[] DefaultImage { get; set; }


        /// <summary>
        /// Заполнение listview
        /// </summary>
        private async void GetEmployee()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT        TOP " +
                    " (SELECT        COUNT(ID) AS Expr1 " +
                    "FROM            dbo.Employee " +
                    "WHERE        (ID <> 0)) dbo.EmployeePhoto.ImageData, Employee_1.LastName, Employee_1.Name, Employee_1.Patronymic, dbo.Shops.ShopName, dbo.Positions.PositionName, Employee_1.ID, Employee_1.StatusID " +
                    "FROM            dbo.Employee AS Employee_1 INNER JOIN " +
                    "dbo.Positions ON Employee_1.PositionID = dbo.Positions.ID INNER JOIN " +
                    "dbo.Shops ON Employee_1.ShopID = dbo.Shops.ID LEFT OUTER JOIN " +
                    "dbo.EmployeePhoto ON Employee_1.ID = dbo.EmployeePhoto.ID " +
                    "WHERE        (Employee_1.ID <> 0) AND (Employee_1.ShopID = "+CurrentShop.ID+") " +
                    "ORDER BY Employee_1.StatusID ";
                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                EmployeeList.Items.Clear();

                while (dataReader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.Content = new EmployeeElement(dataReader[0] == DBNull.Value ? DefaultImage : (byte[])dataReader[0],
                    dataReader[1].ToString(), dataReader[2].ToString(), dataReader[3].ToString(),
                    dataReader[4].ToString(), dataReader[5].ToString());
                    item.Tag = dataReader[6].ToString();
                    if (Convert.ToInt32(dataReader[7]) == 1)
                        item.Background = Brushes.LightYellow;
                    item.BorderBrush = Brushes.LightGray;
                    EmployeeList.Items.Add(item);
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
        /// Редактировать данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new UpdateEmployeePage());
        }
    }
}
