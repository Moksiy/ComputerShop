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
    /// Логика взаимодействия для AddNewShopPage.xaml
    /// </summary>
    public partial class AddNewShopPage : Page
    {
        public AddNewShopPage()
        {
            InitializeComponent();
            GetDirectors();
        }

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
        /// Добавить магазин
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ResetColors();
            if(!String.IsNullOrEmpty(Name.Text)&&
               !String.IsNullOrEmpty(Address.Text)&&
               !String.IsNullOrEmpty(Phone.Text)&&
               !String.IsNullOrEmpty(Director.Text))
            {
                AddShop();
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

        private void ResetColors()
        {
            Name.BorderBrush = Brushes.SlateGray;
            Address.BorderBrush = Brushes.SlateGray;
            Phone.BorderBrush = Brushes.SlateGray;
            Error.Content = "";
        }

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

        private async void AddShop()
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
                command.CommandText = "INSERT INTO Shops VALUES((SELECT ISNULL(MAX(Shops.ID)+1,0) FROM Shops), '"+
                Name.Text+"','"+Address.Text+"','"+Phone.Text+"',"+item.Tag.ToString()+")";

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
                this.NavigationService.GoBack();
            }
        }
    }
}
