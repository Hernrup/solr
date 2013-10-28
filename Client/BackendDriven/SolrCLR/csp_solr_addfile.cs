//------------------------------------------------------------------------------
// <copyright file="CSSqlStoredProcedure.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Client;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void csp_solr_addfile(string endpoint, string id, string recordclass, int idrecord, string descriptive, string fileName, string binaryData, bool commit)
    {
        API.addFile(endpoint, id, recordclass, idrecord, descriptive, fileName, binaryData, commit);
    }
}
