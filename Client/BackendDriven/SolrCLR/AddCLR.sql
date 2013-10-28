﻿RAISERROR ('SET Trustworthy',0,0) WITH NOWAIT
DECLARE @sql AS NVARCHAR(MAX)
SET @sql='ALTER DATABASE '+quotename(db_name())+' SET TRUSTWORTHY ON';
EXEC(@sql)

GO

RAISERROR ('CONFIGURE CLR ENABLE',0,0) WITH NOWAIT

GO
sp_configure 'show advanced options', 1;
GO
RECONFIGURE;
GO
sp_configure 'clr enabled', 1;
GO
RECONFIGURE;
GO

PRINT N'Creating [SolrClient]...';
CREATE ASSEMBLY [SolrClient]
AUTHORIZATION [dbo]
FROM 'F:\dev\solr\Client\BackendDriven\SolrCLR\bin\Debug\SolrClient.dll'
WITH PERMISSION_SET = UNSAFE;

GO

PRINT N'Creating [SolrCLR]...';
CREATE ASSEMBLY [SolrCLR]
AUTHORIZATION [dbo]
FROM 'F:\dev\solr\Client\BackendDriven\SolrCLR\bin\Debug\SolrCLR.dll'
WITH PERMISSION_SET = UNSAFE;

GO

GO
PRINT N'Creating [dbo].[csp_solr_addrecord]...';


GO
CREATE PROCEDURE [dbo].[csp_solr_addrecord]
@endpoint NVARCHAR (4000), @id NVARCHAR (4000), @recordclass NVARCHAR (4000), @idrecord NVARCHAR (4000), @descriptive NVARCHAR (4000), @content NVARCHAR (4000), @commit BIT
AS EXTERNAL NAME [SolrCLR].[StoredProcedures].[csp_solr_addrecord]


GO
PRINT N'Creating [dbo].[csp_solr_fetch]...';


GO
CREATE PROCEDURE [dbo].[csp_solr_fetch]
@endpoint NVARCHAR (4000), @query NVARCHAR (4000)
AS EXTERNAL NAME [SolrCLR].[StoredProcedures].[csp_solr_fetch]


GO
PRINT N'Creating [dbo].[csp_solr_addfile]...';


GO
CREATE PROCEDURE [dbo].[csp_solr_addfile]
@endpoint NVARCHAR (4000), @id NVARCHAR (4000), @recordclass NVARCHAR (4000), @idrecord NVARCHAR (4000), @descriptive NVARCHAR (4000), @fileName NVARCHAR (4000), @binaryData NVARCHAR (4000), @commit BIT
AS EXTERNAL NAME [SolrCLR].[StoredProcedures].[csp_solr_addfile]


GO
PRINT N'Creating [dbo].[csp_solr_delete]...';


GO
CREATE PROCEDURE [dbo].[csp_solr_delete]
@endpoint NVARCHAR (4000), @id NVARCHAR (4000), @commit BIT
AS EXTERNAL NAME [SolrCLR].[StoredProcedures].[csp_solr_delete]


GO
PRINT N'Creating [dbo].[csp_solr_commit]...';


GO
CREATE PROCEDURE [dbo].[csp_solr_commit]
@endpoint NVARCHAR (4000)
AS EXTERNAL NAME [SolrCLR].[StoredProcedures].[csp_solr_commit]


GO
PRINT N'Creating [dbo].[csp_solr_rollback]...';


GO
CREATE PROCEDURE [dbo].[csp_solr_rollback]
@endpoint NVARCHAR (4000)
AS EXTERNAL NAME [SolrCLR].[StoredProcedures].[csp_solr_rollback]


GO
PRINT N'Creating [dbo].[csp_solr_optimize]...';


GO
CREATE PROCEDURE [dbo].[csp_solr_optimize]
@endpoint NVARCHAR (4000)
AS EXTERNAL NAME [SolrCLR].[StoredProcedures].[csp_solr_optimize]

