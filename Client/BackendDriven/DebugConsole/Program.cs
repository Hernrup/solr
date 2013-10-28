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
            Actions.addRecord(@"http://MH:8983/solr/lime", "tmp002_1", "classname2", "002", "Some content2", true);

            byte[] b = System.IO.File.ReadAllBytes(@"F:\dev\solr\Client\BackendDriven\Client\Presentation.pdf");
            string v = Convert.ToBase64String(b);
            Actions.addFile(@"http://MH:8983/solr/lime", "tmp002_2", "classname", "001", "Filename.pdf", v, true);

        }
    }
}
