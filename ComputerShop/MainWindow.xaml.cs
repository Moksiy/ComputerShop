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

namespace ComputerShop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Строка подключения
        /// </summary>
        public static string ConnectionSrting { get; } = @"Data Source=DESKTOP-O70G8S5;Initial Catalog=BuildYourOwnPC;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        

        public MainWindow()
        {
            InitializeComponent();
            Center.Navigate(new AuthorizationPage());
            //((MainWindow)System.Windows.Application.Current.MainWindow).LeftBar.Navigate(new GeneralDirectorPage());
        }
    }
}
