using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace ComputerShop
{
    public static class ChequeBuilder
    {
        public static FlowDocument Build()
        {
            //Итоговый чек
            FlowDocument cheque = new FlowDocument();

            //Задаем белый фон
            cheque.Background = Brushes.White;

            //Задем красивый шрифт
            cheque.FontFamily = new FontFamily("Lucida Console");

            //Задаем размер шрифта
            cheque.FontSize = 14;

            //Задем стиль шрифта
            cheque.FontStyle = FontStyles.Normal;

            //Устанавливаем ширину печатаемого документа
            cheque.PageWidth = 790;
            cheque.ColumnWidth = 790;

            #region Строковые шаблоны
            string hr = "------------------------------------------------------------------------------------------\n";    //90
            string hr2 = "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - \n";

            //Формирование шапки
            string up = hr;
            up += new string(' ',36) + "Build Your Own PC" + new string(' ',37) + "\n";
            up += hr + "\n";
            up += new string(' ',39) + "КАССОВЫЙ ЧЕК"+ new string(' ',39) + "\n";
            string date = DateTime.Now.ToString();
            up += new string(' ', (90 - date.Length) / 2) + date + "\n";
            up += new string(' ', 38) + "ИНН 7704217370" + new string(' ', 38) + "\n";
            up += new string(' ', 33) + "Вид налогообложения: ОСН" + new string(' ', 33) + "\n";
            up += new string(' ', 42) + "Приход" + new string(' ', 42) + "\n";
            switch(CurrentCheque.Type)
            {
                case 0:
                    up += new string(' ', 41) + "ПОКУПКА" + new string(' ', 42) + "\n";
                    break;

                case 1:
                    up += new string(' ', 29) + "РЕМОНТ ПЕРСОНАЛЬНОГО КОМПЬЮТЕРА" + new string(' ', 03) + "\n";
                    break;

                case 2:
                    up += new string(' ', 29) + "СБОРКА ПЕРСОНАЛЬНОГО КОМПЬЮТЕРА" + new string(' ', 30) + "\n";
                    break;
            }
            up += hr2;


            //Формируем тело чека
            int i = 1;
            string num = "";
            string product = "";
            string cost = "";
            string medium = "";
            foreach(ProductItem item in CurrentCheque.Cart)
            {
                num = i + ". ";
                product = item.Detail.Substring(0,item.Detail.Length>60?60:item.Detail.Length) + " X " +item.Quan;
                cost = item.Total;
                cost = cost.Substring(0, cost.Length - 2) + "₽";
                medium += num + product + new string('.', (90 - num.Length - product.Length - cost.Length)) + cost + "\n\n";
                i++;
            }
            medium += hr2;


            //Формируем конец
            string bot = "";
            string total = CurrentCheque.Cost + "₽";
            bot += "ИТОГ" + new string(' ',86-total.Length) + total + "\n\n";
            bot += "Клиент" + new string(' ', 84 - CurrentCheque.Client.Length) + CurrentCheque.Client + "\n";
            bot += "Сотрудник" + new string(' ', 81 - CurrentCheque.Employee.Length) + CurrentCheque.Employee + "\n";
            bot += "ФН:" + new string(' ', 71) + "8710000100694062" + "\n";
            bot += "РН ККТ:" + new string(' ', 67) + "0000758935034525" + "\n";
            bot += "ФД:" + new string(' ', 81) + "210159" + "\n";
            bot += "ФПД:" + new string(' ', 78) + "24865857" + "\n";
            bot += "Сайт ФНС:" + new string(' ', 69) + "www.nalog.ru" + "\n\n\n";
            bot += new string(' ', 30) + "С <3 Команда Build Your Own PC" + new string(' ',30);
            #endregion

            //Формируем текст


            //Формируем параграфы
            Paragraph head = new Paragraph(new Run(up));
            Paragraph body = new Paragraph(new Run(medium));
            Paragraph bottom = new Paragraph(new Run(bot));

            //Все собираем вместе
            cheque.Blocks.Add(head);
            cheque.Blocks.Add(body);
            cheque.Blocks.Add(bottom);

            return cheque;
        }
    }
}
