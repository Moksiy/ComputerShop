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
    /// Логика взаимодействия для MoreinfoProductPage.xaml
    /// </summary>
    public partial class MoreinfoProductPage : Page
    {
        public MoreinfoProductPage()
        {
            InitializeComponent();
            ProductImage.Source = new BitmapImage(new Uri("/Images/defImage.jpg", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
            AddInfo();
            AddCharact();
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
        /// На предыдущую вкладку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        /// <summary>
        /// Добавляем информацию из БД
        /// </summary>
        private async void AddInfo()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT Images.ImageData, Products.ProductName, Products.Manufacturer, Products.Artikul, Products.Cost FROM     dbo.Images INNER JOIN dbo.Products ON dbo.Images.ID = dbo.Products.ID WHERE Products.ID = " + CurrentProduct.ID;

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    ProductImage.Source = ImageFromBuffer((byte[])dataReader[0]);
                    ProductName.Text = dataReader[1].ToString();
                    Manufact.Text = dataReader[2].ToString();
                    Artukul.Text = dataReader[3].ToString();
                    Price.Text = Convert.ToInt32(dataReader[4]).ToString();
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
        /// Добавляем характеристики в список
        /// </summary>
        private async void AddCharact()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT [Name], Characteristic FROM Characteristics WHERE ID = " + CurrentProduct.ID;

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Charactes.Items.Add(new CharacteristicElement(dataReader[0].ToString(),
                        dataReader[1].ToString()));
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
    }
}
