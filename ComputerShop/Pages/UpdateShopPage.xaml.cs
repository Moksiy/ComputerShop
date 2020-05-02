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
    /// Логика взаимодействия для UpdateShopPage.xaml
    /// </summary>
    public partial class UpdateShopPage : Page
    {
        public UpdateShopPage()
        {
            InitializeComponent();
            GetShopInfo();
        }

        public int DirectorID { get; set; }

        /// <summary>
        /// Назад
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        /// <summary>
        /// Созранить изменения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ResetColors();
            if (!String.IsNullOrEmpty(Name.Text) &&
               !String.IsNullOrEmpty(Address.Text) &&
               !String.IsNullOrEmpty(Phone.Text) &&
               !String.IsNullOrEmpty(Director.Text))
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
                if (String.IsNullOrEmpty(Director.Text))
                    Error.Content = "Выберите сотрудника на должность директора магазина";
            }
        }

        /// <summary>
        /// Сброс выделения
        /// </summary>
        private void ResetColors()
        {
            Name.BorderBrush = Brushes.SlateGray;
            Address.BorderBrush = Brushes.SlateGray;
            Phone.BorderBrush = Brushes.SlateGray;
            Error.Content = "";
        }

        /// <summary>
        /// Сохраняем изменения
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

                ComboBoxItem item = (ComboBoxItem)Director.SelectedItem;

                //Запрос
                command.CommandText = @"EXEC UpdateShop @name, @address, @phone, @director, @id";

                command.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 50);

                command.Parameters.Add("@address", System.Data.SqlDbType.VarChar, 50);

                command.Parameters.Add("@phone", System.Data.SqlDbType.VarChar, 20);

                command.Parameters.Add("@director", System.Data.SqlDbType.Int);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int);

                command.Parameters["@name"].Value = Name.Text;

                command.Parameters["@address"].Value = Address.Text;

                command.Parameters["@phone"].Value = Phone.Text;

                command.Parameters["@director"].Value = Convert.ToInt32(item.Tag);

                command.Parameters["@id"].Value = Convert.ToInt32(CurrentShop.ID);

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
                this.NavigationService.Navigate(new ShopsPage());
            }
        }

        /// <summary>
        /// Полуаем директоров из БД
        /// </summary>
        private async void GetDirectors()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT LastName, Name, Patronymic, ID FROM Employee WHERE (PositionID = 1 OR PositionID = 2) AND StatusID = 0";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = dataReader[0].ToString() + " " +
                    dataReader[1].ToString() + " " + dataReader[2].ToString();
                    item.Tag = Convert.ToInt32(dataReader[3]);
                    Director.Items.Add(item);                    
                }

                foreach(ComboBoxItem c in Director.Items)
                {
                    if (Convert.ToInt32(c.Tag) == DirectorID)
                        Director.SelectedItem = c;
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
        /// получаем информацию о магазине из БД
        /// </summary>
        private async void GetShopInfo()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT * FROM Shops WHERE ID = "+CurrentShop.ID;

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Name.Text = dataReader[1].ToString();
                    Address.Text = dataReader[2].ToString();
                    Phone.Text = dataReader[3].ToString();
                    DirectorID = Convert.ToInt32(dataReader[4]);
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
                GetDirectors();
            }
        }
    }
}
