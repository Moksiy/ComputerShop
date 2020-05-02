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
using System.IO;

namespace ComputerShop
{
    /// <summary>
    /// Логика взаимодействия для UpdateEmployeePage.xaml
    /// </summary>
    public partial class UpdateEmployeePage : Page
    {
        public UpdateEmployeePage()
        {
            InitializeComponent();
            EmpImage.Source = new BitmapImage(new Uri("/Images/defImage.jpg", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
            GetPositions();
            GetShops();
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
        /// Назад
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
            ResetBorders();
            if (!String.IsNullOrEmpty(FirstName.Text) &&
               !String.IsNullOrEmpty(LastName.Text) &&
               !String.IsNullOrEmpty(Patronom.Text) &&
               !String.IsNullOrEmpty(Login.Text) &&
               !String.IsNullOrEmpty(Password.Text) &&
               !String.IsNullOrEmpty(Shop.Text) &&
               !String.IsNullOrEmpty(Position.Text))
            {
                UpdateEmloyee();
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

        /// <summary>
        /// Добавить фото
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
        /// Получаем данные о сотруднике из БД
        /// </summary>
        private async void GetEmployeeInfo()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT dbo.Employee.LastName, dbo.Employee.Name, dbo.Employee.Patronymic, dbo.Employee.ShopID, dbo.Employee.PositionID, dbo.Employee.StatusID, dbo.EmployeePhoto.ImageData, dbo.Users.Login, dbo.Users.Password FROM     dbo.Employee INNER JOIN dbo.Users ON dbo.Employee.ID = dbo.Users.EmployeeID LEFT OUTER JOIN dbo.EmployeePhoto ON dbo.Employee.ID = dbo.EmployeePhoto.ID WHERE Employee.ID = " + CurrentEmployee.ID;

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    LastName.Text = dataReader[0].ToString();
                    FirstName.Text = dataReader[1].ToString();
                    Patronom.Text = dataReader[2].ToString();
                    Shop.SelectedIndex = Convert.ToInt32(dataReader[3]);
                    Position.SelectedIndex = Convert.ToInt32(dataReader[4])-1;
                    if (!(dataReader[6] is DBNull))
                        EmpImage.Source = ImageFromBuffer((byte[])dataReader[6]);
                    Login.Text = dataReader[7].ToString();
                    Password.Text = dataReader[8].ToString();
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

        private async void GetPositions()
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
                GetEmployeeInfo();
            }
        }

        private async void GetShops()
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

        /// <summary>
        /// Конвертер массива байтов в изображение
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public BitmapImage ImageFromBuffer(Byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            return image;
        }

        /// <summary>
        /// Обновляем данные а таблице сотрудники
        /// </summary>
        private async void UpdateEmloyee()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = @"EXEC UpdateEmpl @lastname, @firstname, @patronom, @shop, @position, @id";

                command.Parameters.Add("@lastname", System.Data.SqlDbType.VarChar, 50);

                command.Parameters.Add("@firstname", System.Data.SqlDbType.VarChar, 50);

                command.Parameters.Add("@patronom", System.Data.SqlDbType.VarChar, 50);

                command.Parameters.Add("@shop", System.Data.SqlDbType.Int);

                command.Parameters.Add("@position", System.Data.SqlDbType.Int);

                command.Parameters.Add("@id", System.Data.SqlDbType.Int);

                command.Parameters["@lastname"].Value = LastName.Text;

                command.Parameters["@firstname"].Value = FirstName.Text;

                command.Parameters["@patronom"].Value = Patronom.Text;

                command.Parameters["@shop"].Value = Convert.ToInt32(Shop.SelectedIndex);

                command.Parameters["@position"].Value = Convert.ToInt32(Position.SelectedIndex);

                command.Parameters["@id"].Value = Convert.ToInt32(CurrentEmployee.ID);

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
                UpdateUser();
            }
        }

        /// <summary>
        /// Обновляем данные в таблице юзеров
        /// </summary>
        private async void UpdateUser()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "UPDATE Users SET Login = '"+Login.Text+"', Password = '"+Password.Text+"' WHERE EmployeeID = "+CurrentEmployee.ID;

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
                if(ImageData != null)
                    UpdatePhoto();
                else
                    this.NavigationService.Navigate(new EmployeesPage());
            }
        }

        /// <summary>
        /// Обновляем фото, если его нет, то добавляем
        /// </summary>
        private async void UpdatePhoto()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = @"IF (SELECT COUNT(ID) FROM EmployeePhoto WHERE ID = "+CurrentEmployee.ID+@") > 0 BEGIN UPDATE EmployeePhoto SET ImageData = @ImageData WHERE ID = " + CurrentEmployee.ID+@" END ELSE BEGIN INSERT INTO EmployeePhoto VALUES ("+CurrentEmployee.ID+ @", @ImageData) END";

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
    }
}
