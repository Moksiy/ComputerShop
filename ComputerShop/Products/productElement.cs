using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public class ProductElement
    {
        public int ProductID { get; set; }
        public byte[] PhotoData { get; set; }
        public string Name { get; set; }
        public string Manufact { get; set; }
        public string Articul { get; set; }
        public int Cost { get; set; }

        public ProductElement(int id, byte[] photo, string name, string manufact, string articul, int cost)
        {
            this.ProductID = id;
            this.PhotoData = photo;
            this.Name = name;
            this.Manufact = manufact;
            this.Articul = articul;
            this.Cost = cost;
        }
    }
}
