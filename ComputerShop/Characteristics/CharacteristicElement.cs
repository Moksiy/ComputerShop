using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public class CharacteristicElement
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public CharacteristicElement(string name, string text)
        {
            this.Name = name;
            this.Text = text;
        }
    }
}
