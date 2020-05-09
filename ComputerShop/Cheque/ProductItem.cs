using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public class ProductItem
    {
        public string Detail { get; set; }
        public string Quan { get; set; }
        public string Total { get; set; }

        public ProductItem(string d, string q, string t)
        {
            this.Detail = d;
            this.Quan = q;
            this.Total = t;
        }
    }
}
