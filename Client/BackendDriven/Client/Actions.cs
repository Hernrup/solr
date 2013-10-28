using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using System.Xml;
using SolrNet;
using SolrNet.Commands;
using SolrNet.Impl;
using SolrNet.Utils;

namespace Client {
    public static class Actions {
        public static void addRecord(string endpoint, string id, string recordclass, string idrecord, string content, bool commit) {

            var connection = new SolrConnection(endpoint);

            var param = new List<KeyValuePair<string, string>>();
            param.Add(KV.Create("id",id));
            param.Add(KV.Create("class", recordclass));
            param.Add(KV.Create("idrecord", idrecord));
            param.Add(KV.Create("text", content));

            var commAdd = new AddCommand(param);
            var response = commAdd.Execute(connection);

            if (commit) {
                var commCommit = new CommitCommand();
                commCommit.Execute(connection);
            }
        }

        public static void addFile(string endpoint, string id, string recordclass, string idrecord, string fileName, string binaryData, bool commit) {
            
            var connection = new SolrConnection(endpoint);

            byte[] data = Convert.FromBase64String(binaryData);
            MemoryStream stream = new MemoryStream(data);

            var param = new ExtractParameters(stream, id, fileName) {
                ExtractFormat = ExtractFormat.Text,
                ExtractOnly = false,
                Fields = new[] { 
                            new ExtractField("idrecord", "1234"), 
                            new ExtractField("class", "classname")
                        }
            };

            var command = new ExtractCommand(param);
            var response = command.Execute(connection);

            if (commit) {
                var commCommit = new CommitCommand();
                commCommit.Execute(connection);
            }
        }

       
        public static void delete(string endpoint, string id) {

        }

        
    }


}
