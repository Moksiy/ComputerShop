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
    /// Логика взаимодействия для UpdateProductPage.xaml
    /// </summary>
    public partial class UpdateProductPage : Page
    {
        public UpdateProductPage()
        {
            InitializeComponent();
            GetCategories();

        }

        public string FilePath { get; set; }
        public byte[] ImageData { get; set; }

        public CharacteristicElement Element { get; set; }

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Добавить характеристику
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCharact_Click(object sender, RoutedEventArgs e)
        {

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
                    ImageData = new byte[fs.Length];

                    fs.Read(ImageData, 0, ImageData.Length);
                }
                ProductImage.Source = new BitmapImage(new Uri(dlg.FileName.ToString()));
            }
        }

        /// <summary>
        /// назад
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        /// <summary>
        /// Нажатие правой кнопкой мыши по элементу listview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            object obj = item.Tag;
            ContextMenu cm = this.FindName("CONTEXT") as ContextMenu;
            cm.IsOpen = true;
            Element = (CharacteristicElement)obj;
        }

        /// <summary>
        /// Удалить характеристику
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            int index = Characteristics.IndexOf(Element);
            if (index >= 0)
            {
                Characteristics.Remove(index);
                Characts.Items.RemoveAt(index);
            }
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

        private async void GetCategories()
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
                GetProductInfo();
                GetCharacteristics();
            }
        }

        private async void GetProductInfo()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT Images.ImageData, Products.ProductName, Products.Manufacturer, Products.Artikul, Products.Cost, Products.TypeID FROM     dbo.Images INNER JOIN dbo.Products ON dbo.Images.ID = dbo.Products.ID WHERE Products.ID = " + CurrentProduct.ID;

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    ProductImage.Source = ImageFromBuffer((byte[])dataReader[0]);
                    Name.Text = dataReader[1].ToString();
                    Manufacture.Text = dataReader[2].ToString();
                    Artikul.Text = dataReader[3].ToString();
                    CostBox.Text = Convert.ToInt32(dataReader[4]).ToString();
                    Categories.SelectedIndex = Convert.ToInt32(dataReader[5]);
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
        /// Получаем харакетристики в список
        /// </summary>
        private async void GetCharacteristics()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT * FROM Characteristics WHERE ID = " + CurrentProduct.ID;

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                Characteristics.Clear();

                while (dataReader.Read())
                {
                    ListViewItem item = new ListViewItem();
                    item.Content = dataReader[1].ToString() + " : " + dataReader[2].ToString();
                    item.Tag = new CharacteristicElement(dataReader[1].ToString(), dataReader[2].ToString());
                    Characts.Items.Add(item);
                    Characteristics.Add(dataReader[1].ToString(), dataReader[2].ToString());
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
