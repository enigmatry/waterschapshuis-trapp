CREATE OR ALTER  FUNCTION [dbo].[fn_GetAbsolutePeriodOfYear] 
(
	@date DATE
)
RETURNS INT
AS
BEGIN
	DECLARE @result int;

	SET @result = CONVERT(int, CEILING(CONVERT(decimal, DATEPART(wk, @date)) / 4.00));

	RETURN @result;
END