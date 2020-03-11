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
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();

            //рефрешим каптчу сразу
            Captcha.Content = CaptchaBuild.Refresh();
        }

        /// <summary>
        /// Смена каптчи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshCaptcha_Click(object sender, RoutedEventArgs e)
        {
            Captcha.Content = CaptchaBuild.Refresh();
        }

        /// <summary>
        /// Войти в учетную запись
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            //Возвращаем исходный контур
            LoginBox.BorderBrush = Brushes.SlateGray;
            PasswordBox.BorderBrush = Brushes.SlateGray;
            CaptchaText.BorderBrush = Brushes.SlateGray;

            //Проверяем на заполненность логин и пароль
            if(!String.IsNullOrEmpty(LoginBox.Text) && !String.IsNullOrEmpty(PasswordBox.Password))
            {
                //Проверяем капчу
                if(Captcha.Content.ToString() == CaptchaText.Text.ToUpper())
                {
                    CheckUser();
                }
                else
                {
                    CaptchaText.BorderBrush = Brushes.Red;
                }
            }
            else
            {
                //Красим логин и пароль в красный
                LoginBox.BorderBrush = Brushes.Red;
                PasswordBox.BorderBrush = Brushes.Red;
            }

            //Рефрешим капчу
            Captcha.Content = CaptchaBuild.Refresh();
        }

        /// <summary>
        /// Проверка логина и пароля в бд
        /// </summary>
        private async void CheckUser()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT * FROM [Users] WHERE Login = '" + LoginBox.Text + "' AND Password = '" + PasswordBox.Password + "'";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                //Добавляем в список
                while (dataReader.Read())
                {
                    
                }
            }
            catch (SqlException ex)
            {
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
