using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public class Costs
    {
        public int ID { get; set; }
        public double Cost { get; set; }
        public Costs(int id, double cost)
        {
            this.ID = id;
            this.Cost = cost;
        }
    }
}
