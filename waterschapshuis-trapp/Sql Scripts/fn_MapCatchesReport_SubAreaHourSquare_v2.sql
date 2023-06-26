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
)