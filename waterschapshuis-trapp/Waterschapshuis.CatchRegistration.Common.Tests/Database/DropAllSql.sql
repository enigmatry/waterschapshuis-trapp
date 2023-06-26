/* Azure friendly */
/* Drop all Foreign Key constraints */
DECLARE @name VARCHAR(128)
DECLARE @schema VARCHAR(128)
DECLARE @constraint VARCHAR(254)
DECLARE @SQL VARCHAR(254)

SELECT @name = (SELECT TOP 1 TABLE_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'FOREIGN KEY' ORDER BY TABLE_NAME)
SELECT @schema = (SELECT TOP 1 CONSTRAINT_SCHEMA FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'FOREIGN KEY' ORDER BY TABLE_NAME)

WHILE @name is not null
BEGIN
    SELECT @constraint = (SELECT TOP 1 CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'FOREIGN KEY' AND TABLE_NAME = @name ORDER BY CONSTRAINT_NAME)
    WHILE @constraint IS NOT NULL
    BEGIN
        SELECT @SQL = 'ALTER TABLE [' + RTRIM(@schema) +'].[' + RTRIM(@name) +'] DROP CONSTRAINT [' + RTRIM(@constraint) +']'
        EXEC (@SQL)
        PRINT 'Dropped FK Constraint: ' + @constraint + ' on ' + @name
        SELECT @constraint = (SELECT TOP 1 CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'FOREIGN KEY' AND CONSTRAINT_NAME <> @constraint AND TABLE_NAME = @name ORDER BY CONSTRAINT_NAME)
    END
SELECT @name = (SELECT TOP 1 TABLE_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'FOREIGN KEY' ORDER BY TABLE_NAME)
SELECT @schema = (SELECT TOP 1 CONSTRAINT_SCHEMA FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'FOREIGN KEY' ORDER BY TABLE_NAME)

END
GO

/* Drop all Spatial indexes */
DECLARE @index VARCHAR(254)
DECLARE @table VARCHAR(128)
DECLARE @SQL VARCHAR(254)

SELECT @index = (SELECT TOP 1 si.name FROM SYS.KEY_CONSTRAINTS kc INNER JOIN SYS.INDEXES si INNER JOIN SYS.OBJECTS o on si.object_id=o.object_id on kc.parent_object_id=si.object_id where si.type_desc = 'SPATIAL')

WHILE @index IS NOT NULL
BEGIN
    SELECT @index = (SELECT TOP 1 si.name FROM SYS.KEY_CONSTRAINTS kc INNER JOIN SYS.INDEXES si INNER JOIN SYS.OBJECTS o on si.object_id=o.object_id on kc.parent_object_id=si.object_id where si.type_desc = 'SPATIAL')
	SELECT @table = (SELECT TOP 1 o.name FROM SYS.KEY_CONSTRAINTS kc INNER JOIN SYS.INDEXES si INNER JOIN SYS.OBJECTS o on si.object_id=o.object_id	on kc.parent_object_id=si.object_id where si.type_desc = 'SPATIAL')
    WHILE @index is not null
    BEGIN
		SELECT @SQL = 'DROP INDEX ['+ RTRIM(@index)+']  ON [dbo].['+@table+']'
        EXEC (@SQL)
        PRINT 'Dropped index: ' + @index + ' on ' + @table
        SELECT @index = (SELECT TOP 1 si.name FROM SYS.KEY_CONSTRAINTS kc INNER JOIN SYS.INDEXES si INNER JOIN SYS.OBJECTS o on si.object_id=o.object_id on kc.parent_object_id=si.object_id where si.type_desc = 'SPATIAL')
		SELECT @table = (SELECT TOP 1 o.name FROM SYS.KEY_CONSTRAINTS kc INNER JOIN SYS.INDEXES si INNER JOIN SYS.OBJECTS o on si.object_id=o.object_id	on kc.parent_object_id=si.object_id where si.type_desc = 'SPATIAL')
    END
END
GO

/* Drop all Primary Key constraints */
DECLARE @name VARCHAR(128)
DECLARE @schema VARCHAR(128)
DECLARE @constraint VARCHAR(254)
DECLARE @SQL VARCHAR(254)

SELECT @name = (SELECT TOP 1 TABLE_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'PRIMARY KEY' ORDER BY TABLE_NAME)
SELECT @schema = (SELECT TOP 1 CONSTRAINT_SCHEMA FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'PRIMARY KEY' ORDER BY TABLE_NAME)

WHILE @name IS NOT NULL
BEGIN
    SELECT @constraint = (SELECT TOP 1 CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'PRIMARY KEY' AND TABLE_NAME = @name ORDER BY CONSTRAINT_NAME)
    WHILE @constraint is not null
    BEGIN
        SELECT @SQL = 'ALTER TABLE [' + RTRIM(@schema) +'].[' + RTRIM(@name) +'] DROP CONSTRAINT [' + RTRIM(@constraint)+']'
        EXEC (@SQL)
        PRINT 'Dropped PK Constraint: ' + @constraint + ' on ' + @name
        SELECT @constraint = (SELECT TOP 1 CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'PRIMARY KEY' AND CONSTRAINT_NAME <> @constraint AND TABLE_NAME = @name ORDER BY CONSTRAINT_NAME)
    END
SELECT @name = (SELECT TOP 1 TABLE_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'PRIMARY KEY' ORDER BY TABLE_NAME)
SELECT @schema = (SELECT TOP 1 CONSTRAINT_SCHEMA FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'PRIMARY KEY' ORDER BY TABLE_NAME)

END
GO

/* Drop all tables */
DECLARE @name VARCHAR(128)
DECLARE @schema VARCHAR(128)
DECLARE @SQL VARCHAR(254)

SELECT @name = (select top(1) name from sys.objects WHERE [type] = 'U' ORDER BY [name])
SELECT @schema = (select top(1) schema_Name(schema_id) from sys.objects WHERE [type] = 'U' ORDER BY [name])

WHILE @name IS NOT NULL
BEGIN
    SELECT @SQL = 'DROP TABLE [' + RTRIM(@schema) +'].[' + RTRIM(@name) +']'
    EXEC (@SQL)
    PRINT 'Dropped Table: ' + @schema + '.' + @name
    SELECT @name = (select top(1) name from sys.objects WHERE [type] = 'U' ORDER BY [name])
    SELECT @schema = (select top(1) schema_Name(schema_id) from sys.objects WHERE [type] = 'U' ORDER BY [name])
END
GO