using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerShop
{
    public static class Account
    {
        public static void LogOut()
        {
            User.ID = "";
            User.Login = "";
            User.Password = "";
            User.RoleID = "";
        }
    }
}
