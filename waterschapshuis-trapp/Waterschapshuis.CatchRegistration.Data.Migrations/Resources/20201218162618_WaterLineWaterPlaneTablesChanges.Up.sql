
IF NOT EXISTS
     (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'PK_water_lines_lokaalid'))
ALTER TABLE [dbo].[water_lines] ALTER COLUMN lokaalid bigint NOT NULL;
GO

IF NOT EXISTS
     (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'PK_water_lines_lokaalid'))
ALTER TABLE [dbo].[water_lines]
   ADD CONSTRAINT PK_water_lines_lokaalid PRIMARY KEY CLUSTERED (lokaalid);
GO

IF NOT EXISTS
     (SELECT * FROM sys.indexes WHERE name = 'IX_Spatial_water_lines_Location')
CREATE SPATIAL INDEX [IX_Spatial_water_lines_Location] ON [dbo].[water_lines]
(
	[ogr_geometry]
)USING  GEOMETRY_GRID 
WITH (BOUNDING_BOX =(10425, 306846, 280001, 614394), GRIDS =(LEVEL_1 = HIGH,LEVEL_2 = HIGH,LEVEL_3 = HIGH,LEVEL_4 = HIGH), 
CELLS_PER_OBJECT = 16, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF NOT EXISTS
     (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'PK_water_planes_lokaalid'))
ALTER TABLE [dbo].[water_planes] ALTER COLUMN lokaalid bigint NOT NULL;
GO

IF NOT EXISTS
     (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'PK_water_planes_lokaalid'))
ALTER TABLE [dbo].[water_planes]
   ADD CONSTRAINT PK_water_planes_lokaalid PRIMARY KEY CLUSTERED (lokaalid);
GO

IF NOT EXISTS
     (SELECT * FROM sys.indexes WHERE name = 'IX_Spatial_water_planes_Location')
CREATE SPATIAL INDEX [IX_Spatial_water_planes_Location] ON [dbo].[water_planes]
(
	[ogr_geometry]
)USING  GEOMETRY_GRID 
WITH (BOUNDING_BOX =(10425, 306846, 280001, 614394), GRIDS =(LEVEL_1 = HIGH,LEVEL_2 = HIGH,LEVEL_3 = HIGH,LEVEL_4 = HIGH), 
CELLS_PER_OBJECT = 16, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

-----procedure for geting water lines for km waterway calculation
CREATE OR ALTER PROCEDURE [dbo].[GetWaterLinesForSubareaHourSquare]
	@sahsId uniqueidentifier

AS 

	declare @geom geometry
	select @geom = geometry from SubAreaHourSquare where Id =  @sahsId


	SELECT wl.lokaalid , wl.ogr_geometry , wl.typewater
	FROM water_lines wl
	WHERE 
	@geom.STIntersects(wl.ogr_geometry) = 1 

GO
