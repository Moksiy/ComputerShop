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
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        public ClientsPage()
        {
            InitializeComponent();
            GetClients();
        }

        /// <summary>
        /// Добавить нового клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddClient_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddNewClientPage());
        }

        private async void GetClients()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT        dbo.Clients.LastName, dbo.Clients.Name, dbo.Clients.Patronymic, dbo.Clients.Email, dbo.Clients.PhoneNumber, dbo.Clients.CardID, dbo.BonusProgramms.ProgramName " +
                                      "FROM            dbo.Clients INNER JOIN " +
                                      "dbo.Bonuses ON dbo.Clients.CardID = dbo.Bonuses.CardID INNER JOIN " +
                                      "dbo.BonusProgramms ON dbo.Bonuses.ProgrammID = dbo.BonusProgramms.ID";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Clients.Items.Add(new ClientElement(dataReader[0].ToString(),
                        dataReader[1].ToString(), dataReader[2].ToString(), dataReader[3].ToString(),
                        dataReader[4].ToString(), dataReader[5].ToString(), dataReader[6].ToString()));
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
