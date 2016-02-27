using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Fritzbox.src
{
    class SwitchState
    {
        private string username { get; set; }
        private string sid { get; set; }

        public SwitchState(string username, string sid)
        {
            this.username = username;
            this.sid = sid;
        }

        public string GetSwitchList()
        {
            //example: http://fritz.box/webservices/homeautoswitch.lua?sid=121950924bad6457&switchcmd=getswitchlist

            Uri uri = new Uri(@"http://fritz.box/webservices/homeautoswitch.lua?sid=" + this.sid + "&switchcmd=getswitchlist");
            HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
            HttpWebResponse respone = request.GetResponse() as HttpWebResponse;
            StreamReader sr = new StreamReader(respone.GetResponseStream());
            return sr.ReadToEnd();
        }

        public string GetSwitchPower(string ain)
        {
            //example: http://fritz.box/webservices/homeautoswitch.lua?sid=8e9f93a1a88d4208&switchcmd=getswitchpower&ain=34:31:C4:D4:5C:39

            Uri uri = new Uri(@"http://fritz.box/webservices/homeautoswitch.lua?sid=" + this.sid + "&switchcmd=getswitchpower&ain=" + ain);
            HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
            HttpWebResponse respone = request.GetResponse() as HttpWebResponse;
            StreamReader sr = new StreamReader(respone.GetResponseStream());
            return sr.ReadToEnd();

        }
    }
}
