CREATE OR ALTER VIEW [dbo].[vw_TrackingLines_Geo] AS
SELECT
	*,
	YEAR(TL.[Date]) AS 'CreatedOnYear', 
	DATEDIFF(DAY, TL.[Date], SYSDATETIMEOFFSET()) AS 'DaysOffset',
	CAST(TL.[Date] AS datetime) AS 'TrackingDate'
FROM [TrackingLine] AS TL