CREATE OR ALTER  FUNCTION [dbo].[fn_GetAbsolutePeriodOfYear] 
(
	@date DATE
)
RETURNS INT
AS
BEGIN
	DECLARE @result int;

	SET @result = CONVERT(int, CEILING(CONVERT(decimal, DATEPART(wk, @date)) / 4.00));

	IF (@result > 13) SET @result = 13;

	RETURN @result;
END

GO

DECLARE @maxPeriod int = 13;

WHILE EXISTS (SELECT * FROM [dbo].[TimeRegistration] WHERE [Period] > @maxPeriod)
BEGIN
UPDATE TOP (1000) [dbo].[TimeRegistration]
	SET [Period] = @maxPeriod
WHERE 
	[Period] > @maxPeriod
END

WHILE EXISTS (SELECT * FROM [dbo].[Catch] WHERE [Period] > @maxPeriod)
BEGIN
UPDATE TOP (1000) [dbo].[Catch]
	SET [Period] = @maxPeriod
WHERE 
	[Period] > @maxPeriod
END
