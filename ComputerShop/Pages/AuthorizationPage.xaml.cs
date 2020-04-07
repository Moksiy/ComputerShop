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
using System.Threading;

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

            Thread.Sleep(300);

            //Рефрешим капчу
            Captcha.Content = CaptchaBuild.Refresh();
        }

        /// <summary>
        /// Проверка логина и пароля в бд
        /// </summary>
        private async void CheckUser()
        {
            //Убираем вывод ошибки
            Error.Content = "";

            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT dbo.Users.Login, dbo.Users.Password, dbo.Employee.PositionID, dbo.Employee.ID FROM dbo.Employee INNER JOIN dbo.Users ON dbo.Employee.ID = dbo.Users.EmployeeID WHERE Users.Login = '" + LoginBox.Text+"' AND Users.Password = '"+PasswordBox.Password+"'";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    User.Login = dataReader[0].ToString();
                    User.Password = dataReader[1].ToString();
                    User.RoleID = dataReader[2].ToString();
                    User.ID = dataReader[3].ToString();
                }

                //Если пользователь существует
                if(!String.IsNullOrEmpty(User.Login) && !String.IsNullOrEmpty(User.Password))
                {
                    switch(User.RoleID)
                    {
                        //Админ
                        case "0":
                            this.NavigationService.Navigate(new AdminPage());
                            break;

                        //Генеральный директор
                        case "1":
                            this.NavigationService.Navigate(new LogoPage());
                            ((MainWindow)System.Windows.Application.Current.MainWindow).LeftBar.Navigate(new GeneralDirectorBar());
                            break;

                        //Директор
                        case "2":
                            this.NavigationService.Navigate(new DirectorPage());
                            break;

                        //Логист
                        case "3":
                            this.NavigationService.Navigate(new LogisticianPage());
                            break;

                        //Рабочий ремонтной площадки
                        case "4":
                            this.NavigationService.Navigate(new RepairmanPage());
                            break;

                        //Продавец-консультант
                        case "5":
                            //this.NavigationService.Navigate(new AdminPage());
                            break;
                    }
                }
                else
                {
                    LoginBox.BorderBrush = Brushes.Red;
                    PasswordBox.BorderBrush = Brushes.Red;                    
                    Error.Content = "Неправильная комбинация логин-пароль";
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
                SynchronizationErrors.New(ex.ToString());
            }
            finally
            {
                //В любом случае закрываем подключение
                connection.Close();
            }
        }
    }
}
