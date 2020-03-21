using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public class ShopElement
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Director { get; set; }

        public ShopElement(int id, string name, string address, string number, string dirid)
        {
            this.ID = id;
            this.Name = name;
            this.Address = address;
            this.PhoneNumber = number;
            this.Director = dirid;
        }
    }
}
