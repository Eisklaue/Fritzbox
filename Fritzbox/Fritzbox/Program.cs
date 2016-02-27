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
            string input;
            string[] separator = { "," };

            XMLHandler xHandler = new XMLHandler();

            if (xHandler.username == "")
            {
                Console.WriteLine("Please enter Username: ");
                input = Console.ReadLine();
                xHandler.SetValues("Username", input);
            }

            if (xHandler.password == "")
            {
                Console.WriteLine("Please enter Password: ");
                input = Console.ReadLine();
                xHandler.SetValues("Password", input);
            }
            Login log = new Login(xHandler.username,xHandler.password);
            xHandler.SetValues("Sid", log.sid);

            SwitchState sState = new SwitchState(xHandler.username, xHandler.sid);

            string[] ains = sState.GetSwitchList().Split(separator, StringSplitOptions.RemoveEmptyEntries);

            foreach (string ain in ains)
            {
                Console.WriteLine("Ain: " + ain + " mW: " + sState.GetSwitchPower(ain));
            }
            
        }
    }
}
