using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public class Clients
    {
        public string ID { get; set; }
        public string CardID { get; set; }
        public string FIO { get; set; }
        public string Phone { get; set; }

        public Clients(string id, string card, string fio, string phone)
        {
            this.ID = id;
            this.CardID = card;
            this.FIO = fio;
            this.Phone = phone;
        }
    }
}
