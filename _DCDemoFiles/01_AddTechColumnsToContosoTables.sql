SELECT s.name, t.name, c1.name, c2.name
FROM 
			sys.tables	AS t
INNER JOIN	sys.schemas AS s	ON t.[schema_id] = s.[schema_id]
LEFT JOIN	sys.columns AS c1	ON t.[object_id] = c1.[object_id]	AND c1.[name] IN ('UpdateDate')
LEFT JOIN	sys.columns AS c2	ON t.[object_id] = c2.[object_id]	AND c2.[name] IN ('LoadDate')
WHERE 1=1
ORDER BY s.name, t.name 

---------------------------------------------------

SELECT 'ALTER TABLE [' + s.name + '].[' + t.name + '] ADD [OID] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID();'
FROM 
			sys.tables	AS t
INNER JOIN	sys.schemas AS s	ON t.[schema_id] = s.[schema_id]
WHERE t.[name] LIKE 'DIM%'

---------------------------------------------------

SELECT 'ALTER TABLE [' + s.name + '].[' + t.name + '] DROP CONSTRAINT [' + d.name + '];' 
FROM 
			sys.tables				AS t
INNER JOIN	sys.schemas				AS s	ON t.[schema_id]	=	s.[schema_id]
INNER JOIN	sys.columns				AS c	ON t.[object_id]	=	c.[object_id]
INNER JOIN	sys.default_constraints	AS d	ON t.[object_id]	=	d.[parent_object_id]	AND c.column_id = d.parent_column_id
WHERE 1=1
AND c.name = 'OID'

SELECT 'ALTER TABLE [' + s.name + '].[' + t.name + '] DROP COLUMN [' + c.name + '];' 
FROM 
			sys.tables				AS t
INNER JOIN	sys.schemas				AS s	ON t.[schema_id]	=	s.[schema_id]
INNER JOIN	sys.columns				AS c	ON t.[object_id]	=	c.[object_id]
WHERE 1=1
AND c.name = 'OID'