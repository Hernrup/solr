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
    public static class API {
        
        public static void addRecord(string endpoint, string id, string recordclass, int idrecord, string descriptive, string content, bool commit) {

            var connection = new SolrConnection(endpoint);

            var param = new List<KeyValuePair<string, string>>();
            param.Add(KV.Create("id",id));
            param.Add(KV.Create("class", recordclass));
            param.Add(KV.Create("idrecord", idrecord.ToString()));
            param.Add(KV.Create("text", content));
            param.Add(KV.Create("title", descriptive));

            var commAdd = new AddCommand(param);
            var response = commAdd.Execute(connection);

            if (commit) {
                var commCommit = new CommitCommand();
                commCommit.Execute(connection);
            }
        }

        public static void addFile(string endpoint, string id, string recordclass, int idrecord, string descriptive, string fileName, string binaryData, bool commit) {
            
            var connection = new SolrConnection(endpoint);

            byte[] data = Convert.FromBase64String(binaryData);
            MemoryStream stream = new MemoryStream(data);

            var param = new ExtractParameters(stream, id, fileName) {
                ExtractFormat = ExtractFormat.Text,
                ExtractOnly = false,
                Fields = new[] { 
                            new ExtractField("idrecord", idrecord.ToString()), 
                            new ExtractField("class", recordclass),
                            new ExtractField("title", descriptive)
                        }
            };

            var command = new ExtractCommand(param);
            var response = command.Execute(connection);

            if (commit) {
                var commCommit = new CommitCommand();
                commCommit.Execute(connection);
            }
        }

       
        public static void delete(string endpoint, string id, bool commit) {
            var connection = new SolrConnection(endpoint);

            var commAdd = new DeleteCommand(id);
            var response = commAdd.Execute(connection);

            if (commit) {
                var commCommit = new CommitCommand();
                commCommit.Execute(connection);
            }
        }

        public static List<Dictionary<string, string>> fetch(string endpoint, string searchString, int start, int rows) {
            var connection = new SolrConnection(endpoint);
            var command = new FetchCommand(searchString, start, rows, new string[] { "id", "class", "idrecord", "title" });
            var result = new List<Dictionary<string,string>>();

            var resultXml = command.Execute(connection);

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(resultXml);

            XmlNodeList xnList = xml.SelectNodes("/response/result/doc");
            foreach (XmlNode doc in xnList) {
                var dic = new Dictionary<string, string>();
                foreach (XmlNode node in doc.ChildNodes) {
                    dic.Add(node.Attributes["name"].Value,node.InnerText);
                }
                result.Add(dic);
            }



            return result;
        }

        public static void commit(string endpoint) {
            var connection = new SolrConnection(endpoint);
            var command = new CommitCommand();
            command.Execute(connection);
        }

        public static void rollback(string endpoint) {
            var connection = new SolrConnection(endpoint);
            var command = new RollbackCommand();
            command.Execute(connection);
        }

        public static void optimize(string endpoint) {
            var connection = new SolrConnection(endpoint);
            var command = new OptimizeCommand();
            command.Execute(connection);
        }

        
    }


}
