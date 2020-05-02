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
    /// Логика взаимодействия для AddNewSupplierPage.xaml
    /// </summary>
    public partial class AddNewSupplierPage : Page
    {
        public AddNewSupplierPage()
        {
            InitializeComponent();
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
        /// Добавить поставщика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrEmpty(Name.Text)&&
               !String.IsNullOrEmpty(Address.Text)&&
               !String.IsNullOrEmpty(Phone.Text))
            {
                AddSupplier();
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
        /// Добавляем потсавщика в БД
        /// </summary>
        private async void AddSupplier()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = @"EXEC AddNewSupplier @name, @address, @phone";

                command.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 50);

                command.Parameters.Add("@address", System.Data.SqlDbType.VarChar, 100);

                command.Parameters.Add("@phone", System.Data.SqlDbType.VarChar, 20);

                command.Parameters["@name"].Value = Name.Text;

                command.Parameters["@address"].Value = Address.Text;

                command.Parameters["@phone"].Value = Phone.Text;

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
