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
    /// Логика взаимодействия для ChequePage.xaml
    /// </summary>
    public partial class ChequePage : Page
    {
        public ChequePage()
        {
            InitializeComponent();
            AddInfo();
        }

        /// <summary>
        /// Добавляем документ
        /// </summary>
        public void AddInfo()
        {
            CurrentDocument.Document = ChequeBuilder.Build();
            this.Scroll.Document = ChequeBuilder.Build();
        }

        /// <summary>
        /// Напечатать чек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_Click(object sender, RoutedEventArgs e)
        {
            FlowDocument d = new FlowDocument();
            Document = d;

            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {                
                printDialog.PrintDocument(((IDocumentPaginatorSource)CurrentDocument.Document).DocumentPaginator, "Cheque");
            }
        }

        /// <summary>
        /// Сохранить чек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Выход из окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).Center.Navigate(new LogoPage());
        }
    }
}
