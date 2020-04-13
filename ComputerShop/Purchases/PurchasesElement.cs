using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public class PurchasesElement
    {
        public string ID { get; set; }
        public string Client { get; set; }
        public string Employee { get; set; }
        public string Shop { get; set; }
        public string Date { get; set; }
        public string Cost { get; set; }
        public string ProductCart { get; set; }

        public PurchasesElement(string id, string client, string empl, string shop, string date, string cost, string cart)
        {
            this.ID = id;
            this.Client = client;
            this.Employee = empl;
            this.Shop = shop;
            this.Date = date;
            this.Cost = cost;
            this.ProductCart = cart;
        }
    }
}
