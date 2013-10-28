using Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Data;

namespace DebugConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //API.addRecord(@"http://MH:8983/solr/lime", "tmp002_1", "classname2", 002, "test", "Some content2", true);

            //byte[] b = System.IO.File.ReadAllBytes(@"F:\dev\solr\Client\BackendDriven\Client\Presentation.pdf");
            //string v = Convert.ToBase64String(b);
            //API.addFile(@"http://MH:8983/solr/lime", "tmp002_2", "classname", "001", "Filename.pdf", v, true);

            //API.delete(@"http://MH:8983/solr/lime", "tmp002_2", true);

  

            var result = API.fetch(@"http://MH:8983/solr/lime", "doc", 0, 100);

          
            foreach (Dictionary<string, string> dic in result) {
                var b = dic["idrecord"];
                var c = Int32.Parse(dic["idrecord"]);
            }


            //new SqlInt32(Int32.Parse("003"));

        }
    }
}
