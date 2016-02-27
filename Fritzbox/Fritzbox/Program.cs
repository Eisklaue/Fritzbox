using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Fritzbox.src;

namespace Fritzbox
{
    class Program
    {
        static void Main(string[] args)
        {
            string username = "XXX";
            string password = "XXX";

            login log = new login(username,password);

            string sid = log.GetSessionId();
        }
    }
}
