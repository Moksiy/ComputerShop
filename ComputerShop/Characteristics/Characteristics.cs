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
            if(characteristics.Count >= 1)
                return characteristics;
            return null;
        }

        /// <summary>
        /// Получаем количество записей в списке
        /// </summary>
        /// <returns></returns>
        public static int GetCount()
        {
            return characteristics.Count();
        }

        /// <summary>
        /// Получаем количество характеристик в списке
        /// </summary>
        /// <returns>count</returns>
        public static int Count()
        {
            return characteristics.Count();
        }

        /// <summary>
        /// Чистим список
        /// </summary>
        public static void Clear()
        {
            characteristics.Clear();
        }

        /// <summary>
        /// Возвращаем индекс нужного элемента
        /// </summary>
        /// <returns></returns>
        public static int IndexOf(CharacteristicElement item)
        {
            for(int i = 0; i < characteristics.Count; i++)
            {
                if (characteristics[i].Name == item.Name && characteristics[i].Text == item.Text)
                    return i;                
            }
            return -1;
        }
    }
}
