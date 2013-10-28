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
using SolrNet.Utils;

namespace SolrNet.Commands {
    /// <summary>
    /// Sends documents to solr for extraction
    /// </summary>
    public class FetchCommand : ISolrCommand {

        private readonly int? start;
        private readonly int? rows;
        private readonly string[] fields;
        private readonly string query;

        public FetchCommand(string query, int? start, int? rows, string[] fields) {
            this.start = start;
            this.rows = rows;
            this.fields = fields;
            this.query = query;
        }

        public string Execute(ISolrConnection connection) {
            var parameters = new List<KeyValuePair<string, string>>();

            parameters.Add(KV.Create("q", this.query));

            if (this.start != null)
                parameters.Add(KV.Create("start", this.start.ToString()));

            if (this.start != null)
                parameters.Add(KV.Create("rows", this.rows.ToString()));

            if (this.fields.Length > 0)
                parameters.Add(KV.Create("fl", string.Join(",", this.fields)));

            return connection.Get("/select", parameters);
        }

     
    }
}