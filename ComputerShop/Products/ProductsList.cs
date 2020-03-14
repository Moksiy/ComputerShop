using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public static class ProductsList
    {
        public static List<ProductElement> products = new List<ProductElement>();    
        
        /// <summary>
        /// добавление нового элемента
        /// </summary>
        /// <param name="item"></param>
        public static void Add(ProductElement item)
        {
            products.Add(item);
        }

        /// <summary>
        /// Удаление элемента
        /// </summary>
        public static void Delete()
        {

        }

    }
}
