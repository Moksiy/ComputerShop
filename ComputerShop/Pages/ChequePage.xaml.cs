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
using System.IO;
using System.Windows.Markup;
using System.Windows.Forms;

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

            System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();
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
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.AddExtension = true;
            saveDialog.Filter = "(*.txt)|*.txt|Все файлы (*.*)|*.* ";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream fs = new FileStream(saveDialog.FileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    FlowDocument flow = new FlowDocument();
                    flow = ChequeBuilder.Build();
                    TextRange textRange = new TextRange(flow.ContentStart, flow.ContentEnd);
                    textRange.Save(fs, System.Windows.DataFormats.Text);
                }
            }            

        }

        /// <summary>
        /// Выход из окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).Center.Navigate(new LogoPage());
            switch(User.RoleID)
            {
                case "1":
                    ((MainWindow)System.Windows.Application.Current.MainWindow).LeftBar.Navigate(new GeneralDirectorBar());
                    break;

                case "2":
                    ((MainWindow)System.Windows.Application.Current.MainWindow).LeftBar.Navigate(new DirectorBar());
                    break;

                case "4":
                    ((MainWindow)System.Windows.Application.Current.MainWindow).LeftBar.Navigate(new RepairmanBar());
                    break;

                case "5":
                    ((MainWindow)System.Windows.Application.Current.MainWindow).LeftBar.Navigate(new ConsultantBar());
                    break;
            }
        }
    }
}
