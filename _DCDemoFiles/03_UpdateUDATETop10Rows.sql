USE [ContosoRetailDW]
GO

DECLARE @sqlcmd1	NVARCHAR(MAX);
DECLARE @sqlcmd2	NVARCHAR(MAX);
DECLARE @newMaxDate	NVARCHAR(MAX);

--	
-------------------------------------------------------------

DECLARE dropcur CURSOR 
FOR
SELECT 

[sqlcmd1]	=	'
					SELECT @newMaxDate = DATEADD(DAY, 1, MAX(UpdateDate))
					FROM [' + s.name + '].[' + t.name + '];
				',
[sqlcmd2]	=	'
					UPDATE TOP(10) u
					SET [UpdateDate] = @newMaxDate
					FROM [' + s.name + '].[' + t.name + '] AS u
				'
FROM 
			sys.tables	AS t
INNER JOIN	sys.schemas AS s ON t.schema_id = s.schema_id
WHERE 1=1
AND s.name = 'dbo'
AND t.name <> 'DimDate'

    OPEN dropcur  
    
	FETCH NEXT FROM dropcur 
	INTO @sqlcmd1, @sqlcmd2  

    WHILE @@FETCH_STATUS = 0  
    BEGIN  

		EXEC sp_executesql		
			@sqlcmd1
		,	N'@newMaxDate DATETIME OUTPUT'
		,	@newMaxDate	=	@newMaxDate OUTPUT
		;

		EXEC sp_executesql		
			@sqlcmd2
		,	N'@newMaxDate DATETIME'
		,	@newMaxDate	=	@newMaxDate
		;

        FETCH NEXT FROM dropcur 
		INTO @sqlcmd1, @sqlcmd2
	
	END  

    CLOSE dropcur  

DEALLOCATE dropcur  



