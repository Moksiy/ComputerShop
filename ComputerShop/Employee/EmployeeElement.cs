using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public class EmployeeElement
    {
        public byte[] PhotoData { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Shop { get; set; }
        public string Position { get; set; }

        public EmployeeElement(byte[] image, string lastName, string name, string patron, string shop, string position)
        {
            this.PhotoData = image;
            this.LastName = lastName;
            this.FirstName = name;
            this.Patronymic = patron;
            this.Shop = shop;
            this.Position = position;
        }
    }
}
