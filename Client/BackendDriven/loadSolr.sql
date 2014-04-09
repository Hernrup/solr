--EXEC csp_solr_addrecord N'http://5g1515j:8983/solr/lime','test1','tmpcalss',1,'Description text','indexed text',true

DECLARE 
	@endpoint NVARCHAR(512),
	@id NVARCHAR(512),
	@recordClass NVARCHAR(512),
	@idrecord INT,
	@descriptive NVARCHAR(1024),
	@filename NVARCHAR(1024),
	@binaryData VARBINARY(MAX),
	@commit BIT,
	@content NVARCHAR(MAX)
	

SET @endpoint = N'http://5g1515j:8983/solr/lime'

--document
DECLARE c CURSOR FORWARD_ONLY STATIC FOR
SELECT 
'document_'+CAST(iddocument AS NVARCHAR(64)),
'document',
iddocument,
comment,
'doc.'+fileextension,
data,
0
FROM dbo.document d
LEFT JOIN [file] f ON d.document = f.idfile
WHERE d.document IS NOT NULL	
OPEN c
FETCH NEXT FROM c INTO @id,@recordClass,@idrecord,@descriptive,@filename,@binaryData,@commit
WHILE @@FETCH_STATUS = 0
BEGIN 
	EXEC csp_solr_addfile @endpoint,@id,@recordClass,@idrecord,@descriptive,@filename,@binaryData,@commit
	FETCH NEXT FROM c INTO @id,@recordClass,@idrecord,@descriptive,@filename,@binaryData,@commit
END
CLOSE c
DEALLOCATE c
EXEC csp_solr_commit @endpoint

--person
DECLARE c CURSOR FORWARD_ONLY STATIC FOR
SELECT 
'person_'+CAST(idperson AS NVARCHAR(64)),
'person',
idperson,
d.name,
d.name+' '+d.firstname+' '+d.lastname+' '+d.email,
0
FROM dbo.person d
WHERE status = 0
OPEN c
FETCH NEXT FROM c INTO @id,@recordClass,@idrecord,@descriptive,@content,@commit
WHILE @@FETCH_STATUS = 0
BEGIN 
	EXEC csp_solr_addrecord @endpoint,@id,@recordClass,@idrecord,@descriptive,@content,@commit
	FETCH NEXT FROM c INTO @id,@recordClass,@idrecord,@descriptive,@content,@commit
END
CLOSE c
DEALLOCATE c
EXEC csp_solr_commit @endpoint

--company
DECLARE c CURSOR FORWARD_ONLY STATIC FOR
SELECT 
'company_'+CAST(idcompany AS NVARCHAR(64)),
'company',
idcompany,
d.name,
d.country+' '+d.phone+' '+d.postaladdress1+' '+d.postalzipcode,
0
FROM dbo.company d
WHERE status = 0
OPEN c
FETCH NEXT FROM c INTO @id,@recordClass,@idrecord,@descriptive,@content,@commit
WHILE @@FETCH_STATUS = 0
BEGIN 
	EXEC csp_solr_addrecord @endpoint,@id,@recordClass,@idrecord,@descriptive,@content,@commit
	FETCH NEXT FROM c INTO @id,@recordClass,@idrecord,@descriptive,@content,@commit
END
CLOSE c
DEALLOCATE c
EXEC csp_solr_commit @endpoint