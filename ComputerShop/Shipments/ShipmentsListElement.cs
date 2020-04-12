using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public class ShipmentsListElement
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int Quan { get; set; }
        public ShipmentsListElement(int id, int pid, int q)
        {
            this.ID = id;
            this.ProductID = pid;
            this.Quan = q;
        }
    }
}
