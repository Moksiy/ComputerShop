﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public class RepairElement
    {
        public string ID { get; set; }
        public string Shop { get; set; }
        public string Client { get; set; }
        public string Employee { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public string EndingDate { get; set; }
        public string Total { get; set; }
        public string Cart { get; set; }

        public RepairElement(string id, string shop, string client, string employee,
            string status, string date, string edate, string total, string cart)
        {
            this.ID = id;
            this.Shop = shop;
            this.Client = client;
            this.Employee = employee;
            this.Status = status;
            this.Date = date;
            this.EndingDate = edate;
            this.Total = total;
            this.Cart = cart;
        }
    }
}
