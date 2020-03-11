using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public static class CaptchaBuild
    {
        public static string Refresh()
        {
            string captcha = default;

            Random rand = new Random();

            for (int i = 0; i < 4; i++)
            {
                captcha += (char)rand.Next('A', 'Z' + 1);
            }

            return captcha;
        }
    }
}
