using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public static class Characteristics
    {
        public static List<CharacteristicElement> characteristics = new List<CharacteristicElement>();

        /// <summary>
        /// Добавление новой характеристики в список
        /// </summary>
        /// <param name="name">Название зарактеристики</param>
        /// <param name="text">Текст характеристики</param>
        public static void Add(string name, string text)
        {
            characteristics.Add(new CharacteristicElement(name, text));
        }

        /// <summary>
        /// Удаление характеристики из списка
        /// </summary>
        /// <param name="id">id характеристики в списке</param>
        public static void Remove(int id)
        {
            characteristics.RemoveAt(id);
        }

        /// <summary>
        /// Метод, возвращающий список характеристик
        /// </summary>
        /// <returns></returns>
        public static List<CharacteristicElement> Get()
        {
            return characteristics;
        }

        /// <summary>
        /// Получаем количество характеристик в списке
        /// </summary>
        /// <returns>count</returns>
        public static int Count()
        {
            return characteristics.Count();
        }
    }
}
