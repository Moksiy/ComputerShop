using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public class ErrorElement
    {
        public string ErrorID { get; set; }
        public string UserLogin { get; set; }
        public string ErrorText { get; set; }
        public DateTime ErrorDate { get; set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="login">Login</param>
        /// <param name="text">текст ошибки</param>
        /// <param name="date">Дата ошибки</param>
        public ErrorElement(string id, string login, string text, DateTime date)
        {
            this.ErrorID = id;
            this.UserLogin = login;
            this.ErrorText = text;
            this.ErrorDate = date;
        }
    }
}
