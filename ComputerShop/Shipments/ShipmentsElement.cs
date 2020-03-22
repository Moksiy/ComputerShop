using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public class ShipmentsElement
    {
        public int ID { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public string Supplier { get; set; }
        public string Shop { get; set; }
        public DateTime Date { get; set; }

        public ShipmentsElement(int id, string prod, int quan, string suppl, DateTime date, string shop)
        {
            this.ID = id;
            this.Product = prod;
            this.Quantity = quan;
            this.Supplier = suppl;
            this.Date = date;
            this.Shop = shop;
        }
    }
}
