--------------------------------------------------
DROP FUNCTION IF EXISTS [dbo].[fn_HistoricalTraps_Geo]
GO

--------------------------------------------------
CREATE FUNCTION [dbo].[fn_HistoricalTraps_Geo]
(	
	@showPastYearCatchesOnly bit, 
	@status int,
	@trapUpdatedYear int
)

RETURNS TABLE 
AS
RETURN 
(
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

	WHERE 
	t.Status = @status
	AND (@trapUpdatedYear = 0  OR YEAR(t.UpdatedOn)= @trapUpdatedYear)
	AND ((@showPastYearCatchesOnly = 1 AND c.RecordedOn> DateAdd(dd, 1, DateAdd(yy, -1, GetDate())) AND c.RecordedOn < GETDATE() )
	OR @showPastYearCatchesOnly = 0)

	GROUP BY t.Id
) tpc ON tpc.TrapId = t.Id
)
GO

--------------------------------------------------
DROP VIEW IF EXISTS [dbo].[vw_HistoricalTraps_Geo]
GO