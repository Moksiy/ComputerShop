using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public static class SynchronizationErrors
    {
        public static List<ErrorElement> ErrorList = new List<ErrorElement>();

        public static void New(string error)
        {

        }

        /// <summary>
        /// Добавить ошибку
        /// </summary>
        public static void Add(string id, string login, string text, DateTime date)
        {
            ErrorList.Add(new ErrorElement(id, login, text, date));
        }

        /// <summary>
        /// Удалить ошибку
        /// </summary>
        /// <param name="id"></param>
        public static void Remove(string id)
        {
            for (int i = 0; i < ErrorList.Count; i++)
            {
                if (ErrorList[i].ErrorID == id)
                {
                    ErrorList.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// Получить список ошибок
        /// </summary>
        /// <returns></returns>
        public static List<object> Get()
        {
            return null;
        }

        /// <summary>
        /// Удаление ошибки из БД
        /// </summary>
        /// <param name="id">ID</param>
        public async static void RemoveFromDB(string id)
        {

        }
    }
}
