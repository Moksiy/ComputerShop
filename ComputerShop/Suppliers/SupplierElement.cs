using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public class SupplierElement
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public SupplierElement(int id, string name, string address, string number)
        {
            this.ID = id;
            this.Name = name;
            this.Address = address;
            this.PhoneNumber = number;
        }
    }
}
