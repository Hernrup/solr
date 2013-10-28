using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolrNet;
using Client;

namespace Tests
{
    [TestClass]
    public class BasicStaticData
    {

        //[TestInitialize()]
        //public void FixtureSetup() {
        //    try {
        //        Startup.Init<Document>("http://localhost:8983/solr/lime");
        //    } catch (Exception e) {
        //        Console.WriteLine("Warn: Startup failed, probably called twice, " + e.Message);
        //    }
        //}

        //[TestMethod]
        //public void Add()
        //{
            
        //    var p = new Document
        //    {
        //        Id = "SP2514N",
        //        Manufacturer = "Samsung Electronics Co. Ltd.",
        //        Categories = new[] {
        //    "electronics",
        //    "hard drive",
        //},
        //        Price = 92,
        //        InStock = true,
        //    };

        //    var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Document>>();
        //    solr.Add(p);
        //    solr.Commit();
        //}

        //[TestMethod]
        //public void Query()
        //{
           
        //    var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Document>>();
        //    var results = solr.Query(new SolrQueryByField("id", "SP2514N"));
        //    Assert.AreEqual(1, results.Count);
        //    Console.WriteLine(results[0].Price);
        //}


        //[TestMethod]
        //public void AddPdf()
        //{
        //    var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Document>>();

        //    using (FileStream fileStream = File.OpenRead(@"C:\dev\solr\instance\Client\Client\Presentation.pdf"))
        //    {
        //        var response =
        //            solr.Extract(
        //                new ExtractParameters(fileStream, "doc1")
        //                {
        //                    ExtractFormat = ExtractFormat.Text,
        //                    ExtractOnly = false,
        //                    Fields = new[] { 
        //                        new ExtractField("idrecord", "1234"), 
        //                        new ExtractField("class", "classname") 
        //                    }
        //                }
        //                );
        //    }

        //    solr.Commit();
        //}

        //[TestMethod]
        //public void GetPdfFileHighlighted()
        //{
        //    var solr = ServiceLocator.Current.GetInstance<ISolrOperations<Document>>();
        //    var results = solr.Query(new SolrQueryByField("id", "doc1"), new QueryOptions
        //    {
        //        Highlight = new HighlightingParameters
        //        {
        //            Fields = new[] { "content" },
        //        }
        //    });
        //    if (results.Count > 0)
        //    {
        //        foreach (var h in results.Highlights[results[0].Id])
        //        {
        //            Console.WriteLine("{0}: {1}", h.Key, string.Join(", ", h.Value));
        //        }
        //    }
        //}
    }


}
