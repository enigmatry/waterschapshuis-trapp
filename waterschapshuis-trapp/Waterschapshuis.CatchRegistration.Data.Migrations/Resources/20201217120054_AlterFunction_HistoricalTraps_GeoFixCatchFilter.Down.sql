
--------------------------------------------------
CREATE OR ALTER FUNCTION [dbo].[fn_HistoricalTraps_Geo]
(	
	@status int,
	@trapUpdatedYear int,
	@showPastYearCatchesOnly bit,
	@trapTypeId varchar(36),
	@trapStartDate datetime,
	@trapEndDate datetime,
	@catchStartDate datetime,
	@catchEndDate datetime,
	@catchType int
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
		SUM(
		CASE WHEN ct.IsByCatch = 0 
		AND(@catchType = 2 
			OR 
			--beverrat
			( @catchType=0 AND ct.Id in ('85803328-15E7-92EF-528F-00E91B6D4815',
										'2539B02A-9298-7B9F-4273-3E8AC99D7C63',
										'7A8199E8-21DF-7556-1F0A-549E94645B6F',
										'49B51935-918B-5A38-2493-A4141FEF8C52',
										'8957CB9D-936C-29CB-8511-A9C9A7EC6A7E')) 
			OR 
			--muskusrat
			( @catchType=1 AND ct.Id in ('645F7089-7F21-50C5-30C4-5FE30CC693F1',
										'3D1358F4-61D4-21D8-9438-90096EEEA47E',
										'44711E96-25B8-0AF6-669B-CCDC8ABA9017',
										'E72CCB01-65BB-A1AA-A5E8-EB909FE77374',
										'C8783519-41C6-5654-1977-F6956ABA2EF4'))) 
		-- no filter is applied, so default values are provided
		AND ((@showPastYearCatchesOnly = 1 AND @catchStartDate = '1900-01-01' AND @catchEndDate = '9999-01-01' AND c.RecordedOn> DateAdd(dd, 1, DateAdd(yy, -1, GetDate())) AND c.RecordedOn < GETDATE() )
		-- filter is applied or @showPastYearCatchesOnly is 0 no matter what is the filter
		OR (c.RecordedOn>= @catchStartDate AND  c.RecordedOn < DateAdd(dd, 1, @catchEndDate)) )
		THEN c.Number 
		ELSE 0
		END	) AS NumberOfCatches,
		SUM(
		CASE WHEN ct.IsByCatch = 1 AND (c.RecordedOn>= @catchStartDate) AND (c.RecordedOn < DateAdd(dd, 1, @catchEndDate)) THEN c.Number 
		ELSE 0
		END) AS NumberOfByCatches
	FROM dbo.Trap t
	LEFT OUTER JOIN Catch c on c.TrapId = t.Id
	LEFT OUTER JOIN CatchType ct on c.CatchTypeId = ct.Id

	WHERE 
	t.Status = @status
	AND (@trapUpdatedYear = 0  OR YEAR(t.UpdatedOn)= @trapUpdatedYear)
	AND (@trapStartDate is null OR @trapStartDate = 0 OR @trapStartDate <= t.CreatedOn)
	AND (@trapEndDate is null OR @trapEndDate = 0 OR  t.CreatedOn < DateAdd(dd, 1, @trapEndDate))
	AND (@trapTypeId is null OR @trapTypeId = '0' OR t.TrapTypeId = @trapTypeId)


	GROUP BY t.Id
) tpc ON tpc.TrapId = t.Id
)
GO
