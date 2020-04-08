using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public class MessageElement
    {
        public string Message { get; set; }

        public MessageElement(string text)
        {
            this.Message = text;
        }
    }
}
