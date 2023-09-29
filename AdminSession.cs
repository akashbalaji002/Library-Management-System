using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_Project
{
    public static class AdminSession
    {
        public static bool IsLoggedIn { get; private set; }

        public static void Login()
        {
            IsLoggedIn = true;
        }

        public static void Logout()
        {
            IsLoggedIn = false;
        }

        
    }

}
