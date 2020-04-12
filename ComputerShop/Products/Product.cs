using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Product(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }
    }
}
