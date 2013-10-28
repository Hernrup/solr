#region license
// Copyright (c) 2007-2010 Mauricio Scheffer
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using SolrNet.Utils;

namespace SolrNet.Commands {
	/// <summary>
	/// Adds / updates documents to solr
	/// </summary>
	/// <typeparam name="T">Document type</typeparam>
	public class AddCommand : ISolrCommand {
        private readonly IEnumerable<KeyValuePair<string, string>> document = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// Adds / updates documents to solr
        /// </summary>
        /// <param name="documents"></param>
        /// <param name="serializer"></param>
        /// <param name="parameters"></param>
        public AddCommand(IEnumerable<KeyValuePair<string, string>> document) {
            this.document = document;
        }

        /// <summary>
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        /// <summary>
        /// Serializes command to Solr XML
        /// </summary>
        /// <returns></returns>
        public string ConvertToXml() {

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.ConformanceLevel = ConformanceLevel.Fragment;

            string xml;
            using (var sw = new StringWriter()) {
                using (var xw = XmlWriter.Create(sw, settings)) {
                    
                    xw.WriteStartElement("add");
                    xw.WriteStartElement("doc");

                    foreach(KeyValuePair<string, string> field in this.document){
                        xw.WriteStartElement("field");
                        xw.WriteAttributeString("name", field.Key);
                        xw.WriteRaw(field.Value);
                        xw.WriteEndElement();
                    }

                    xw.WriteEndElement();
                    xw.WriteEndElement();
                }

                xml = sw.ToString();
            }
            
            return xml;
        }

	    public string Execute(ISolrConnection connection) {
	        var xml = ConvertToXml();
			return connection.Post("/update", xml);
		}
	}
}