using Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebugConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Actions.addRecord(@"http://localhost:8983/solr/lime/", "classname", 001, "Some content");

            byte[] b = System.IO.File.ReadAllBytes(@"C:\dev\solr\instance\Client\Client\Presentation.pdf");

            Actions.addFile(@"http://localhost:8983/solr/lime/", "classname", 003, b.ToString());
            Console.ReadLine();
        }
    }
}
