//------------------------------------------------------------------------------
// <copyright file="CSSqlStoredProcedure.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Client;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures {
    
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void csp_solr_fetch(string endpoint, string query, int start, int rows) {

        var result = API.fetch(endpoint, query, start, rows);

        SqlPipe pipe = SqlContext.Pipe;
        SqlMetaData[] cols = new SqlMetaData[]{
            new SqlMetaData("id", SqlDbType.NVarChar, 1024),
            new SqlMetaData("class", SqlDbType.NVarChar, 1024),
            new SqlMetaData("idrecord", SqlDbType.Int),
            new SqlMetaData("title", SqlDbType.NVarChar, 1024),
        };
       
        SqlDataRecord rec = new SqlDataRecord(cols);
        pipe.SendResultsStart(rec);

        foreach (Dictionary<string,string> dic in result) {
            rec.SetSqlString(0, new SqlString(dic["id"]));
            rec.SetSqlString(1, new SqlString(dic.ContainsKey("class") ? dic["class"] : null));
            rec.SetSqlInt32(2, new SqlInt32(dic.ContainsKey("idrecord") ? Int32.Parse(dic["idrecord"]) : 0));
            rec.SetSqlString(3, new SqlString(dic.ContainsKey("title") ? dic["title"] : null));
            pipe.SendResultsRow(rec);
        }

        pipe.SendResultsEnd();
         
    }
}
