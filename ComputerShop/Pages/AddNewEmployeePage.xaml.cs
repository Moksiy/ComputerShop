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
    /// Логика взаимодействия для AddNewEmployeePage.xaml
    /// </summary>
    public partial class AddNewEmployeePage : Page
    {
        public AddNewEmployeePage()
        {
            InitializeComponent();
            EmpImage.Source = new BitmapImage(new Uri("/Images/defImage.jpg", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
            AddShops();
            AddPositions();
        }

        /// <summary>
        /// Изображение в битном представлении
        /// </summary>
        public static byte[] ImageData { get; set; }

        /// <summary>
        /// путь к конвертируемому файлу
        /// </summary>
        public static string FilePath { get; set; }

        /// <summary>
        /// Назад, на предыдущую вкладку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        /// <summary>
        /// Добавить сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            ResetBorders();
            if (!String.IsNullOrEmpty(FirstName.Text) &&
               !String.IsNullOrEmpty(LastName.Text) &&
               !String.IsNullOrEmpty(Patronom.Text) &&
               !String.IsNullOrEmpty(Login.Text) &&
               !String.IsNullOrEmpty(Password.Text) &&
               !String.IsNullOrEmpty(Shop.Text) &&
               !String.IsNullOrEmpty(Position.Text))
            {
                AddEmpl();
            }
            else
            {
                if (String.IsNullOrEmpty(FirstName.Text))
                    FirstName.BorderBrush = Brushes.Red;
                if (String.IsNullOrEmpty(LastName.Text))
                    LastName.BorderBrush = Brushes.Red;
                if (String.IsNullOrEmpty(Patronom.Text))
                    Patronom.BorderBrush = Brushes.Red;
                if (String.IsNullOrEmpty(Login.Text))
                    Login.BorderBrush = Brushes.Red;
                if (String.IsNullOrEmpty(Password.Text))
                    Password.BorderBrush = Brushes.Red;
                if (String.IsNullOrEmpty(Shop.Text))
                    Shop.BorderBrush = Brushes.Red;
                if (String.IsNullOrEmpty(Position.Text))
                    Position.BorderBrush = Brushes.Red;
            }
        }

        private void CheckDirector()
        {

        }

        /// <summary>
        /// Сброс выделения элементов
        /// </summary>
        private void ResetBorders()
        {
            FirstName.BorderBrush = Brushes.SlateGray;
            LastName.BorderBrush = Brushes.SlateGray;
            Patronom.BorderBrush = Brushes.SlateGray;
            Login.BorderBrush = Brushes.SlateGray;
            Password.BorderBrush = Brushes.SlateGray;
            Shop.BorderBrush = Brushes.SlateGray;
            Position.BorderBrush = Brushes.SlateGray;
        }

        /// <summary>
        /// Добавление сотрудника в БД
        /// </summary>
        private async void AddEmpl()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                ComboBoxItem item = new ComboBoxItem();
                item = (ComboBoxItem)Position.SelectedItem;

                //Запрос
                command.CommandText = @"INSERT INTO Employee VALUES ((SELECT ISNULL(MAX(Employee.ID),0) FROM Employee) + 1,'"
                + LastName.Text + "','" + FirstName.Text + "','" + Patronom.Text + "','" + Shop.SelectedIndex + "','" + item.Tag +
                "',0)";

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
                AddUser();
            }
        }

        /// <summary>
        /// Выбор фото сотрудника через диалоговое окно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPG Files (*.jpg)|*.jpg";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                FilePath = dlg.FileName;

                using (System.IO.FileStream fs = new System.IO.FileStream(FilePath, System.IO.FileMode.Open))
                {
                    ImageData = new byte[fs.Length];

                    fs.Read(ImageData, 0, ImageData.Length);
                }

                EmpImage.Source = new BitmapImage(new Uri(dlg.FileName.ToString()));
            }
        }

        private async void AddShops()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT ID, ShopName FROM Shops";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Shop.Items.Add(new ComboBoxItem { Tag = dataReader[0], Content = dataReader[1] });
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

        private async void AddPositions()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT * FROM Positions WHERE ID > 0";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Position.Items.Add(new ComboBoxItem { Tag = dataReader[0], Content = dataReader[1] });
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
        /// Добавляем загруженное изображение в БД
        /// </summary>
        private async void AddImage()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = @"INSERT INTO EmployeePhoto VALUES ((SELECT MAX(ID) FROM Employee), @ImageData)";

                command.Connection = connection;

                command.Parameters.Add("@ImageData", System.Data.SqlDbType.Image, 1000000);

                command.Parameters["@ImageData"].Value = ImageData;

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
                this.NavigationService.Navigate(new EmployeesPage());
            }
        }

        /// <summary>
        /// Добавляем новую учетку в БД
        /// </summary>
        private async void AddUser()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "INSERT INTO Users VALUES('" + Login.Text + "','" + Password.Text + "',(SELECT MAX(Employee.ID) FROM Employee))";

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
                //Если изображение загружено, то вызываем метод добавления изображения в бд
                if (!String.IsNullOrEmpty(FilePath))
                    AddImage();
                else
                    this.NavigationService.Navigate(new EmployeesPage());
            }
        }
    }
}
