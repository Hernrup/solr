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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using SolrNet.Commands.Parameters;
using SolrNet.Utils;

namespace SolrNet.Commands {
    /// <summary>
    /// Deletes document(s), either by id or by query
    /// </summary>
	public class DeleteCommand : ISolrCommand {

        string id;

        public DeleteCommand(string id) {
            this.id = id;
        }


        public string ConvertToXml() {

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            settings.ConformanceLevel = ConformanceLevel.Fragment;

            string xml;
            using (var sw = new StringWriter()) {
                using (var xw = XmlWriter.Create(sw, settings)) {

                    xw.WriteStartElement("delete");
                    xw.WriteStartElement("id");
                    xw.WriteRaw(this.id);
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