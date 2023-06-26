--/****** Object:  User [waterschapshuis_catch_registration_geoserver] ******/
--CREATE USER [waterschapshuis_catch_registration_geoserver] FOR LOGIN [waterschapshuis_catch_registration_geoserver] WITH DEFAULT_SCHEMA=[dbo]
--GO
--ALTER ROLE [db_datareader] ADD MEMBER [waterschapshuis_catch_registration_geoserver]
--GO
--ALTER ROLE [db_datawriter] ADD MEMBER [waterschapshuis_catch_registration_geoserver]
--GO
--/****** Object:  User [wsh_import] ******/
--CREATE USER [wsh_import] FOR LOGIN [wsh_import] WITH DEFAULT_SCHEMA=[dbo]
--GO
--ALTER ROLE [db_datareader] ADD MEMBER [wsh_import]
--GO
--ALTER ROLE [db_datawriter] ADD MEMBER [wsh_import]
--GO
/****** Object:  Indexes for tables: Trap and Catch ******/
DROP INDEX [IX_Catch_CatchTypeId] ON [dbo].[Catch];

CREATE NONCLUSTERED INDEX [IX_Catch_CatchTypeId]
ON [dbo].[Catch] ([CatchTypeId])
INCLUDE ([TrapId],[Number]);

CREATE NONCLUSTERED INDEX [IX_Trap_Status]
ON [dbo].[Trap] ([Status])
INCLUDE ([TrapTypeId],[NumberOfTraps],[CreatedOn],[Location],[SubAreaHourSquareId]);

CREATE NONCLUSTERED INDEX [IX_Trap_ExternalId]
ON [dbo].[Trap] ([ExternalId] ASC);

CREATE SPATIAL INDEX [IX_Spatial_Trap_Location]
ON dbo.Trap ([location])
WITH (
	BOUNDING_BOX = (xmin=14055, ymin=359216, xmax=277972, ymax=609644),
	GRIDS = (LEVEL_4 = LOW, LEVEL_3 = LOW, LEVEL_2= LOW, LEVEL_1 = LOW)
);
GO
/****** Object:  Schema [report] ******/
IF NOT EXISTS (SELECT name FROM sys.schemas WHERE name = N'report')
   BEGIN
      EXEC('CREATE SCHEMA [report]');
END
GO
/****** Object:  Table [report].[CatchData] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [report].[CatchData](
	[CatchId] [uniqueidentifier] NOT NULL,
	[CatchNumber] [int] NOT NULL,
	[Date] [datetimeoffset](7) NOT NULL,
	[Period] [int] NOT NULL,
	[Week] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[PeriodValue] [int] NOT NULL,
	[CatchStatus] [tinyint] NOT NULL,
	[TrapId] [uniqueidentifier] NOT NULL,
	[NumberOfTraps] [int] NOT NULL,
	[CatchTypeId] [uniqueidentifier] NOT NULL,
	[IsByCatch] [bit] NOT NULL,
	[TrapTypeId] [uniqueidentifier] NOT NULL,
	[TrappingTypeId] [uniqueidentifier] NOT NULL,
	[SubAreaHourSquareId] [uniqueidentifier] NOT NULL,
	[SubAreaId] [uniqueidentifier] NOT NULL,
	[CatchAreaId] [uniqueidentifier] NOT NULL,
	[RayonId] [uniqueidentifier] NOT NULL,
	[OrganizationId] [uniqueidentifier] NOT NULL,
	[WaterAuthorityId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Indexes for table [report].[CatchData] ******/
CREATE NONCLUSTERED INDEX [IX_CatchData_CatchId] ON [report].[CatchData]
(
	[CatchId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_PeriodValue] ON [report].[CatchData]
(
	[PeriodValue] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_PeriodValueIsByCatch] ON [report].[CatchData]
(
	[IsByCatch] ASC,
	[PeriodValue] ASC
) INCLUDE ([CatchNumber],[CatchAreaId],[OrganizationId],[RayonId],[SubAreaId],[SubAreaHourSquareId],[WaterAuthorityId])

CREATE NONCLUSTERED INDEX [IX_CatchData_TrapId] ON [report].[CatchData]
(
	[TrapId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_CatchTypeId] ON [report].[CatchData]
(
	[CatchTypeId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_TrapTypeId] ON [report].[CatchData]
(
	[TrapTypeId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_TrappingTypeId] ON [report].[CatchData]
(
	[TrappingTypeId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_CatchAreaId] ON [report].[CatchData]
(
	[CatchAreaId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_SubAreaHourSquareId] ON [report].[CatchData]
(
	[SubAreaHourSquareId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_SubAreaId] ON [report].[CatchData]
(
	[SubAreaId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_RayonId] ON [report].[CatchData]
(
	[RayonId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_WaterAuthorityId] ON [report].[CatchData]
(
	[WaterAuthorityId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_OrganizationId] ON [report].[CatchData]
(
	[OrganizationId] ASC
)
GO
/****** Object:  Table [report].[TimeRegistrationData] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [report].[TimeRegistrationData](
	[TimeRegistrationId] [uniqueidentifier] NOT NULL,
	[Date] [datetimeoffset](7) NULL,
	[Hours] [float] NOT NULL,
	[Period] [int] NOT NULL,
	[Week] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[PeriodValue] [int] NOT NULL,
	[TrappingTypeId] [uniqueidentifier] NOT NULL,
	[SubAreaHourSquareId] [uniqueidentifier] NOT NULL,
	[SubAreaId] [uniqueidentifier] NOT NULL,
	[CatchAreaId] [uniqueidentifier] NOT NULL,
	[RayonId] [uniqueidentifier] NOT NULL,
	[OrganizationId] [uniqueidentifier] NOT NULL,
	[WaterAuthorityId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Indexes for table [report].[TimeRegistrationData] ******/
CREATE NONCLUSTERED INDEX [IX_TimeRegistrationData_TimeRegistrationId] ON [report].[TimeRegistrationData]
(
	[TimeRegistrationId] ASC
)

CREATE NONCLUSTERED INDEX [IX_TimeRegistrationData_PeriodValue] ON [report].[TimeRegistrationData] 
(
	[PeriodValue]
)
INCLUDE ([Hours],[CatchAreaId],[OrganizationId],[RayonId],[SubAreaId],[SubAreaHourSquareId],[WaterAuthorityId])

CREATE NONCLUSTERED INDEX [IX_TimeRegistrationData_TrappingTypeId] ON [report].[TimeRegistrationData]
(
	[TrappingTypeId] ASC
)

CREATE NONCLUSTERED INDEX [IX_TimeRegistrationData_SubAreaHourSquareId] ON [report].[TimeRegistrationData]
(
	[SubAreaHourSquareId] ASC
)

CREATE NONCLUSTERED INDEX [IX_TimeRegistrationData_SubAreaId] ON [report].[TimeRegistrationData]
(
	[SubAreaId] ASC
)

CREATE NONCLUSTERED INDEX [IX_TimeRegistrationData_CatchAreaId] ON [report].[TimeRegistrationData]
(
	[CatchAreaId] ASC
)

CREATE NONCLUSTERED INDEX [IX_TimeRegistrationData_RayonId] ON [report].[TimeRegistrationData]
(
	[RayonId] ASC
)

CREATE NONCLUSTERED INDEX [IX_TimeRegistrationData_OrganizationId] ON [report].[TimeRegistrationData]
(
	[OrganizationId] ASC
)

CREATE NONCLUSTERED INDEX [IX_TimeRegistrationData_WaterAuthorityId] ON [report].[TimeRegistrationData]
(
	[WaterAuthorityId] ASC
)
GO
/****** Object:  Table [dbo].[gt_pk_metadata] ******/
CREATE TABLE gt_pk_metadata (
    table_schema [varchar](32) NOT NULL,
    table_name [varchar](32) NOT NULL,
    pk_column [varchar](32) NOT NULL,
    pk_column_idx int,
    pk_policy [varchar](32),
    pk_sequence [varchar](64),
    CONSTRAINT chk_pk_policy CHECK (pk_policy in ('sequence', 'assigned', 'autogenerated')));

    CREATE UNIQUE INDEX gt_pk_metadata_table_idx01 ON gt_pk_metadata (table_schema, table_name, pk_column);
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetAbsolutePeriodOfYear] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER   FUNCTION [dbo].[fn_GetAbsolutePeriodOfYear] 
(
	@date DATE
)
RETURNS INT
AS
BEGIN
	DECLARE @result int;

	SET @result = CONVERT(int, CEILING(CONVERT(decimal, DATEPART(wk, @date)) / 4.00));

	RETURN @result;
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetAbsoluteWeekOfYear] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER   FUNCTION [dbo].[fn_GetAbsoluteWeekOfYear] 
(
	@date DATE
)
RETURNS INT
AS
BEGIN
	DECLARE @result int;

	SET @result = DATEPART(wk, @date);

	RETURN @result;
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetIso86001DayOfWeek] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Function to calculate the Day Of Week when the week doesn't start on Sunday
                -- (Needed because SET DATEFIRST can't be used in a function)

CREATE OR ALTER   FUNCTION [dbo].[fn_GetIso86001DayOfWeek]
( 
	@date DATE
)
RETURNS INT 
AS 
BEGIN
	DECLARE @dateFirst TINYINT = @@DATEFIRST;

	RETURN (DATEPART(dw, @date) + @dateFirst - 2) % 7 + 1;
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetIso8601PeriodOfYear] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER   FUNCTION [dbo].[fn_GetIso8601PeriodOfYear] 
                (
	                @date DATE
                )
                RETURNS INT
                AS
                BEGIN
	                DECLARE @result int;

	                SET @result = CONVERT(int, CEILING(CONVERT(decimal, DATEPART(isowk, @date)) / 4.00));

	                RETURN @result;
                END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetIso8601WeekOfYear] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER   FUNCTION [dbo].[fn_GetIso8601WeekOfYear] 
                (
	                @date DATE
                )
                RETURNS INT
                AS
                BEGIN
	                DECLARE @result int;

	                SET @result = DATEPART(isowk, @date);

	                RETURN @result;
                END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetIso8601YearForWeekOfYear] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER   FUNCTION [dbo].[fn_GetIso8601YearForWeekOfYear] 
                (
	                @date DATE
                )
                RETURNS INT
                AS
                BEGIN
	                DECLARE @result int;

	                DECLARE @year int = DATEPART(yy, @date);
	                DECLARE @month int = DATEPART(mm, @date);
	                DECLARE @day int = DATEPART(dd, @date);
	                DECLARE @weekDay int = [dbo].[fn_GetIso86001DayOfWeek](@date);

	                SET @result = @year;

	                IF(@month = 1 AND @day <= 3 AND @weekDay > 3)
		                SET @result -= 1;

	                IF(@month = 12 AND @day >= 29 AND @weekDay <= 3)
		                SET @result += 1;

	                RETURN @result;
                END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetPeriodValue] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Create functions to be called from Geoserver Layers for Map Catches Report

CREATE OR ALTER   FUNCTION [dbo].[fn_GetPeriodValue] 
(
	@year int,
	@period int,
	@isEndPeriod bit = 0
)
RETURNS int
AS
BEGIN
	DECLARE @result int;

	SET @result = CASE WHEN @isEndPeriod = 1 AND @year = 0 THEN 50000 ELSE @year * 15 + @period END;

	RETURN @result;
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_GetTrackingsByUser] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER   FUNCTION [dbo].[fn_GetTrackingsByUser]
(	
	@userId uniqueidentifier
)
RETURNS TABLE AS
RETURN 
(
    SELECT 
        Id AS TrackingId, 
        Location,
        CreatedById
    FROM Tracking
    WHERE 
		CreatedById = @userId
        AND CAST(RecordedOn AS Date) = CAST(GETDATE() AS Date)
)
GO
/****** Object:  UserDefinedFunction [dbo].[fn_HeatMapReport] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---MapCatchesReport for Province

---MapReport Heatmap
CREATE OR ALTER   FUNCTION [dbo].[fn_HeatMapReport]
(	
	@isBeverrat int,
	@organizationId varchar(36),
	@startDate datetime,
	@endDate datetime
)

RETURNS TABLE 
AS
RETURN 
(
	SELECT 
		t.Id AS TrapId,
		t.[Location] AS TrapLocation,
		Result.NumberOfCatches AS Catches
	FROM Trap t
	INNER JOIN
	(
		SELECT
			cd.TrapId AS TrapId,
			SUM (cd.CatchNumber) AS NumberOfCatches

		FROM [report].[CatchData] cd
		WHERE 
			cd.IsByCatch = 0
			AND (@organizationId IS NULL OR @organizationId='0' OR cd.OrganizationId = @organizationId) 
			AND (@startDate is null OR @startDate < cd.Date)
			AND (@endDate is null OR cd.Date < @endDate )
			AND ((@isBeverrat is null OR @isBeverrat = 0)
				OR (@isBeverrat = 1 AND cd.CatchTypeId in ('85803328-15E7-92EF-528F-00E91B6D4815',
														'2539B02A-9298-7B9F-4273-3E8AC99D7C63',
														'7A8199E8-21DF-7556-1F0A-549E94645B6F',
														'49B51935-918B-5A38-2493-A4141FEF8C52',
														'8957CB9D-936C-29CB-8511-A9C9A7EC6A7E')) 
				OR (@isBeverrat = 2 AND cd.CatchTypeId in ('645F7089-7F21-50C5-30C4-5FE30CC693F1',
														'3D1358F4-61D4-21D8-9438-90096EEEA47E',
														'44711E96-25B8-0AF6-669B-CCDC8ABA9017',
														'E72CCB01-65BB-A1AA-A5E8-EB909FE77374',
														'C8783519-41C6-5654-1977-F6956ABA2EF4'))
			)
		GROUP BY cd.TrapId
	) Result ON Result.TrapId = t.Id  
)
GO
/****** Object:  UserDefinedFunction [dbo].[fn_MapCatchesReport_CatchArea] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER   FUNCTION [dbo].[fn_MapCatchesReport_CatchArea]
(	
	@measurement int,
	@trappingType varchar(36),
	@yearFrom int,
	@periodFrom int,
	@yearTo int,
	@periodTo int
)

RETURNS TABLE 
AS
RETURN 
(
	SELECT
    ca.Id as RegionId,
    ca.Geometry as Geometry,
    CASE WHEN @measurement in (0,1) THEN COALESCE(Result.CatchNumber, 0) ELSE null END AS CatchNumber,
    CASE WHEN @measurement in (2,3) THEN ROUND(COALESCE(CAST(Result.CatchNumber AS float)/CAST(TotalKm.Km AS float),0),2) ELSE null END AS CatchesPerKM,
    CASE WHEN @measurement = 4 THEN ROUND(COALESCE(CAST(Hours.Hours AS float)/CAST(TotalKm.Km AS float),0), 2) ELSE null END AS HoursPerKM
FROM
    dbo.CatchArea ca
    LEFT JOIN
    (SELECT SUM(sahs.KmWaterWay) as Km, sa.CatchAreaId
    FROM dbo.SubAreaHourSquare sahs
        JOIN dbo.SubArea sa on sa.id = sahs.SubAreaId
        JOIN dbo.CatchArea ca on ca.Id = sa.CatchAreaId
    GROUP BY CatchAreaId)
        as TotalKm
    ON TotalKm.CatchAreaId = ca.Id
    LEFT JOIN
    (
        SELECT
        cd.CatchAreaId as CatchAreaId,
        SUM(cd.CatchNumber) AS CatchNumber
    FROM
        [report].[CatchData] cd
        JOIN CatchArea ca on ca.Id = cd.CatchAreaId
    WHERE 
        @measurement != 4
        AND cd.IsByCatch = (CASE WHEN @measurement in (1,3) THEN 1 Else 0 END)
        AND (@trappingType IS NULL OR @trappingType = '0' OR cd.TrappingTypeId = @trappingType)
        AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= cd.PeriodValue
        AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= cd.PeriodValue
    GROUP BY CatchAreaId)							
        AS Result
    ON Result.CatchAreaId = ca.Id
    LEFT JOIN
    (SELECT
        td.CatchAreaId as CatchAreaId,
        SUM(td.Hours) AS Hours
    FROM
        [report].[TimeRegistrationData] td
        JOIN CatchArea ca on ca.Id = td.CatchAreaId
    WHERE @measurement = 4
        AND (@trappingType IS NULL OR @trappingType = '0' OR td.TrappingTypeId = @trappingType)
        AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= td.PeriodValue
        AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= td.PeriodValue
    GROUP BY CatchAreaId)
        AS Hours
    ON Hours.CatchAreaId = ca.Id 
    WHERE TotalKm.Km > (CASE WHEN @measurement in (2,3,4) THEN 0 Else -1 END)
)
GO
/****** Object:  UserDefinedFunction [dbo].[fn_MapCatchesReport_Organization] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

---MapCatchesReport for Organization
CREATE OR ALTER   FUNCTION [dbo].[fn_MapCatchesReport_Organization]
(	
	@measurement int,
	@trappingType varchar(36),
	@yearFrom int,
	@periodFrom int,
	@yearTo int,
	@periodTo int
)

RETURNS TABLE 
AS
RETURN 
(
	SELECT
    o.Id as RegionId,
    o.Geometry as Geometry,
    CASE WHEN @measurement in (0,1) THEN COALESCE(Result.CatchNumber, 0) ELSE null END AS CatchNumber,
    CASE WHEN @measurement in (2,3) THEN ROUND(COALESCE(CAST(Result.CatchNumber AS float)/CAST(TotalKm.Km AS float),0),2) ELSE null END AS CatchesPerKM,
    CASE WHEN @measurement = 4 THEN ROUND(COALESCE(CAST(Hours.Hours AS float)/CAST(TotalKm.Km AS float),0), 2) ELSE null END AS HoursPerKM
FROM
	dbo.Organization o
    LEFT JOIN
    (SELECT SUM(sahs.KmWaterWay) as Km, wa.OrganizationId
    FROM dbo.SubAreaHourSquare sahs
        JOIN dbo.SubArea sa on sa.id = sahs.SubAreaId
        JOIN dbo.WaterAuthority wa on wa.Id = sa.WaterAuthorityId
    GROUP BY OrganizationId)
        as TotalKm
    ON TotalKm.OrganizationId = o.Id
    LEFT JOIN
    (
        SELECT
        cd.OrganizationId as OrganizationId,
        SUM(cd.CatchNumber) AS CatchNumber
    FROM
        [report].[CatchData] cd
        JOIN dbo.Organization o on o.Id = cd.OrganizationId
    WHERE 
        @measurement != 4
        AND cd.IsByCatch = (CASE WHEN @measurement in (1,3) THEN 1 Else 0 END)
        AND (@trappingType IS NULL OR @trappingType = '0' OR cd.TrappingTypeId = @trappingType)
        AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= cd.PeriodValue
        AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= cd.PeriodValue
    GROUP BY OrganizationId)							
        AS Result
    ON Result.OrganizationId = o.Id
    LEFT JOIN
    (SELECT
        td.OrganizationId as OrganizationId,
        SUM(td.Hours) AS Hours
    FROM
        [report].[TimeRegistrationData] td
        JOIN dbo.Organization o on o.Id = td.OrganizationId
    WHERE @measurement = 4
        AND (@trappingType IS NULL OR @trappingType = '0' OR td.TrappingTypeId = @trappingType)
        AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= td.PeriodValue
        AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= td.PeriodValue
    GROUP BY OrganizationId)
        AS Hours
    ON Hours.OrganizationId = o.Id  
    WHERE TotalKm.Km > (CASE WHEN @measurement in (2,3,4) THEN 0 Else -1 END)
)
GO
/****** Object:  UserDefinedFunction [dbo].[fn_MapCatchesReport_Rayon] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---MapCatchesReport for Rayon
CREATE OR ALTER   FUNCTION [dbo].[fn_MapCatchesReport_Rayon]
(	
	@measurement int,
	@trappingType varchar(36),
	@yearFrom int,
	@periodFrom int,
	@yearTo int,
	@periodTo int
)

RETURNS TABLE 
AS
RETURN 
(
	SELECT
    r.Id as RegionId,
    r.Geometry as Geometry,
    CASE WHEN @measurement in (0,1) THEN COALESCE(Result.CatchNumber, 0) ELSE null END AS CatchNumber,
    CASE WHEN @measurement in (2,3) THEN ROUND(COALESCE(CAST(Result.CatchNumber AS float)/CAST(TotalKm.Km AS float),0),2) ELSE null END AS CatchesPerKM,
    CASE WHEN @measurement = 4 THEN ROUND(COALESCE(CAST(Hours.Hours AS float)/CAST(TotalKm.Km AS float),0), 2) ELSE null END AS HoursPerKM
FROM
	dbo.Rayon r
    LEFT JOIN
    (SELECT SUM(sahs.KmWaterWay) as Km, ca.RayonId
    FROM dbo.SubAreaHourSquare sahs
        JOIN dbo.SubArea sa on sa.id = sahs.SubAreaId
		JOIN dbo.CatchArea ca on ca.Id = sa.CatchAreaId
    GROUP BY RayonId)
        as TotalKm
    ON TotalKm.RayonId = r.Id
    LEFT JOIN
    (
        SELECT
        cd.RayonId as RayonId,
        SUM(cd.CatchNumber) AS CatchNumber
    FROM
        [report].[CatchData] cd
        JOIN dbo.Rayon r on r.Id = cd.RayonId
    WHERE 
        @measurement != 4
        AND cd.IsByCatch = (CASE WHEN @measurement in (1,3) THEN 1 Else 0 END)
        AND (@trappingType IS NULL OR @trappingType = '0' OR cd.TrappingTypeId = @trappingType)
        AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= cd.PeriodValue
        AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= cd.PeriodValue
    GROUP BY RayonId)							
        AS Result
    ON Result.RayonId = r.Id
    LEFT JOIN
    (SELECT
        td.RayonId as RayonId,
        SUM(td.Hours) AS Hours
    FROM
        [report].[TimeRegistrationData] td
        JOIN dbo.Rayon r on r.Id = td.RayonId
    WHERE @measurement = 4
        AND (@trappingType IS NULL OR @trappingType = '0' OR td.TrappingTypeId = @trappingType)
        AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= td.PeriodValue
        AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= td.PeriodValue
    GROUP BY RayonId)
        AS Hours
    ON Hours.RayonId = r.Id 
    WHERE TotalKm.Km > (CASE WHEN @measurement in (2,3,4) THEN 0 Else -1 END)
)
GO
/****** Object:  UserDefinedFunction [dbo].[fn_MapCatchesReport_SubArea] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---MapCatchesReport for SubArea
CREATE OR ALTER   FUNCTION [dbo].[fn_MapCatchesReport_SubArea]
(	
	@measurement int,
	@trappingType varchar(36),
	@yearFrom int,
	@periodFrom int,
	@yearTo int,
	@periodTo int
)

RETURNS TABLE 
AS
RETURN 
(
	SELECT
    sa.Id as RegionId,
    sa.Geometry as Geometry,
    CASE WHEN @measurement in (0,1) THEN COALESCE(Result.CatchNumber, 0) ELSE null END AS CatchNumber,
    CASE WHEN @measurement in (2,3) THEN ROUND(COALESCE(CAST(Result.CatchNumber AS float)/CAST(TotalKm.Km AS float),0),2) ELSE null END AS CatchesPerKM,
    CASE WHEN @measurement = 4 THEN ROUND(COALESCE(CAST(Hours.Hours AS float)/CAST(TotalKm.Km AS float),0), 2) ELSE null END AS HoursPerKM
FROM
	dbo.SubArea sa
    LEFT JOIN
    (SELECT SUM(sahs.KmWaterWay) as Km, sahs.SubAreaId
    FROM dbo.SubAreaHourSquare sahs
    GROUP BY SubAreaId)
        as TotalKm
    ON TotalKm.SubAreaId = sa.Id

    LEFT JOIN
    (
        SELECT
        cd.SubAreaId as SubAreaId,
        SUM(cd.CatchNumber) AS CatchNumber
    FROM
        [report].[CatchData] cd
        JOIN dbo.SubArea sa on sa.Id = cd.SubAreaId
    WHERE 
        @measurement != 4
        AND cd.IsByCatch = (CASE WHEN @measurement in (1,3) THEN 1 Else 0 END)
        AND (@trappingType IS NULL OR @trappingType = '0' OR cd.TrappingTypeId = @trappingType)
        AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= cd.PeriodValue
        AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= cd.PeriodValue
    GROUP BY SubAreaId)							
        AS Result
    ON Result.SubAreaId = sa.Id
    LEFT JOIN
    (SELECT
        td.SubAreaId as SubAreaId,
        SUM(td.Hours) AS Hours
    FROM
        [report].[TimeRegistrationData] td
        JOIN dbo.SubArea sa on sa.Id = td.SubAreaId
    WHERE @measurement = 4
        AND (@trappingType IS NULL OR @trappingType = '0' OR td.TrappingTypeId = @trappingType)
        AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= td.PeriodValue
        AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= td.PeriodValue
    GROUP BY SubAreaId)
        AS Hours
    ON Hours.SubAreaId = sa.Id 
    WHERE TotalKm.Km > (CASE WHEN @measurement in (2,3,4) THEN 0 Else -1 END)
)
GO
/****** Object:  UserDefinedFunction [dbo].[fn_MapCatchesReport_SubAreaHourSquare] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---MapCatchesReport for SubAreaHourSquare
CREATE OR ALTER   FUNCTION [dbo].[fn_MapCatchesReport_SubAreaHourSquare]
(	
	@measurement int,
	@trappingType varchar(36),
	@yearFrom int,
	@periodFrom int,
	@yearTo int,
	@periodTo int
)

RETURNS TABLE 
AS
RETURN 
(
	SELECT
    sahs.Id as RegionId,
    sahs.Geometry as Geometry,
    CASE WHEN @measurement in (0,1) THEN COALESCE(Result.CatchNumber, 0) ELSE null END AS CatchNumber,
    CASE WHEN @measurement in (2,3) THEN ROUND(COALESCE(CAST(Result.CatchNumber AS float)/CAST(sahs.KmWaterway AS float),0),2) ELSE null END AS CatchesPerKM,
    CASE WHEN @measurement = 4 THEN ROUND(COALESCE(CAST(Hours.Hours AS float)/CAST(sahs.KmWaterway AS float),0), 2) ELSE null END AS HoursPerKM
FROM
	dbo.SubAreaHourSquare sahs
    LEFT JOIN
    (
        SELECT
        cd.SubAreaHourSquareId as SubAreaHourSquareId,
        SUM(cd.CatchNumber) AS CatchNumber
    FROM
        [report].[CatchData] cd
        JOIN dbo.SubAreaHourSquare sahs on sahs.Id = cd.SubAreaHourSquareId
    WHERE 
        @measurement != 4
        AND cd.IsByCatch = (CASE WHEN @measurement in (1,3) THEN 1 Else 0 END)
        AND (@trappingType IS NULL OR @trappingType = '0' OR cd.TrappingTypeId = @trappingType)
        AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= cd.PeriodValue
        AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= cd.PeriodValue
		
    GROUP BY SubAreaHourSquareId)							
        AS Result
    ON Result.SubAreaHourSquareId = sahs.Id
    LEFT JOIN
    (SELECT
        td.SubAreaHourSquareId as SubAreaHourSquareId,
        SUM(td.Hours) AS Hours
    FROM
        [report].[TimeRegistrationData] td
        JOIN dbo.SubAreaHourSquare sahs on sahs.Id = td.SubAreaHourSquareId
    WHERE @measurement = 4
        AND (@trappingType IS NULL OR @trappingType = '0' OR td.TrappingTypeId = @trappingType)
        AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= td.PeriodValue
        AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= td.PeriodValue
    GROUP BY SubAreaHourSquareId)
        AS Hours
    ON Hours.SubAreaHourSquareId = sahs.Id  
	WHERE sahs.KmWaterway > (CASE WHEN @measurement in (2,3,4) THEN 0 Else -1 END)
)
GO
/****** Object:  UserDefinedFunction [dbo].[fn_MapCatchesReport_WaterAuthority] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---MapCatchesReport for WaterAuthority
CREATE OR ALTER   FUNCTION [dbo].[fn_MapCatchesReport_WaterAuthority]
(	
	@measurement int,
	@trappingType varchar(36),
	@yearFrom int,
	@periodFrom int,
	@yearTo int,
	@periodTo int
)

RETURNS TABLE 
AS
RETURN 
(
	SELECT
    wa.Id as RegionId,
    wa.Geometry as Geometry,
    CASE WHEN @measurement in (0,1) THEN COALESCE(Result.CatchNumber, 0) ELSE null END AS CatchNumber,
    CASE WHEN @measurement in (2,3) THEN ROUND(COALESCE(CAST(Result.CatchNumber AS float)/CAST(TotalKm.Km AS float),0),2) ELSE null END AS CatchesPerKM,
    CASE WHEN @measurement = 4 THEN ROUND(COALESCE(CAST(Hours.Hours AS float)/CAST(TotalKm.Km AS float),0), 2) ELSE null END AS HoursPerKM
FROM
	dbo.WaterAuthority wa
    LEFT JOIN
    (SELECT SUM(sahs.KmWaterWay) as Km, sa.WaterAuthorityId
    FROM dbo.SubAreaHourSquare sahs
        JOIN dbo.SubArea sa on sa.id = sahs.SubAreaId
    GROUP BY WaterAuthorityId)
        as TotalKm
    ON TotalKm.WaterAuthorityId = wa.Id
    LEFT JOIN
    (
        SELECT
        cd.WaterAuthorityId as WaterAuthorityId,
        SUM(cd.CatchNumber) AS CatchNumber
    FROM
        [report].[CatchData] cd
        JOIN dbo.WaterAuthority wa on wa.Id = cd.WaterAuthorityId
    WHERE 
        @measurement != 4
        AND cd.IsByCatch = (CASE WHEN @measurement in (1,3) THEN 1 Else 0 END)
        AND (@trappingType IS NULL OR @trappingType = '0' OR cd.TrappingTypeId = @trappingType)
        AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= cd.PeriodValue
        AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= cd.PeriodValue
    GROUP BY WaterAuthorityId)							
        AS Result
    ON Result.WaterAuthorityId = wa.Id
    LEFT JOIN
    (SELECT
        td.WaterAuthorityId as WaterAuthorityId,
        SUM(td.Hours) AS Hours
    FROM
        [report].[TimeRegistrationData] td
        JOIN dbo.WaterAuthority wa on wa.Id = td.WaterAuthorityId
    WHERE @measurement = 4
        AND (@trappingType IS NULL OR @trappingType = '0' OR td.TrappingTypeId = @trappingType)
        AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= td.PeriodValue
        AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= td.PeriodValue
    GROUP BY WaterAuthorityId)
        AS Hours
    ON Hours.WaterAuthorityId = wa.Id 
    WHERE TotalKm.Km > (CASE WHEN @measurement in (2,3,4) THEN 0 Else -1 END)
)
GO
/****** Object:  View [dbo].[vw_HistoricalTraps_Geo] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER   VIEW [dbo].[vw_HistoricalTraps_Geo] AS
SELECT 
	t.Id AS TrapId,
	t.[Location],
	t.SubAreaHourSquareId,
	t.NumberOfTraps as NumberOfTraps,
	t.TrapTypeId AS TrapTypeId,
	tpc.NumberOfCatches,
	tpc.NumberOfByCatches,
	YEAR(t.RecordedOn) AS TrapCreatedYear,
	t.[Status],
	YEAR(t.UpdatedOn) AS TrapUpdatedYear
FROM Trap t
INNER JOIN
(
	SELECT
		t.Id AS TrapId,
		SUM(CASE WHEN ct.IsByCatch = 0 THEN c.Number END) AS NumberOfCatches,
		SUM(CASE WHEN ct.IsByCatch = 1 THEN c.Number END) AS NumberOfByCatches
	FROM dbo.Trap t
	LEFT OUTER JOIN Catch c on c.TrapId = t.Id
	LEFT OUTER JOIN CatchType ct on c.CatchTypeId = ct.Id
	GROUP BY t.Id
) tpc ON tpc.TrapId = t.Id
GO
/****** Object:  View [dbo].[vw_Observations_Geo] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER   VIEW [dbo].[vw_Observations_Geo] AS
SELECT 
	[Id],
	[Type],
	[Location],
	[Archived],
	CASE
		WHEN [Archived] = 1 THEN 'AOL'
		ELSE 'OL'
	END AS StyleCode
FROM [dbo].[Observation]
GO
/****** Object:  View [dbo].[vw_PublicTrackings_Geo] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER   VIEW [dbo].[vw_PublicTrackings_Geo] AS
SELECT 
	Id,
    [Location],
	CreatedById
FROM Tracking
WHERE 
	isTrackingPrivate = 0 AND 
	CAST(RecordedOn AS Date) = CAST(GETDATE() AS Date)
GO
/****** Object:  View [dbo].[vw_SubAreaHourSquare_Catch_Geo] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER   VIEW [dbo].[vw_SubAreaHourSquare_Catch_Geo] AS
SELECT 
	sahs.Id AS SubAreaHourSquareId,
	sahs.Geometry AS Geometry,
	CASE
		WHEN CatchesBySubAreaHourSquare.CatchNumber is null THEN 0
		ELSE CatchesBySubAreaHourSquare.CatchNumber 
	END AS CatchNumber,
	CASE
		WHEN CatchesBySubAreaHourSquare.CatchYear is null THEN 0
		ELSE CatchesBySubAreaHourSquare.CatchYear 
	END AS CatchYear,
	CASE
		WHEN (max(CatchNumber) OVER(PARTITION BY CatchYear)) is null then 0
		ELSE max(CatchNumber) OVER(PARTITION BY CatchYear)
	END AS MaxCatchesPerYear
FROM dbo.SubAreaHourSquare sahs
LEFT JOIN (
	SELECT      
		t.SubAreaHourSquareId AS SubAreaHourSquareId,
		sum(c.Number) AS CatchNumber,
		Year(c.RecordedOn) AS CatchYear
	FROM dbo.Trap t 
		JOIN dbo.Catch c ON t.Id = c.TrapId	
	GROUP BY 
		SubAreaHourSquareId,
		Year(c.RecordedOn)
	) AS CatchesBySubAreaHourSquare ON CatchesBySubAreaHourSquare.SubAreaHourSquareId = sahs.Id
GO
/****** Object:  View [dbo].[vw_TrackingLines_Geo] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER   VIEW [dbo].[vw_TrackingLines_Geo] AS
                                SELECT *,
                                YEAR(TL.[Date]) AS 'CreatedOnYear', 
                                DATEDIFF(DAY, TL.[Date], SYSDATETIMEOFFSET()) AS 'DaysOffset',
                                CAST(TL.[Date] AS datetime) AS 'TrackingDate'
                                FROM [TrackingLine] AS TL
GO
/****** Object:  StoredProcedure [report].[PopulateCatchData] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Drop and re-inserts the catch data table
-- =============================================
CREATE OR ALTER   PROCEDURE [report].[PopulateCatchData]
AS
DECLARE @ResultValue int
BEGIN

    TRUNCATE TABLE [report].[CatchData]

    INSERT INTO [report].[CatchData]
        ([CatchId], [CatchNumber], [Date], [Period], [Week], [Year],
        [PeriodValue], [CatchStatus],
        [TrapId] ,[NumberOfTraps],
        [CatchTypeId], [IsByCatch],
        [TrapTypeId],
        [TrappingTypeId],
        [SubAreaHourSquareId],
        [SubAreaId],
        [CatchAreaId],
        [RayonId],
        [OrganizationId],
        [WaterAuthorityId]
        )
    SELECT
        c.Id as CatchId, c.Number as CatchNumber, c.RecordedOn, c.Period, c.[Week], c.[Year],
        dbo.fn_GetPeriodValue(c.[Year], c.Period, 0) as PeriodValue, c.Status as CatchStatus,
        t.Id as TrapId, t.NumberOfTraps,
        ct.Id as CatchTypeId, ct.IsByCatch,
        tt.Id as TrapTypeId,
        trpt.Id as TrappingTypeId,
        sahs.Id as SubAreaHourSquareId,
        sa.Id as SubAreaId,
        ca.Id as CatchAreaId,
        r.Id as RayonId,
        r.OrganizationId,
        wa.Id as WaterAuthorityId
    FROM
        dbo.Catch c
        JOIN dbo.Trap t on t.Id = c.TrapId
        JOIN dbo.TrapType tt on tt.id = t.TrapTypeId
        JOIN dbo.TrappingType trpt on trpt.Id = tt.TrappingTypeId
        JOIN dbo.CatchType ct on c.CatchTypeId = ct.Id
        JOIN dbo.SubAreaHourSquare sahs on sahs.Id = t.SubAreaHourSquareId
        JOIN dbo.SubArea sa on sa.Id = sahs.SubAreaId
        JOIN dbo.CatchArea ca on ca.Id = sa.CatchAreaId
        JOIN dbo.Rayon r on r.Id = ca.RayonId
        JOIN dbo.WaterAuthority wa on wa.Id = sa.WaterAuthorityId

    SELECT @ResultValue=COUNT(*)
    FROM [report].[CatchData]

    RETURN @ResultValue
END
GO
/****** Object:  StoredProcedure [report].[PopulateTimeRegistrationData] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Drop and re-inserts the time registration data table
-- =============================================
CREATE OR ALTER   PROCEDURE [report].[PopulateTimeRegistrationData]
AS
DECLARE @ResultValue int
BEGIN
    TRUNCATE TABLE [report].[TimeRegistrationData]

    INSERT INTO [report].[TimeRegistrationData]
        ([TimeRegistrationId], [Date], [Hours], [Period], [Week], [Year], [PeriodValue],
        [TrappingTypeId],
        [SubAreaHourSquareId],
        [SubAreaId],
        [CatchAreaId],
        [RayonId],
        [OrganizationId],
        [WaterAuthorityId]
        )
    SELECT
        tr.Id as TimeRegistrationId, tr.Date, tr.[Hours], tr.Period, tr.[Week], tr.[Year],
        dbo.fn_GetPeriodValue(tr.[Year], tr.Period, 0) as PeriodValue,
        trpt.Id as TrappingTypeId,
        sahs.Id as SubAreaHourSquareId,
        sa.Id as SubAreaId,
        ca.Id as CatchAreaId,
        r.Id as RayonId,
        r.OrganizationId,
        wa.Id as WaterAuthorityId
    FROM
        dbo.TimeRegistration tr
        JOIN dbo.TrappingType trpt on trpt.Id = tr.TrappingTypeId
        JOIN dbo.SubAreaHourSquare sahs on sahs.Id = tr.SubAreaHourSquareId
        JOIN dbo.SubArea sa on sa.Id = sahs.SubAreaId
        JOIN dbo.CatchArea ca on ca.Id = sa.CatchAreaId
        JOIN dbo.Rayon r on r.Id = ca.RayonId
        JOIN dbo.WaterAuthority wa on wa.Id = sa.WaterAuthorityId

    SELECT @ResultValue=COUNT(*)
    FROM [report].[TimeRegistrationData]

    RETURN @ResultValue
END
GO