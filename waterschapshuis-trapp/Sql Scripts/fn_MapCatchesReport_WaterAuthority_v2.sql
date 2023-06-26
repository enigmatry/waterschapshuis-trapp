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
)