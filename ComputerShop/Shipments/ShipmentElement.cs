using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public class ShipmentElement
    {
        public int ID { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public string Supplier { get; set; }
        public DateTime Date { get; set; }

        public ShipmentElement(int id, string prod, int quan, string suppl, DateTime date)
        {
            this.ID = id;
            this.Product = prod;
            this.Quantity = quan;
            this.Supplier = suppl;
            this.Date = date;
        }
    }
}
