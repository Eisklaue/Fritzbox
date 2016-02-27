using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace Fritzbox.src
{
    class login
    {
        private string username;
        private string password;
        public string sid;

        public login (string username, string password)
        {
            this.password = password;
            this.username = username;
            this.sid = GetSessionId();
        }
        private string GetSessionId()
        {
            XDocument doc = XDocument.Load(@"http://fritz.box/login_sid.lua");
            this.sid = GetValue(doc, "SID");
            if (this.sid == "0000000000000000")
            {
                string challenge = GetValue(doc, "Challenge");
                string uri = @"http://fritz.box/login_sid.lua?username=" +
               this.username + @"&response=" + GetResponse(challenge);
                doc = XDocument.Load(uri);
                this.sid = GetValue(doc, "SID");
            }
            return this.sid;
        }
        private string GetResponse(string challenge)
        {
            return challenge + "-" + GetMD5Hash(challenge + "-" + this.password);
        }

        private string GetMD5Hash(string input)
        {
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Unicode.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private string GetValue(XDocument doc, string name)
        {
            XElement info = doc.FirstNode as XElement;
            return info.Element(name).Value;
        }

        public string getSid()
        {
            return this.sid;
        }

    }
}
