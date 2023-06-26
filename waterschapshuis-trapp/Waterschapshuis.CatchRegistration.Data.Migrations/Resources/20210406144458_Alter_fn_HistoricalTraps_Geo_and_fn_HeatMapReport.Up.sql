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
	@catchType int,
	@showTrapsWithCatches bit
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
			( @catchType=0 AND LOWER(ct.Name) LIKE '%beverrat%')  
			OR 
			--muskusrat
			( @catchType=1 AND LOWER(ct.Name) LIKE '%muskusrat%'))  
		-- no filter is applied, so default values are provided
		AND ((@showPastYearCatchesOnly = 1 AND @catchStartDate = '1900-01-01' AND @catchEndDate = '9999-01-01' AND c.RecordedOn> DateAdd(dd, 1, DateAdd(yy, -1, GetDate())) AND c.RecordedOn < GETDATE() )
		-- filter is applied or @showPastYearCatchesOnly is 0 no matter what is the filter
		OR ((@catchStartDate != '1900-01-01' OR @catchEndDate != '9999-01-01' OR @showPastYearCatchesOnly = 0) AND c.RecordedOn>= @catchStartDate AND  c.RecordedOn < DateAdd(dd, 1, @catchEndDate)) )
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

WHERE NumberOfCatches >= 
	CASE WHEN @showTrapsWithCatches = 0 THEN 0 
		ELSE 1
	END
)
GO


---MapReport Heatmap
CREATE OR ALTER FUNCTION [dbo].[fn_HeatMapReport]
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
		INNER JOIN  CatchType ct ON cd.CatchTypeId = ct.Id
		WHERE 
			cd.IsByCatch = 0
			AND (@organizationId IS NULL OR @organizationId='0' OR cd.OrganizationId = @organizationId) 
			AND (@startDate is null OR @startDate < cd.RecordedOn)
			AND (@endDate is null OR cd.RecordedOn < @endDate )
			AND ((@isBeverrat is null OR @isBeverrat = 0)
				OR (@isBeverrat = 1 AND LOWER(ct.Name) LIKE '%beverrat%') 
				OR (@isBeverrat = 2 AND LOWER(ct.Name) LIKE '%muskusrat%') 
			)
		GROUP BY cd.TrapId
	) Result ON Result.TrapId = t.Id  
)
GO


