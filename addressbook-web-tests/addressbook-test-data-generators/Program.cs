using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using WebAddressbookTests;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = Convert.ToInt32(args[0]);
            string format = args[3];

            List<GroupData> groups = new List<GroupData>();
            StreamWriter writer = new StreamWriter(args[1]);
            for (int i = 0; i<count; i++)
            {
                groups.Add(new GroupData(TestBase.GenerateRandomString(10))
                    {
                    Header = TestBase.GenerateRandomString(10),
                    Footer = TestBase.GenerateRandomString(10)
                    });
                
            }
            if (format == "csv")
            {
                writeGroupsToCsvFile(groups, writer);
            }
            else if (format == "xml") 
            {
                writeGroupsToXmlFile(groups, writer);

            }
            else
            {
                System.Console.Out.Write("Unrecognised format " + format);
            }
            writer.Close();

        }

        static void writeGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.Name, group.Header, group.Footer));
            }
        }

        static void writeGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups); 
        }
    }
}
