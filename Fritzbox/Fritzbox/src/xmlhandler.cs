using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml;

namespace Fritzbox.src
{
    class XMLHandler
    {
        private string path = @".\config.xml";
        public string username { get; set; }
        public string password { get; set; }
        public string sid { get; set; }

        private XElement config;

        public XMLHandler()
        {
            if (!File.Exists(this.path))
            {
                CreateXMLFile();
            }
            else
            { 

                ReadXMLFile();
                GetValues();
            }
        }

        private void CreateXMLFile()
        {
            XDocument xDoc = new XDocument(
                new XElement("Config",
                    new XElement("Authentication",
                        new XElement("Username"),
                        new XElement("Password")),
                new XElement("Sid")));

            StringWriter sw = new StringWriter();
            XmlWriter xWrite = XmlWriter.Create(sw);
            xDoc.Save(xWrite);
            xWrite.Close();

            // Save to Disk
            xDoc.Save(this.path);
            Console.WriteLine("Xml Saved");
        }

        private void ReadXMLFile()
        {
            try
            {
                XmlReader xRead = XmlReader.Create(this.path);
                this.config = XElement.Load(xRead);
                xRead.Close();
            }
            catch (FileNotFoundException ex)
            {
                CreateXMLFile();
            }
        }

        public void GetValues()
        {
            this.sid = config.Element("Sid").Value;
            this.username = config.Element("Authentication").Element("Username").Value;
            this.password = config.Element("Authentication").Element("Password").Value;
        }

        public void SetValues(string key, string value)
        {
            XDocument xDoc = XDocument.Load(path);

            switch (key)
            {
                case "Username":
                    xDoc.Root.Element("Authentication").Element("Username").Value = value;
                    break;
                case "Password":
                    xDoc.Root.Element("Authentication").Element("Password").Value = value;
                    break;
                case "Sid":
                    xDoc.Root.Element("Sid").Value = value;
                    break;
            }
            xDoc.Save(path);
        }
    }
}
