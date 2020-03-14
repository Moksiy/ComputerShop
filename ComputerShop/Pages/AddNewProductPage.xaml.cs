﻿using System;
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
    /// Логика взаимодействия для AddNewProductPage.xaml
    /// </summary>
    public partial class AddNewProductPage : Page
    {
        public AddNewProductPage()
        {                        
            InitializeComponent();
            ProductImage.Source = new BitmapImage(new Uri("/Images/defImage.jpg", UriKind.Relative)) { CreateOptions = BitmapCreateOptions.IgnoreImageCache };
            AddCategories();
        }

        public async void AddCategories()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT * FROM Typess";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while(dataReader.Read())
                {
                    Categories.Items.Add(new ComboBoxItem { Tag = dataReader[0], Content = dataReader[1]});
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
        /// Путь к изображению
        /// </summary>
        public static string FilePath { get; set; } = "";

        /// <summary>
        /// Двоичное представление изображения
        /// </summary>
        public static byte[] imageData { get; set; }

        /// <summary>
        /// Переход на предыдущую страницу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        /// <summary>
        /// Выбор фото
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".jpg";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                FilePath = dlg.FileName;                

                using (System.IO.FileStream fs = new System.IO.FileStream(FilePath, System.IO.FileMode.Open))
                {
                    imageData = new byte[fs.Length];

                    fs.Read(imageData, 0, imageData.Length);
                }

                ProductImage.Source = new BitmapImage(new Uri(dlg.FileName.ToString()));                
            }
        }

        /// <summary>
        /// Добавить характеристику
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCharact_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResetColors()
        {
            Name.BorderBrush = Brushes.SlateGray;
            Manufacturer.BorderBrush = Brushes.SlateGray;
            Artikul.BorderBrush = Brushes.SlateGray;
            Categories.BorderBrush = Brushes.SlateGray;
        }

        /// <summary>
        /// Добавить товар
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrEmpty(Name.Text) &&
                !String.IsNullOrEmpty(Manufacturer.Text) &&
                !String.IsNullOrEmpty(Artikul.Text) &&
                !String.IsNullOrEmpty(Categories.SelectedItem.ToString()))
            {

            }
            else
            {
                if (String.IsNullOrEmpty(Name.Text))
                    Name.BorderBrush = Brushes.Red;
                if (String.IsNullOrEmpty(Manufacturer.Text))
                    Manufacturer.BorderBrush = Brushes.Red;
                if (String.IsNullOrEmpty(Artikul.Text))
                    Artikul.BorderBrush = Brushes.Red;
                if (String.IsNullOrEmpty(Categories.SelectedItem.ToString()))
                    Categories.BorderBrush = Brushes.Red;
            }


            AddImage();
        }

        /// <summary>
        /// Добавление изображения в БД
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
                command.CommandText = @"INSERT INTO Images VALUES ((SELECT ISNULL(MAX(Images.ID),0) FROM Images)+1, @FileName, @Title, @ImageData)";

                command.Connection = connection;

                command.Parameters.Add("@FileName", System.Data.SqlDbType.NVarChar,50);

                command.Parameters.Add("@Title", System.Data.SqlDbType.NVarChar, 50);

                command.Parameters.Add("@ImageData", System.Data.SqlDbType.Image, 1000000);

                string shortFileName = FilePath.Substring(FilePath.LastIndexOf('\\') + 1);

                string title = ""; //- выбранная категория
                
                // передаем данные в команду через параметры
                command.Parameters["@FileName"].Value = shortFileName;
                command.Parameters["@Title"].Value = title;
                command.Parameters["@ImageData"].Value = imageData;

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
            }
        }
    }
}
