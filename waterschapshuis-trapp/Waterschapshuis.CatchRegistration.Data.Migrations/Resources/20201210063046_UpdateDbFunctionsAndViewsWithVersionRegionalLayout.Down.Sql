CREATE OR ALTER FUNCTION [dbo].[fn_MapCatchesReport_CatchArea]
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
    FROM dbo.CatchArea ca
        LEFT JOIN (
            SELECT SUM(sahs.KmWaterWay) as Km, sa.CatchAreaId
            FROM dbo.SubAreaHourSquare sahs
                JOIN dbo.SubArea sa on sa.id = sahs.SubAreaId
                JOIN dbo.CatchArea ca on ca.Id = sa.CatchAreaId
            GROUP BY CatchAreaId
        ) as TotalKm ON TotalKm.CatchAreaId = ca.Id
        LEFT JOIN (
            SELECT cd.CatchAreaId as CatchAreaId, SUM(cd.CatchNumber) AS CatchNumber
            FROM [report].[CatchData] cd
                JOIN CatchArea ca on ca.Id = cd.CatchAreaId
            WHERE 
                @measurement != 4
                AND cd.IsByCatch = (CASE WHEN @measurement in (1,3) THEN 1 Else 0 END)
                AND (@trappingType IS NULL OR @trappingType = '0' OR cd.TrappingTypeId = @trappingType)
                AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= cd.PeriodValue
                AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= cd.PeriodValue
            GROUP BY CatchAreaId
        ) AS Result ON Result.CatchAreaId = ca.Id
        LEFT JOIN (
            SELECT td.CatchAreaId as CatchAreaId, SUM(td.Hours) AS Hours
            FROM [report].[TimeRegistrationData] td
                JOIN CatchArea ca on ca.Id = td.CatchAreaId
            WHERE @measurement = 4
                AND (@trappingType IS NULL OR @trappingType = '0' OR td.TrappingTypeId = @trappingType)
                AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= td.PeriodValue
                AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= td.PeriodValue
            GROUP BY CatchAreaId
        ) AS Hours ON Hours.CatchAreaId = ca.Id 
    WHERE TotalKm.Km > (CASE WHEN @measurement in (2,3,4) THEN 0 Else -1 END)
)
GO


CREATE OR ALTER FUNCTION [dbo].[fn_MapCatchesReport_Rayon]
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
    FROM dbo.Rayon r
        LEFT JOIN (
            SELECT SUM(sahs.KmWaterWay) as Km, ca.RayonId
            FROM dbo.SubAreaHourSquare sahs
                JOIN dbo.SubArea sa on sa.id = sahs.SubAreaId
		        JOIN dbo.CatchArea ca on ca.Id = sa.CatchAreaId
            GROUP BY RayonId
        ) as TotalKm ON TotalKm.RayonId = r.Id
        LEFT JOIN (
            SELECT cd.RayonId as RayonId, SUM(cd.CatchNumber) AS CatchNumber
            FROM [report].[CatchData] cd
                JOIN dbo.Rayon r on r.Id = cd.RayonId
            WHERE 
                @measurement != 4
                AND cd.IsByCatch = (CASE WHEN @measurement in (1,3) THEN 1 Else 0 END)
                AND (@trappingType IS NULL OR @trappingType = '0' OR cd.TrappingTypeId = @trappingType)
                AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= cd.PeriodValue
                AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= cd.PeriodValue
            GROUP BY RayonId
        ) AS Result ON Result.RayonId = r.Id
        LEFT JOIN (
            SELECT td.RayonId as RayonId, SUM(td.Hours) AS Hours
            FROM [report].[TimeRegistrationData] td
                JOIN dbo.Rayon r on r.Id = td.RayonId
            WHERE @measurement = 4
                AND (@trappingType IS NULL OR @trappingType = '0' OR td.TrappingTypeId = @trappingType)
                AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= td.PeriodValue
                AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= td.PeriodValue
            GROUP BY RayonId
        ) AS Hours ON Hours.RayonId = r.Id 
    WHERE TotalKm.Km > (CASE WHEN @measurement in (2,3,4) THEN 0 Else -1 END)
)
GO


CREATE OR ALTER FUNCTION [dbo].[fn_MapCatchesReport_SubArea]
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
    FROM dbo.SubArea sa
        LEFT JOIN (
            SELECT SUM(sahs.KmWaterWay) as Km, sahs.SubAreaId
            FROM dbo.SubAreaHourSquare sahs
            GROUP BY SubAreaId
        ) as TotalKm ON TotalKm.SubAreaId = sa.Id
        LEFT JOIN (
            SELECT cd.SubAreaId as SubAreaId, SUM(cd.CatchNumber) AS CatchNumber
            FROM [report].[CatchData] cd
                JOIN dbo.SubArea sa on sa.Id = cd.SubAreaId
            WHERE 
                @measurement != 4
                AND cd.IsByCatch = (CASE WHEN @measurement in (1,3) THEN 1 Else 0 END)
                AND (@trappingType IS NULL OR @trappingType = '0' OR cd.TrappingTypeId = @trappingType)
                AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= cd.PeriodValue
                AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= cd.PeriodValue
            GROUP BY SubAreaId
        ) AS Result ON Result.SubAreaId = sa.Id
        LEFT JOIN (
            SELECT td.SubAreaId as SubAreaId, SUM(td.Hours) AS Hours
            FROM [report].[TimeRegistrationData] td
                JOIN dbo.SubArea sa on sa.Id = td.SubAreaId
            WHERE @measurement = 4
                AND (@trappingType IS NULL OR @trappingType = '0' OR td.TrappingTypeId = @trappingType)
                AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= td.PeriodValue
                AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= td.PeriodValue
            GROUP BY SubAreaId
        ) AS Hours ON Hours.SubAreaId = sa.Id 
    WHERE TotalKm.Km > (CASE WHEN @measurement in (2,3,4) THEN 0 Else -1 END)
)
GO


CREATE OR ALTER FUNCTION [dbo].[fn_MapCatchesReport_SubAreaHourSquare]
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
    FROM dbo.SubAreaHourSquare sahs
        LEFT JOIN (
            SELECT cd.SubAreaHourSquareId as SubAreaHourSquareId, SUM(cd.CatchNumber) AS CatchNumber
            FROM [report].[CatchData] cd
                JOIN dbo.SubAreaHourSquare sahs on sahs.Id = cd.SubAreaHourSquareId
            WHERE 
                @measurement != 4
                AND cd.IsByCatch = (CASE WHEN @measurement in (1,3) THEN 1 Else 0 END)
                AND (@trappingType IS NULL OR @trappingType = '0' OR cd.TrappingTypeId = @trappingType)
                AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= cd.PeriodValue
                AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= cd.PeriodValue
            GROUP BY SubAreaHourSquareId
        ) AS Result ON Result.SubAreaHourSquareId = sahs.Id
        LEFT JOIN (
            SELECT td.SubAreaHourSquareId as SubAreaHourSquareId, SUM(td.Hours) AS Hours
            FROM [report].[TimeRegistrationData] td
                JOIN dbo.SubAreaHourSquare sahs on sahs.Id = td.SubAreaHourSquareId
            WHERE @measurement = 4
                AND (@trappingType IS NULL OR @trappingType = '0' OR td.TrappingTypeId = @trappingType)
                AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= td.PeriodValue
                AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= td.PeriodValue
            GROUP BY SubAreaHourSquareId
        ) AS Hours ON Hours.SubAreaHourSquareId = sahs.Id  
    WHERE sahs.KmWaterway > (CASE WHEN @measurement in (2,3,4) THEN 0 Else -1 END)
)
GO


CREATE OR ALTER FUNCTION [dbo].[fn_MapCatchesReport_WaterAuthority]
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
    FROM dbo.WaterAuthority wa 
        LEFT JOIN (
            SELECT SUM(sahs.KmWaterWay) as Km, sa.WaterAuthorityId
            FROM dbo.SubAreaHourSquare sahs
                JOIN dbo.SubArea sa on sa.id = sahs.SubAreaId
            GROUP BY WaterAuthorityId
        ) as TotalKm ON TotalKm.WaterAuthorityId = wa.Id
        LEFT JOIN (
            SELECT cd.WaterAuthorityId as WaterAuthorityId, SUM(cd.CatchNumber) AS CatchNumber
            FROM [report].[CatchData] cd
                JOIN dbo.WaterAuthority wa on wa.Id = cd.WaterAuthorityId
            WHERE 
                @measurement != 4
                AND cd.IsByCatch = (CASE WHEN @measurement in (1,3) THEN 1 Else 0 END)
                AND (@trappingType IS NULL OR @trappingType = '0' OR cd.TrappingTypeId = @trappingType)
                AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= cd.PeriodValue
                AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= cd.PeriodValue
            GROUP BY WaterAuthorityId
        ) AS Result ON Result.WaterAuthorityId = wa.Id
        LEFT JOIN (
            SELECT td.WaterAuthorityId as WaterAuthorityId, SUM(td.Hours) AS Hours
            FROM [report].[TimeRegistrationData] td
                JOIN dbo.WaterAuthority wa on wa.Id = td.WaterAuthorityId
            WHERE @measurement = 4
                AND (@trappingType IS NULL OR @trappingType = '0' OR td.TrappingTypeId = @trappingType)
                AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= td.PeriodValue
                AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= td.PeriodValue
            GROUP BY WaterAuthorityId
        ) AS Hours ON Hours.WaterAuthorityId = wa.Id 
    WHERE TotalKm.Km > (CASE WHEN @measurement in (2,3,4) THEN 0 Else -1 END)
)
GO


DROP FUNCTION [dbo].[fn_GetLatestVersionRegionalLayoutId]
GO


DROP VIEW [dbo].[vw_Rayons_Geo]
GO


DROP VIEW [dbo].[vw_CatchAreas_Geo]
GO


DROP VIEW [dbo].[vw_WaterAuthorities_Geo]
GO


DROP VIEW [dbo].[vw_SubAreas_Geo]
GO


DROP VIEW [dbo].[vw_SubAreaHourSquares_Geo]
GO


CREATE OR ALTER VIEW [dbo].[vw_SubAreaHourSquare_Catch_Geo] AS
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