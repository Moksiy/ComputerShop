using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public static class ShipmentProductsList
    {
        public static List<Product> ProductList = new List<Product>();

        /// <summary>
        /// Добавление продукта в список
        /// </summary>
        /// <param name="item"></param>
        public static void Add(Product item)
        {
            ProductList.Add(item);
        }

        /// <summary>
        /// Очистка списка
        /// </summary>
        public static void Clear()
        {
            ProductList.Clear();
        }
    }
}
