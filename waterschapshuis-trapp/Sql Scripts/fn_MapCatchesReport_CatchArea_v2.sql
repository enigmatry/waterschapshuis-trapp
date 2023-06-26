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
)