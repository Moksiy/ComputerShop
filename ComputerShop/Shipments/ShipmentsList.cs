using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public static class ShipmentsList
    {
        public static List<ShipmentsListElement> list = new List<ShipmentsListElement>();

        /// <summary>
        /// Удаляем элемент по айдишнику
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(int id)
        {
            for(int i = 0; i < list.Count; i++)
            {
                if (list[i].ID == id)
                    list.RemoveAt(id);
            }
        }
    }
}
