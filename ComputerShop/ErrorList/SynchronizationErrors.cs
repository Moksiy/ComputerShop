using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ComputerShop
{
    public static class SynchronizationErrors
    {
        public static List<ErrorElement> ErrorList = new List<ErrorElement>();

        /// <summary>
        /// Добавляем новую ошибку
        /// </summary>
        /// <param name="error">Текст ошибки</param>
        public static void New(string error)
        {
            Add(User.ID, error, DateTime.Now);
        }

        /// <summary>
        /// Добавить ошибку
        /// </summary>
        public static void Add(string id, string text, DateTime date)
        {
            ErrorList.Add(new ErrorElement(Convert.ToString(ErrorList.Count()+1), id, text, date));
        }

        /// <summary>
        /// Возвращаем список ошибок
        /// </summary>
        /// <returns></returns>
        public static List<ErrorElement> GetList()
        {
            return ErrorList;
        }

        /// <summary>
        /// Удалить ошибку
        /// </summary>
        /// <param name="id">ID</param>
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
        public async static void Get()
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "SELECT * FROM ErrorList";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();

                while(dataReader.Read())
                {
                    ErrorList.Add(new ErrorElement(dataReader[0].ToString(),
                        dataReader[1].ToString(), dataReader[2].ToString(),
                        Convert.ToDateTime(dataReader[3])));                    
                }
            }
            catch (SqlException ex)
            {
                SynchronizationErrors.New(ex.ToString());
            }
            finally
            {
                //В любом случае закрываем подключение
                connection.Close();
            }
        }

        /// <summary>
        /// Удаление ошибки из БД
        /// </summary>
        /// <param name="id">ID</param>
        public async static void RemoveFromDB(string id)
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "DELETE FROM ErrorList WHERE ID="+id;

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();
            }
            catch (SqlException ex)
            {
                SynchronizationErrors.New(ex.ToString());
            }
            finally
            {
                //В любом случае закрываем подключение
                connection.Close();
            }
        }

        /// <summary>
        /// ошибку в БД
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="text">текст ошибки</param>
        /// <param name="date">Дата ошибки</param>
        public async static void AddToDB(string id, string text, DateTime date)
        {
            SqlConnection connection = new SqlConnection();

            try
            {
                connection.ConnectionString = MainWindow.ConnectionSrting;

                //Открываем подключение
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //Запрос
                command.CommandText = "INSERT INTO ErrorList VALUES((SELECT ISNULL(MAX(ErrorList.ID),0) FROM ErrorList)+1,"+id+",'"+text+"','"+date+"')";

                command.Connection = connection;

                SqlDataReader dataReader = command.ExecuteReader();
            }
            catch (SqlException ex)
            {
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
