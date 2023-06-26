CREATE OR ALTER    FUNCTION [dbo].[fn_GetWeekOfYearWithCustomRule] 
(
	@date DATE
)
RETURNS INT
AS
BEGIN
	DECLARE @result int;
	DECLARE @month int = MONTH(@date);
	DECLARE @day int = DAY(@date);
	DECLARE @numberOfWeeksInYear int = DATEPART(isowk, DATEFROMPARTS( YEAR(@date), 12, 28));
	DECLARE @weekDay int = DATEPART(WEEKDAY, @date);

	SET @result=
					CASE 
					WHEN @month = 1 AND @day <=3 AND (@weekDay > 5 OR @weekDay = 1) THEN 1
					WHEN @month = 12 AND @day >=29 AND (@weekDay < 5 AND @weekDay != 1) THEN @numberOfWeeksInYear + 1

	                ELSE DATEPART(isowk, @date)
					END

	RETURN @result;
END
GO

---update existing data with newly calculated week numbers

UPDATE TimeRegistration
SET Week = [dbo].[fn_GetWeekOfYearWithCustomRule] (Date)
WHERE [dbo].[fn_GetWeekOfYearWithCustomRule] (Date) != Week

UPDATE TimeRegistrationGeneral
SET Week = [dbo].[fn_GetWeekOfYearWithCustomRule] (Date)
WHERE [dbo].[fn_GetWeekOfYearWithCustomRule] (Date) != Week

UPDATE Catch
SET Week = [dbo].[fn_GetWeekOfYearWithCustomRule] (RecordedOn)
WHERE [dbo].[fn_GetWeekOfYearWithCustomRule] (RecordedOn) != Week

