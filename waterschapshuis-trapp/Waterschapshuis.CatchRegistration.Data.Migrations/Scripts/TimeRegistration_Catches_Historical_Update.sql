/***** Set dbo.TimeRegistration correct Week, Period and Year *****/

WHILE EXISTS (SELECT * FROM dbo.TimeRegistration WHERE [Year] <> 0 OR [Week] <> 0 OR [Period] <> 0)
BEGIN
UPDATE TOP (1000) dbo.TimeRegistration
SET [Year] = 0,
	[Week] = 0, 
	[Period] = 0
WHERE 
	[Year] <> 0 OR [Week] <> 0 OR [Period] <> 0
END


WHILE EXISTS (SELECT * FROM dbo.TimeRegistration WHERE [Year] = 0 OR [Week] = 0 OR [Period] = 0)
BEGIN
UPDATE TOP (1000) dbo.TimeRegistration
SET [Year] = DATEPART(yyyy, [Date]),
	[Week] = dbo.fn_GetAbsoluteWeekOfYear([Date]), 
	[Period] = dbo.fn_GetAbsolutePeriodOfYear([Date])
WHERE 
	[Year] = 0 OR [Week] = 0 OR [Period] = 0
END


/***** Set dbo.[Catch] correct Week, Period and Year *****/

WHILE EXISTS (SELECT * FROM dbo.[Catch] WHERE [Year] <> 0 OR [Week] <> 0 OR [Period] <> 0)
BEGIN
UPDATE TOP (1000) dbo.[Catch]
SET [Year] = 0,
	[Week] = 0, 
	[Period] = 0
WHERE 
	[Year] <> 0 OR [Week] <> 0 OR [Period] <> 0
END


WHILE EXISTS (SELECT * FROM dbo.[Catch] WHERE [Year] = 0 OR [Week] = 0 OR [Period] = 0)
BEGIN
UPDATE TOP (1000) dbo.[Catch]
SET [Year] = DATEPART(yyyy, [RecordedOn]),
	[Week] = dbo.fn_GetAbsoluteWeekOfYear([RecordedOn]), 
	[Period] = dbo.fn_GetAbsolutePeriodOfYear([RecordedOn])
WHERE 
	[Year] = 0 OR [Week] = 0 OR [Period] = 0
END


DECLARE @date date = DATEADD(DAY, -6 * 7, GETUTCDATE());
DECLARE @completedStatus tinyint = 3;

/***** Set dbo.[TimeRegistration] Status completed for 6 weeks old records *****/

WHILE EXISTS (SELECT * FROM [dbo].[TimeRegistration] WHERE [Date] <= @date AND [Status] <> @completedStatus)
BEGIN
UPDATE TOP (1000) [dbo].[TimeRegistration]
SET [Status] = @completedStatus
WHERE 
	[Date] <= @date AND [Status] <> @completedStatus
END


/***** Set dbo.[Catch] Status completed for 6 weeks old records *****/

WHILE EXISTS (SELECT * FROM [dbo].[Catch] WHERE [RecordedOn] <= @date AND [Status] <> @completedStatus)
BEGIN
UPDATE TOP (1000) [dbo].[Catch]
SET [Status] = @completedStatus
WHERE
	[RecordedOn] <= @date AND [Status] <> @completedStatus
END