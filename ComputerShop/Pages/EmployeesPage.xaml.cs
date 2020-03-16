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
    public partial class EmployeesPage : Page
    {
        public EmployeesPage()
        {
            InitializeComponent();
            GetEmployee();
        }

        public static byte[] DefaultImage { get; set; }

        /// <summary>
        /// На предыдущую страницу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }


        /// <summary>
        /// Уволить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Обработчик нажатия правой кнопкой мыши для вызова контекстного меню
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        /// <summary>
        /// Назначить сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddNewEmployeePage());
        }

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
                connection.Open();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT * FROM GetEmployee";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {

                    ListViewItem item = new ListViewItem();
                    item.Content = new EmployeeElement(dataReader[0] == DBNull.Value ? DefaultImage : (byte[])dataReader[0],
                    dataReader[1].ToString(), dataReader[2].ToString(), dataReader[3].ToString(),
                    dataReader[4].ToString(), dataReader[5].ToString());
                    item.Tag = dataReader[6].ToString();
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
    }
}
