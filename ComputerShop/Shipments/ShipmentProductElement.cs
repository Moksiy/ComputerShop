using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public class ShipmentProductElement
    {
        public string Product { get; set; }
        public int Quan { get; set; }
        public ShipmentProductElement(string name, int q)
        {
            this.Product = name;
            this.Quan = q;
        }
    }
}
