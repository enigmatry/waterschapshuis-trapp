CREATE     FUNCTION [dbo].[fn_GetIso86001DayOfWeek]
( 
	@date DATE
)
RETURNS INT 
AS 
BEGIN
	DECLARE @dateFirst TINYINT = @@DATEFIRST;

	RETURN (DATEPART(dw, @date) + @dateFirst - 2) % 7 + 1;
END
GO

CREATE     FUNCTION [dbo].[fn_GetIso8601PeriodOfYear] 
                (
	                @date DATE
                )
                RETURNS INT
                AS
                BEGIN
	                DECLARE @result int;

	                SET @result = CONVERT(int, CEILING(CONVERT(decimal, DATEPART(isowk, @date)) / 4.00));

	                RETURN @result;
                END
GO

CREATE     FUNCTION [dbo].[fn_GetIso8601WeekOfYear] 
                (
	                @date DATE
                )
                RETURNS INT
                AS
                BEGIN
	                DECLARE @result int;

	                SET @result = DATEPART(isowk, @date);

	                RETURN @result;
                END
GO

CREATE     FUNCTION [dbo].[fn_GetIso8601YearForWeekOfYear] 
                (
	                @date DATE
                )
                RETURNS INT
                AS
                BEGIN
	                DECLARE @result int;

	                DECLARE @year int = DATEPART(yy, @date);
	                DECLARE @month int = DATEPART(mm, @date);
	                DECLARE @day int = DATEPART(dd, @date);
	                DECLARE @weekDay int = [dbo].[fn_GetIso86001DayOfWeek](@date);

	                SET @result = @year;

	                IF(@month = 1 AND @day <= 3 AND @weekDay > 3)
		                SET @result -= 1;

	                IF(@month = 12 AND @day >= 29 AND @weekDay <= 3)
		                SET @result += 1;

	                RETURN @result;
                END
GO
