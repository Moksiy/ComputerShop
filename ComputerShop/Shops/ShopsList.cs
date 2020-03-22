using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public static class ShopsList
    {
        public static List<ShopElement> Shops = new List<ShopElement>();

        /// <summary>
        /// Добавление элемента в список
        /// </summary>
        public static void Add(ShopElement item)
        {
            Shops.Add(item);
        }

        /// <summary>
        /// Удаление элемента из списка
        /// </summary>
        /// <param name="index"></param>
        public static void Delete(int index)
        {
            Shops.RemoveAt(index);
        }        
    }
}
