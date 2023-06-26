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