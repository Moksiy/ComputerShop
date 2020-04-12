using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public class ClientElement
    {
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Patronom { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Card { get; set; }
        public string Bonus { get; set; }

        public ClientElement(string ln, string nm, string p, string em, string phn, string crd, string b)
        {
            this.LastName = ln;
            this.Name = nm;
            this.Patronom = p;
            this.Email = em;
            this.Phone = phn;
            this.Card = crd;
            this.Bonus = b;
        }
    }
}
