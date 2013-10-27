using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Client
{
    public static class Actions
    {
        public static void addRecord(string endpoint, string recordclass, int idrecord, string content)
        {
            Uri uri = new Uri(endpoint+"update/");
            string id = String.Format("{0}_{1}", recordclass, idrecord.ToString());
            string xml;

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.ConformanceLevel = ConformanceLevel.Fragment;            

            using (var sw = new StringWriter())
            {
                using (var xw = XmlWriter.Create(sw, settings))
                {
                    xw.WriteStartElement("Add");
                    xw.WriteStartElement("Doc");

                    xw.WriteStartElement("field");
                    xw.WriteAttributeString("name", "id");
                    xw.WriteRaw(id);
                    xw.WriteEndElement();
                    
                    xw.WriteStartElement("field");
                    xw.WriteAttributeString("name", "class");
                    xw.WriteRaw(recordclass);
                    xw.WriteEndElement();
                    
                    xw.WriteStartElement("field");
                    xw.WriteAttributeString("name", "idrecord");
                    xw.WriteRaw(idrecord.ToString());
                    xw.WriteEndElement();

                    xw.WriteStartElement("field");
                    xw.WriteAttributeString("name", "text");
                    xw.WriteRaw(content);
                    xw.WriteEndElement();

                    xw.WriteEndElement();
                    xw.WriteEndElement();
                }
                xml = sw.ToString();
            }

            postRequest(uri.AddQuery("commit", "true").ToString(),xml);
        }

        public static void addFile(string endpoint, string recordclass, int idrecord, string binaryData)
        {
            Uri uri = new Uri(endpoint + "update/extract/");
            string id = String.Format("{0}_{1}", recordclass, idrecord.ToString());
            uri = uri.AddQuery("literal.id", idrecord.ToString()).AddQuery("commit", "true");
            postRequest(uri.ToString(), binaryData);
          }

        public static void delete(string endpoint, string id)
        {

        }

        private static void postRequest(string url, string data)
        { 
            WebRequest request = WebRequest.Create(url);
            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            dataStream.Close();
            response.Close();
        }

        private static void getRequest(string url)
        { 
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            response.Close();
        }
    }


}
