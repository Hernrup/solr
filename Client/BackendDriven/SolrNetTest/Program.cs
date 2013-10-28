using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using SolrNet;

namespace SolrNetTest {
    class Program {
        static void Main(string[] args) {
            AddPdf();
        }

        public static void AddPdf() {
            Startup.Init<Document>("http://MH:8983/solr/lime");
            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Document>>();

            //using (FileStream fileStream = File.OpenRead(@"F:\dev\solr\Client\BackendDriven\Client\Presentation.pdf")) {
            //    var response =
            //        solr.Extract(
            //            new ExtractParameters(fileStream, "doc2") {
            //                ExtractFormat = ExtractFormat.Text,
            //                ExtractOnly = false,
            //                Fields = new[] { 
            //                    new ExtractField("idrecord", "1234"), 
            //                    new ExtractField("class", "classname") 
            //                }
            //            }
            //            );
            //}

             var p = new Document {
                                      Id = "SP2514N2",
                                      Manufacturer = "Samsung Electronics Co. Ltd.",
                                      Categories = new[] {
            "electronics",
            "hard drive",
        },
                                      Price = 92,
                                      InStock = true,
                                  };

             solr.Add(p);
            solr.Commit();
        }
    }
}
