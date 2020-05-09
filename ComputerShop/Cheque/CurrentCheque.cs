using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public static class CurrentCheque
    {
        public static int Type { get; set; }
        public static string ID { get; set; }
        public static string Client { get; set; }
        public static string Employee { get; set; }
        public static string Date { get; set; }
        public static string Cost { get; set; }
        public static List<ProductItem> Cart = new List<ProductItem>();
    }
}
