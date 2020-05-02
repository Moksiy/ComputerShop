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
    /// Логика взаимодействия для AddNewClientPage.xaml
    /// </summary>
    public partial class AddNewClientPage : Page
    {
        public AddNewClientPage()
        {
            InitializeComponent();
            GetBonuses();
        }

        /// <summary>
        /// Добавить клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            //Проверка всех значений
            if(!String.IsNullOrEmpty(LastName.Text) &&
               !String.IsNullOrEmpty(FirstName.Text) &&
               !String.IsNullOrEmpty(Patronom.Text) &&
               !String.IsNullOrEmpty(Email.Text) &&
               !String.IsNullOrEmpty(Phone.Text) &&
               !String.IsNullOrEmpty(Bonus.Text))
            {
                AddNewClient();
            }
            else
            {
                if (String.IsNullOrEmpty(LastName.Text))
                    LastName.BorderBrush = Brushes.Red;
                if (String.IsNullOrEmpty(FirstName.Text))
                    FirstName.BorderBrush = Brushes.Red;
                if (String.IsNullOrEmpty(Patronom.Text))
                    Patronom.BorderBrush = Brushes.Red;
                if (String.IsNullOrEmpty(Email.Text))
                    Email.BorderBrush = Brushes.Red;
                if (String.IsNullOrEmpty(Phone.Text))
                    Phone.BorderBrush = Brushes.Red;
                if (String.IsNullOrEmpty(Bonus.Text))
                    BonusError.Content = "Выберите бонусную программу клиента";
            }


        }

        private void Reset()
        {
                LastName.BorderBrush = Brushes.SlateGray;
                FirstName.BorderBrush = Brushes.SlateGray;
                Patronom.BorderBrush = Brushes.SlateGray;
                Email.BorderBrush = Brushes.SlateGray;
                Phone.BorderBrush = Brushes.SlateGray;
                BonusError.Content = "";
        }

        /// <summary>
        /// Назад
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ClientsPage());
        }

        /// <summary>
        /// Получаем бонусные программы
        /// </summary>
        private async void GetBonuses()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT ID, ProgramName FROM BonusProgramms";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Tag = Convert.ToInt32(dataReader[0]);
                    item.Content = dataReader[1].ToString();
                    Bonus.Items.Add(item);
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
        /// Добавить клиента в БД
        /// </summary>
        private async void AddNewClient()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                ComboBoxItem item = (ComboBoxItem)Bonus.SelectedItem;

                //Запрос
                command.CommandText = @"EXEC AddNewClient @id, @lastname, @firstname, @patronom, @email, @phone";

                command.Parameters.Add("@id", System.Data.SqlDbType.Int);

                command.Parameters.Add("@lastname", System.Data.SqlDbType.VarChar, 50);

                command.Parameters.Add("@firstname", System.Data.SqlDbType.VarChar, 50);

                command.Parameters.Add("@patronom", System.Data.SqlDbType.VarChar, 50);

                command.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 50);

                command.Parameters.Add("@phone", System.Data.SqlDbType.VarChar, 20);

                command.Parameters["@id"].Value = Convert.ToInt32(item.Tag);

                command.Parameters["@lastname"].Value = LastName.Text;

                command.Parameters["@firstname"].Value = FirstName.Text;

                command.Parameters["@patronom"].Value = Patronom.Text;

                command.Parameters["@email"].Value = Email.Text;

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
                this.NavigationService.Navigate(new ClientsPage());
            }
        }
    }
}
