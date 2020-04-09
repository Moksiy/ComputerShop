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
    /// Логика взаимодействия для UpdateSupplierPage.xaml
    /// </summary>
    public partial class UpdateSupplierPage : Page
    {
        public UpdateSupplierPage()
        {
            InitializeComponent();
            GetSupplierInfo();
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

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(Name.Text) &&
               !String.IsNullOrEmpty(Address.Text) &&
               !String.IsNullOrEmpty(Phone.Text))
            {
                SaveChanges();
            }
            else
            {
                if (String.IsNullOrEmpty(Name.Text))
                    Name.BorderBrush = Brushes.Red;
                if (String.IsNullOrEmpty(Address.Text))
                    Address.BorderBrush = Brushes.Red;
                if (String.IsNullOrEmpty(Phone.Text))
                    Phone.BorderBrush = Brushes.Red;
            }
        }

        /// <summary>
        /// Получаем данные о поставщике из БД
        /// </summary>
        private async void GetSupplierInfo()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT * FROM Suppliers WHERE ID = "+CurrentSupplier.ID;

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Name.Text = dataReader[1].ToString();
                    Address.Text = dataReader[2].ToString();
                    Phone.Text = dataReader[3].ToString();
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

        /// <summary>
        /// Сохраняем изменения в БД
        /// </summary>
        private async void SaveChanges()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "UPDATE Suppliers SET SupplierName = '"+Name.Text
                +"', Adress = '"+Address.Text+"', PhoneNumber = '"+Phone.Text+"' WHERE ID = "+CurrentSupplier.ID;

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
                this.NavigationService.Navigate(new SuppliersPage());
            }
        }
    }
}
