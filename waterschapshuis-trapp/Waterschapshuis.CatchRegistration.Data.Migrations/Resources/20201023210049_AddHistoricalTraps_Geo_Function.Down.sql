DROP VIEW [dbo].[vw_HistoricalTraps_Geo]
GO

CREATE     VIEW [dbo].[vw_HistoricalTraps_Geo] AS
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


--------------------------------------------------
DROP FUNCTION [dbo].[fn_HistoricalTraps_Geo]
GO