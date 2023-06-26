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
)