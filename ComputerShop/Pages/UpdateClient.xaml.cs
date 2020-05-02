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
    /// Логика взаимодействия для UpdateClient.xaml
    /// </summary>
    public partial class UpdateClient : Page
    {
        public UpdateClient()
        {
            InitializeComponent();
            GetPrograms();
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

        private async void GetPrograms()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT * FROM BonusProgramms";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Tag = dataReader[0].ToString();
                    item.Content = dataReader[1].ToString();
                    Progr.Items.Add(item);
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
                GetClient();
            }
        }

        /// <summary>
        /// Получение инфы о клиенте
        /// </summary>
        private async void GetClient()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT        dbo.Clients.LastName, dbo.Clients.Name, dbo.Clients.Patronymic, dbo.BonusProgramms.ID, dbo.BonusProgramms.ProgramName, dbo.Clients.Email " +
                    "FROM            dbo.Clients INNER JOIN " +
                    "dbo.Bonuses ON dbo.Clients.CardID = dbo.Bonuses.CardID INNER JOIN " +
                    "dbo.BonusProgramms ON dbo.Bonuses.ProgrammID = dbo.BonusProgramms.ID " +
                    "WHERE dbo.Clients.ID = "+ CurrentClient.ID;

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    LastName.Text = dataReader[0].ToString();
                    FirstName.Text = dataReader[1].ToString();
                    Patronom.Text = dataReader[2].ToString();
                    foreach(ComboBoxItem item in Progr.Items)
                    {
                        if (item.Tag.ToString() == dataReader[3].ToString())
                            Progr.SelectedItem = item;
                    }
                    Email.Text = dataReader[5].ToString();
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
        /// Обновляем данные
        /// </summary>
        private async void UpdateС()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                ComboBoxItem item = (ComboBoxItem)Progr.SelectedItem;

                //Запрос
                command.CommandText = @"EXEC UpdateClient @lastname, @firstname, @patronom, @email, @id, @programmid";

                command.Parameters.Add("@lastname", System.Data.SqlDbType.VarChar, 50);

                command.Parameters.Add("@firstname", System.Data.SqlDbType.VarChar, 50);

                command.Parameters.Add("@patronom", System.Data.SqlDbType.VarChar, 50);

                command.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 50);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int);

                command.Parameters.Add("@programmid", System.Data.SqlDbType.Int);

                command.Parameters["@lastname"].Value = LastName.Text;

                command.Parameters["@firstname"].Value = FirstName.Text;

                command.Parameters["@patronom"].Value = Patronom.Text;

                command.Parameters["@email"].Value = Email.Text;

                command.Parameters["@id"].Value = Convert.ToInt32(CurrentClient.ID);

                command.Parameters["@programmid"].Value = Convert.ToInt32(item.Tag);

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

        public void Reset()
        {
            LastName.BorderBrush = Brushes.SlateGray;
            FirstName.BorderBrush = Brushes.SlateGray;
            Patronom.BorderBrush = Brushes.SlateGray;
            Email.BorderBrush = Brushes.SlateGray;
            Error.Content = "";
        }

        private void UpdateClient_Click(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrEmpty(LastName.Text)&&
               !String.IsNullOrEmpty(FirstName.Text) &&
               !String.IsNullOrEmpty(Patronom.Text) &&
               !String.IsNullOrEmpty(Progr.Text) &&
               !String.IsNullOrEmpty(Email.Text))
            {
                UpdateС();
            }
            else
            {
                if(String.IsNullOrEmpty(LastName.Text))
                    LastName.BorderBrush = Brushes.Red;
                if (String.IsNullOrEmpty(FirstName.Text))
                    FirstName.BorderBrush = Brushes.Red;
                if (String.IsNullOrEmpty(Patronom.Text))
                    Patronom.BorderBrush = Brushes.Red;
                if (String.IsNullOrEmpty(Email.Text))
                    Email.BorderBrush = Brushes.Red;
                if (String.IsNullOrEmpty(Progr.Text))
                    Error.Content = "Выберите бонусную программу";
            }
        }
    }
}
