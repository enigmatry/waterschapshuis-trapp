IF EXISTS (
	SELECT type_desc, type
	FROM sys.procedures WITH(NOLOCK)
	WHERE NAME = 'GetLocationAreaData' AND type = 'P'
)
DROP PROCEDURE [dbo].GetLocationAreaData

GO

CREATE PROCEDURE [dbo].GetLocationAreaData
	@CatchAreaId uniqueidentifier,
	@SubAreaId uniqueidentifier
AS
	-- Traps, per trap type for a certain catch area, ordered by trap type order.
	SELECT SUM(t.NumberOfTraps) AS Value, TT.Name AS ValueType, 1 AS SummaryType, TT.[Order]

	FROM Trap T
	INNER JOIN SubAreaHourSquare SAHS ON T.SubAreaHourSquareId = SAHS.Id
	INNER JOIN SubArea SA ON SAHS.SubAreaId = SA.Id
	INNER JOIN TrapType TT ON T.TrapTypeId = TT.Id
	WHERE Status = 1 AND SA.CatchAreaId = @CatchAreaId
	GROUP BY TT.Name, TT.[Order]

	UNION ALL

	-- Traps, per trap type for a certain sub area, ordered by trap type order.
	SELECT SUM(t.NumberOfTraps) AS Value, TT.Name AS ValueType, 2 AS SummaryType, TT.[Order]
	FROM Trap T
	INNER JOIN SubAreaHourSquare SAHS ON T.SubAreaHourSquareId = SAHS.Id
	INNER JOIN TrapType TT ON T.TrapTypeId = TT.Id
	WHERE Status = 1 AND SAHS.SubAreaId = @SubAreaId
	GROUP BY TT.Name, TT.[Order]

	UNION ALL

	-- Catches in previous week per catch type for a certain catch area.
	SELECT SUM(C.Number) AS Value, CT.Name AS ValueType, 3 AS SummaryType, CT.[Order]
	FROM Catch C
	INNER JOIN Trap T ON C.TrapId = T.Id
	INNER JOIN SubAreaHourSquare SAHS ON T.SubAreaHourSquareId = SAHS.Id
	INNER JOIN SubArea SA ON SAHS.SubAreaId = SA.Id
	INNER JOIN CatchType CT ON C.CatchTypeId = CT.Id
	WHERE SA.CatchAreaId = @CatchAreaId AND CT.IsByCatch = 0
	AND C.RecordedOn between DATEADD(week, -1, DATEADD(ww, DATEDIFF(ww,0,GETDATE()), 0)) AND DATEADD(ww, DATEDIFF(ww,0,GETDATE()), 0)
	GROUP BY CT.Name, CT.[Order]

	UNION ALL

	-- Catches in previous week per catch type for a certain sub area.
	SELECT SUM(C.Number) AS Value, CT.Name AS ValueType, 4 AS SummaryType, CT.[Order]
	FROM Catch C
	INNER JOIN Trap T on C.TrapId = T.Id
	INNER JOIN SubAreaHourSquare SAHS on T.SubAreaHourSquareId = SAHS.Id
	INNER JOIN CatchType CT on C.CatchTypeId = CT.Id
	WHERE SAHS.SubAreaId = @SubAreaId AND CT.IsByCatch = 0
	AND C.RecordedOn between DATEADD(week, -1, DATEADD(ww, DATEDIFF(ww,0,GETDATE()), 0)) AND DATEADD(ww, DATEDIFF(ww,0,GETDATE()), 0)
	GROUP BY CT.Name, CT.[Order]

	UNION ALL

	-- By-catches in previous week per catch type for a certain catch area.
	SELECT SUM(C.Number) AS Value, CT.Name AS ValueType, 5 AS SummaryType, CT.[Order]
	FROM Catch C
	INNER JOIN Trap T ON C.TrapId = T.Id
	INNER JOIN SubAreaHourSquare SAHS ON T.SubAreaHourSquareId = SAHS.Id
	INNER JOIN SubArea SA ON SAHS.SubAreaId = SA.Id
	INNER JOIN CatchType CT ON C.CatchTypeId = CT.Id
	WHERE SA.CatchAreaId = @CatchAreaId AND CT.IsByCatch = 1
	AND C.RecordedOn between DATEADD(week, -1, DATEADD(ww, DATEDIFF(ww,0,GETDATE()), 0)) AND DATEADD(ww, DATEDIFF(ww,0,GETDATE()), 0)
	GROUP BY CT.Name, CT.[Order]

	UNION ALL

	-- By-catches in previous week per catch type for a certain sub area.
	SELECT SUM(C.Number) AS Value, CT.Name AS ValueType, 6 AS SummaryType, CT.[Order]
	FROM Catch C
	INNER JOIN Trap T on C.TrapId = T.Id
	INNER JOIN SubAreaHourSquare SAHS on T.SubAreaHourSquareId = SAHS.Id
	INNER JOIN CatchType CT on C.CatchTypeId = CT.Id
	WHERE SAHS.SubAreaId = @SubAreaId AND CT.IsByCatch = 1
	AND C.RecordedOn between DATEADD(week, -1, DATEADD(ww, DATEDIFF(ww,0,GETDATE()), 0)) AND DATEADD(ww, DATEDIFF(ww,0,GETDATE()), 0)
	GROUP BY CT.Name, CT.[Order]

	UNION ALL

	-- Hours in previous week for a certain catch area.
	SELECT sum(TR.Hours) AS Value, NULL AS ValueType, 7 AS SummaryType, NULL AS [Order]
	FROM TimeRegistration TR
	INNER JOIN SubAreaHourSquare SAHS on TR.SubAreaHourSquareId = SAHS.Id
	INNER JOIN SubArea SA on SAHS.SubAreaId = SA.Id
	WHERE SA.CatchAreaId = @CatchAreaId
	AND TR.Date between DATEADD(week, -1, DATEADD(ww, DATEDIFF(ww,0,GETDATE()), 0)) AND DATEADD(ww, DATEDIFF(ww,0,GETDATE()), 0)

	UNION ALL

	-- Hours in previous week for a certain sub area.
	SELECT SUM(TR.Hours) AS Value,  NULL AS ValueType, 8 AS SummaryType, NULL AS [Order]
	FROM TimeRegistration TR
	INNER JOIN SubAreaHourSquare SAHS on TR.SubAreaHourSquareId = SAHS.Id
	WHERE SAHS.SubAreaId = @SubAreaId
	AND TR.Date between DATEADD(week, -1, DATEADD(ww, DATEDIFF(ww,0,GETDATE()), 0)) AND DATEADD(ww, DATEDIFF(ww,0,GETDATE()), 0)

	ORDER BY SummaryType, [Order]
GO