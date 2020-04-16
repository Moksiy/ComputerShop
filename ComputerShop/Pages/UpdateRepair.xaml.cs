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
    /// Логика взаимодействия для UpdateBuilding.xaml
    /// </summary>
    public partial class UpdateRepair : Page
    {
        public UpdateRepair()
        {
            InitializeComponent();
            GetStatuses();
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Завершить ремонт?", "Завершение активного ремонта", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                UpdateStatus("3");
            }
        }

        /// <summary>
        /// Назад
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RepairsPage());
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Вы уверены, что хотите отменить ремонт?", "Отмена активного ремонта", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                UpdateStatus("4");
            }
        }

        private async void UpdateStatus(string status)
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                switch(status)
                {
                    case "4":
                        command.CommandText = "UPDATE Repairs SET EndingDate = GETDATE(), Status = 4 WHERE ID = "+CurrentRepair.ID;
                        break;

                    case "3":
                        command.CommandText = "UPDATE Repairs SET EndingDate = GETDATE(), Status = 3 WHERE ID = " + CurrentRepair.ID;
                        break;

                    case "1":
                    case "2":
                        command.CommandText = "UPDATE Repairs SET Status = " + status+" WHERE ID = " + CurrentRepair.ID;
                        break;

                    default:
                        command.CommandText = "";
                        break;

                }                

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
                this.NavigationService.Navigate(new RepairsPage());
            }            
        }

        private async void GetStatuses()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT * FROM RepairStatuses WHERE ID < 3";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Tag = dataReader[0].ToString();
                    item.Content = dataReader[1].ToString();
                    Statuses.Items.Add(item);
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

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Error.Content = "";

            if (String.IsNullOrEmpty(Statuses.Text))
            {
                Error.Content = "Не выбран статус ремонта";
            }
            else
            {
                ComboBoxItem item = new ComboBoxItem();
                item = (ComboBoxItem)Statuses.SelectedItem;
                UpdateStatus(item.Tag.ToString());
            }
        }
    }
}
