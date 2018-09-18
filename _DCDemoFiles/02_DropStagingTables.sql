USE SSISPGenDemo
GO

DECLARE @sqlcmd NVARCHAR(MAX);

DECLARE dropcur CURSOR 
FOR
SELECT [sqlcmd] = 'DROP TABLE ' + s.name + '.' + t.name + ';'
FROM 
			sys.tables	AS t
INNER JOIN	sys.schemas AS s ON t.schema_id = s.schema_id
WHERE 1=1
AND s.name = 'Staging'

    OPEN dropcur  
    
	FETCH NEXT FROM dropcur 
	INTO @sqlcmd   

    WHILE @@FETCH_STATUS = 0  
    BEGIN  

        EXEC(@sqlcmd)

        FETCH NEXT FROM dropcur 
		INTO @sqlcmd  
	
	END  

    CLOSE dropcur  

DEALLOCATE dropcur  
;

TRUNCATE TABLE	[etl].[ETL_Audit]
TRUNCATE TABLE	[etl].[ETL_ErrorLog]
;