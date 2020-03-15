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
                command.CommandText = "SELECT * FROM Types";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Categories.Items.Add(new ComboBoxItem { Tag = dataReader[0], Content = dataReader[1] });
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
        /// Индекс для контекстного меню
        /// </summary>
        public static int Index { get; set; }

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
            dlg.Filter = "JPG Files (*.jpg)|*.jpg";
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
            //ресетаем цвета текстбоксов
            CName.BorderBrush = Brushes.SlateGray;
            CText.BorderBrush = Brushes.SlateGray;

            if (!String.IsNullOrEmpty(CName.Text) && !String.IsNullOrEmpty(CText.Text))
            {
                Characteristics.Add(CName.Text, CText.Text);
                ListViewItem item = new ListViewItem();
                item.Content = CName.Text + " : " + CText.Text;
                item.Tag = Characteristics.Count();
                Characts.Items.Add(item);

                //Ресетаем контент
                CName.Text = "";
                CText.Text = "";
            }
            else
            {
                if (String.IsNullOrEmpty(CName.Text))
                    CName.BorderBrush = Brushes.Red;
                if (String.IsNullOrEmpty(CText.Text))
                    CText.BorderBrush = Brushes.Red;
            }
        }

        private void ResetColors()
        {
            Name.BorderBrush = Brushes.SlateGray;
            Manufacture.BorderBrush = Brushes.SlateGray;
            Artikul.BorderBrush = Brushes.SlateGray;
            Categories.BorderBrush = Brushes.SlateGray;
            Error.Content = "";
            ErrorImage.Content = "";
            CostBox.BorderBrush = Brushes.SlateGray;
        }

        /// <summary>
        /// Добавить товар
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            ResetColors();

            if (!String.IsNullOrEmpty(Name.Text) &&
                !String.IsNullOrEmpty(Manufacture.Text) &&
                !String.IsNullOrEmpty(Artikul.Text) &&
                Categories.SelectedItem != null &&
                !String.IsNullOrEmpty(FilePath) &&
                !String.IsNullOrEmpty(CostBox.Text))
            {
                Addproduct();
            }
            else
            {
                if (String.IsNullOrEmpty(Name.Text))
                    Name.BorderBrush = Brushes.Red;
                if (String.IsNullOrEmpty(Manufacture.Text))
                    Manufacture.BorderBrush = Brushes.Red;
                if (String.IsNullOrEmpty(Artikul.Text))
                    Artikul.BorderBrush = Brushes.Red;
                if (Categories.SelectedItem == null)
                    Error.Content = "Выберите категорию";
                if (String.IsNullOrEmpty(FilePath))
                    ErrorImage.Content = "Добавьте изображение товара";
                if (String.IsNullOrEmpty(CostBox.Text))
                    CostBox.BorderBrush = Brushes.Red;
            }
        }

        private void Addproduct()
        {
            //Сначала получаем id
            SqlConnection connection = new SqlConnection();

            int id = 0;

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                connection.Open();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT ISNULL(MAX(Products.ID),0) FROM Products";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    id = Convert.ToInt32(dataReader[0]);
                    id++;
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

            //Добавляем товар в бд
            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                connection.Open();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "INSERT INTO Products VALUES(" + id + ",'" + Name.Text + "','" + Artikul.Text + "','" + Manufacture.Text + "'," + Categories.SelectedIndex + "," + CostBox.Text + ")";

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
            }

            if (Characteristics.GetCount() > 0)
            {
                //Добавляем характеристики товара в бд
                try
                {
                    connection.ConnectionString = MainWindow.ConnectionSrting;

                    //Открываем подключение
                    connection.Open();

                    SqlCommand command = new SqlCommand();

                    //Запрос
                    foreach (var item in Characteristics.Get())
                    {
                        command.CommandText += "INSERT INTO Characteristics VALUES (" + id + ",'" + item.Name + "','" + item.Text + "') ";
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
                }
            }

            AddImage(id.ToString());
        }

        /// <summary>
        /// Добавление изображения в БД
        /// </summary>
        private async void AddImage(string id)
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = @"INSERT INTO Images VALUES (" + id + ", @FileName, @Title, @ImageData)";

                command.Connection = connection;

                command.Parameters.Add("@FileName", System.Data.SqlDbType.NVarChar, 50);

                command.Parameters.Add("@Title", System.Data.SqlDbType.NVarChar, 50);

                command.Parameters.Add("@ImageData", System.Data.SqlDbType.Image, 1000000);

                string shortFileName = FilePath.Substring(FilePath.LastIndexOf('\\') + 1);

                // передаем данные в команду через параметры
                command.Parameters["@FileName"].Value = shortFileName;
                command.Parameters["@Title"].Value = Categories.Text.ToString();
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

            this.NavigationService.Navigate(new ProductsPage());
        }

        /// <summary>
        /// Удалить из списка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (Index != 0)
            {
                Characts.Items.RemoveAt(Index - 1);
                Characteristics.Remove(Index - 1);
            }
        }

        /// <summary>
        /// Обработчик на нажатие правой кнопкой мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            object obj = item.Tag;
            ContextMenu cm = this.FindName("CONTEXT") as ContextMenu;
            cm.IsOpen = true;
            Index = Convert.ToInt32(obj);
        }

        /// <summary>
        /// Разрешаем в текстбокс стоимость вводить только цифры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CostBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }
    }
}
