
-----------------------------------------------------------
CREATE OR ALTER          PROCEDURE [report].[PopulateOwnReportData]
AS
    DECLARE @ResultValue int
BEGIN
    TRUNCATE TABLE [report].[OwnReportData]

    INSERT INTO [report].[OwnReportData]
	    ([CreatedOn]
	    ,[RecordedOnYear]
	    ,[NumberOfCatches]
	    ,[NumberOfByCatches]
	    ,[NumberOfCatchesPreviousYear]
	    ,[NumberOfByCatchesPreviousYear]
	    ,[Period]
	    ,[Week]
	    ,[OwnerId]
	    ,[OwnerName]
	    ,[NumberOfTraps]
	    ,[TrapStatus]
	    ,[TrapTypeName]
	    ,[TrappingTypeName]
	    ,[IsByCatch]
	    ,[CatchTypeName]
	    ,[OrganizationId]
	    ,[OrganizationName]
	    ,[WaterAuthorityName]
	    ,[RayonName]
	    ,[CatchAreaName]
	    ,[SubAreaName]
	    ,[HourSquareName]
	    ,[ProvinceName]
	    ,[FieldTestName]
	    ,[Hours]
	    ,[HoursPreviousYear]
		,[CatchingNights]
		,[KmWaterway])
    SELECT 
	    Result.CreatedOn,
	    Result.RecordedOnYear,
	    Result.NumberOfCatches,
	    Result.NumberOfByCatches,
	    Result.NumberOfCatchesPreviousYear,
	    Result.NumberOfByCatchesPreviousYear,
	    Result.[Period],
	    Result.[Week],
	    Result.OwnerId,
	    Result.OwnerName,
	    Result.NumberOfTraps,
	    Result.TrapStatus,
	    Result.TrapTypeName,
	    Result.TrappingTypeName,
	    Result.IsByCatch,
	    Result.CatchTypeName,
	    Result.OrganizationId,
	    Result.OrganizationName,
	    Result.WaterAuthorityName,
	    Result.RayonName,
	    Result.CatchAreaName,
	    Result.SubAreaName,
	    Result.HourSquareName,
	    Result.ProvinceName,
	    Result.FieldTestName,
	    Result.[Hours],
	    Result.HoursPreviousYear,
		Result.CatchingNights,
		Result.KmWaterway
    FROM (

    -- CATCHES
    SELECT
	    C.CreatedOn AS 'CreatedOn',
	    YEAR(C.RecordedOn) AS 'RecordedOnYear',
	    C.CreatedById AS 'OwnerId',
	    U.[Name] AS 'OwnerName',
	    C.[Period] AS 'Period',
	    C.[Week] AS 'Week',
	    SUM(CASE CT.IsByCatch WHEN 0 THEN C.Number ELSE 0 END) AS 'NumberOfCatches', 
	    SUM(CASE CT.IsByCatch WHEN 1 THEN C.Number ELSE 0 END) AS 'NumberOfByCatches',
	    NULL AS 'NumberOfCatchesPreviousYear', 
	    NULL AS 'NumberOfByCatchesPreviousYear',
	    NULL AS 'NumberOfTraps',
	    NULL AS 'TrapStatus',
	    NULL AS 'TrapTypeName',
	    TrT.[Name] AS 'TrappingTypeName',
	    CT.IsByCatch AS 'IsByCatch', 
	    CT.[Name] AS 'CatchTypeName',
	    O.Id AS 'OrganizationId',
	    O.[Name] AS 'OrganizationName',
	    WA.[Name] AS 'WaterAuthorityName',
	    R.[Name] AS 'RayonName',
	    CA.[Name] AS 'CatchAreaName',
	    SA.[Name] AS 'SubAreaName',
	    P.[Name] AS 'ProvinceName',
	    HS.[Name] AS 'HourSquareName',
	    STRING_AGG (FT.Name, ',') WITHIN GROUP (ORDER BY FT.Name)AS 'FieldTestName',
	    NULL AS 'Hours', 
	    NULL AS 'HoursPreviousYear',
		NULL as 'CatchingNights',
		NULL as 'KmWaterway'
    FROM [Catch] AS C
	    INNER JOIN [User] AS U ON C.CreatedById = U.Id
        INNER JOIN [Trap] AS T ON C.TrapId = T.Id
        INNER JOIN [TrapType] AS TT ON T.TrapTypeId = TT.Id
        INNER JOIN [TrappingType] AS TrT ON TT.TrappingTypeId = TrT.Id
        INNER JOIN [CatchType] AS CT ON C.CatchTypeId = CT.Id
        INNER JOIN [SubAreaHourSquare] AS SAHS ON T.SubAreaHourSquareId = SAHS.Id
        INNER JOIN [HourSquare] AS HS ON SAHS.HourSquareId = HS.Id
		LEFT JOIN [FieldTestHourSquare] AS FTHS ON FTHS.HourSquareId = HS.Id		
		LEFT JOIN [FieldTest] AS FT ON FT.Id = FTHS.FieldTestId AND dbo.fn_GetPeriodValue(FT.StartYear, FT.StartPeriod, 0) <= dbo.fn_GetPeriodValue(C.Year, C.Period, 0)
			AND dbo.fn_GetPeriodValue(FT.EndYear, FT.EndPeriod, 1) >= dbo.fn_GetPeriodValue(C.Year, C.Period, 1)
        INNER JOIN [SubArea] AS SA ON SAHS.SubAreaId = SA.Id
        INNER JOIN [CatchArea] AS CA ON SA.CatchAreaId = CA.Id
        INNER JOIN [Rayon] AS R ON CA.RayonId = R.Id
        INNER JOIN [Organization] AS O ON R.OrganizationId = O.Id
        INNER JOIN [WaterAuthority] AS WA ON SA.WaterAuthorityId = WA.Id
        LEFT OUTER JOIN [Province] AS P ON T.ProvinceId = P.Id
    GROUP BY
        C.Id,
	    C.CreatedOn,
        C.RecordedOn,
	    C.[Period],
	    C.[Week],
        C.CreatedById,
	    CT.IsByCatch, 
	    CT.[Name],
	    TrT.[Name],
	    SA.[Name],
	    CA.[Name],
	    R.[Name],
        O.Id,
	    O.[Name],
	    HS.[Name],
	    WA.[Name],
	    p.[Name],
	    U.[Name]

    UNION

    -- CATCES PREVIOUS YEAR
    SELECT
	    C.CreatedOn AS 'CreatedOn',
	    YEAR(C.RecordedOn) + 1 AS 'RecordedOnYear',
	    C.CreatedById AS 'OwnerId',
	    U.[Name] AS 'OwnerName',
	    C.[Period] AS 'Period',
	    C.[Week] AS 'Week',
	    NULL AS 'NumberOfCatches', 
	    NULL AS 'NumberOfByCatches',
	    SUM(CASE CT.IsByCatch WHEN 0 THEN C.Number ELSE 0 END) AS 'NumberOfCatchesPreviousYear', 
	    SUM(CASE CT.IsByCatch WHEN 1 THEN C.Number ELSE 0 END) AS 'NumberOfByCatchesPreviousYear',
	    NULL AS 'NumberOfTraps',
	    NULL AS 'TrapStatus',
	    NULL AS 'TrapTypeName',
	    TrT.[Name] AS 'TrappingTypeName',
	    CT.IsByCatch AS 'IsByCatch', 
	    CT.[Name] AS 'CatchTypeName',
	    O.Id AS 'OrganizationId',
	    O.[Name] AS 'OrganizationName',
	    WA.[Name] AS 'WaterAuthorityName',
	    R.[Name] AS 'RayonName',
	    CA.[Name] AS 'CatchAreaName',
	    SA.[Name] AS 'SubAreaName',
	    P.[Name] AS 'ProvinceName',
	    HS.[Name] AS 'HourSquareName',
	    NULL AS 'FieldTestName',
	    NULL AS 'Hours', 
	    NULL AS 'HoursPreviousYear',
		NULL as 'CatchingNights',
		NULL as 'KmWaterway'
    FROM [Catch] AS C
	    INNER JOIN [User] AS U ON C.CreatedById = U.Id
        INNER JOIN [Trap] AS T ON C.TrapId = T.Id
        INNER JOIN [TrapType] AS TT ON T.TrapTypeId = TT.Id
        INNER JOIN [TrappingType] AS TrT ON TT.TrappingTypeId = TrT.Id
        INNER JOIN [CatchType] AS CT ON C.CatchTypeId = CT.Id
        INNER JOIN [SubAreaHourSquare] AS SAHS ON T.SubAreaHourSquareId = SAHS.Id
        INNER JOIN [HourSquare] AS HS ON SAHS.HourSquareId = HS.Id
        INNER JOIN [SubArea] AS SA ON SAHS.SubAreaId = SA.Id
        INNER JOIN [CatchArea] AS CA ON SA.CatchAreaId = CA.Id
        INNER JOIN [Rayon] AS R ON CA.RayonId = R.Id
        INNER JOIN [Organization] AS O ON R.OrganizationId = O.Id
        INNER JOIN [WaterAuthority] AS WA ON SA.WaterAuthorityId = WA.Id
        LEFT OUTER JOIN [Province] AS P ON T.ProvinceId = P.Id
        WHERE YEAR(C.RecordedOn) < YEAR(GETDATE())
    GROUP BY
        C.Id,
	    C.CreatedOn,
        C.RecordedOn,
	    C.[Period],
	    C.[Week],
        C.CreatedById,
	    CT.IsByCatch, 
	    CT.[Name],
	    TrT.[Name],
	    SA.[Name],
	    CA.[Name],
	    R.[Name],
        O.Id,
	    O.[Name],
	    HS.[Name],
	    WA.[Name],
	    p.[Name],
	    U.[Name]

    UNION

    -- TIME REGISTRATIONS
    SELECT
	    TR.CreatedOn AS 'CreatedOn',
	    YEAR(TR.[Date]) AS 'RecordedOnYear',
	    TR.UserId AS 'OwnerId',
	    U.[Name] AS 'OwnerName',
	    TR.[Period] AS 'Period',
	    TR.[Week] AS 'Week',
	    NULL AS 'NumberOfCatches', 
	    NULL AS 'NumberOfByCatches',
	    NULL AS 'NumberOfCatchesPreviousYear', 
	    NULL AS 'NumberOfByCatchesPreviousYear',
	    NULL AS 'NumberOfTraps',
	    NULL AS 'TrapStatus',
	    NULL AS 'TrapTypeName',
	    TrT.[Name] AS 'TrappingTypeName',
	    NULL AS 'IsByCatch', 
	    NULL AS 'CatchTypeName',
	    O.Id AS 'OrganizationId',
	    O.[Name] AS 'OrganizationName',
	    WA.[Name] AS 'WaterAuthorityName',
	    R.[Name] AS 'RayonName',
	    CA.[Name] AS 'CatchAreaName',
	    SA.[Name] AS 'SubAreaName',
	    NULL AS 'ProvinceName',
	    HS.[Name] AS 'HourSquareName',
	    STRING_AGG (FT.Name, ',') WITHIN GROUP (ORDER BY FT.Name)AS 'FieldTestName',
	    SUM(TR.[Hours]) AS 'Hours', 
	    NULL AS 'HoursPreviousYear',
		NULL as 'CatchingNights',
		NULL as 'KmWaterway'
    FROM [TimeRegistration] AS TR
	    INNER JOIN [User] AS U ON TR.UserId = U.Id
        INNER JOIN [TrappingType] TrT ON TrT.Id = TR.TrappingTypeId
        INNER JOIN [SubAreaHourSquare] SAHS ON SAHS.Id = TR.SubAreaHourSquareId
        INNER JOIN [SubArea] SA ON SA.Id = SAHS.SubAreaId
        INNER JOIN [HourSquare] HS ON HS.Id = SAHS.HourSquareId
		LEFT JOIN [FieldTestHourSquare] AS FTHS ON FTHS.HourSquareId = HS.Id		
		LEFT JOIN [FieldTest] AS FT ON FT.Id = FTHS.FieldTestId AND dbo.fn_GetPeriodValue(FT.StartYear, FT.StartPeriod, 0) <= dbo.fn_GetPeriodValue(TR.Year, TR.Period, 0)
			AND dbo.fn_GetPeriodValue(FT.EndYear, FT.EndPeriod, 1) >= dbo.fn_GetPeriodValue(TR.Year, TR.Period, 1)
        INNER JOIN [CatchArea] CA ON CA.Id = SA.CatchAreaId
        INNER JOIN [Rayon] R ON R.Id = CA.RayonId
        INNER JOIN [Organization] O ON O.Id = R.OrganizationId
        INNER JOIN [WaterAuthority] WA ON WA.Id = SA.WaterAuthorityId
    GROUP BY
        TR.Id,
	    TR.CreatedOn,
        TR.[Date],
	    TR.[Period],
	    TR.[Week],
        TR.UserId,
	    TrT.[Name],
	    SA.[Name],
	    CA.[Name],
	    R.[Name],
        O.Id,
	    O.[Name],
	    HS.[Name],
	    WA.[Name],
	    U.[Name]

    UNION

    -- TIME REGISTRATIONS PREVIOUS YEAR
    SELECT
	    TR.CreatedOn AS 'CreatedOn',
	    YEAR(TR.[Date]) + 1 AS 'RecordedOnYear',
	    TR.UserId AS 'OwnerId',
	    U.[Name] AS 'OwnerName',
	    TR.[Period] AS 'Period',
	    TR.[Week] AS 'Week',
	    NULL AS 'NumberOfCatches', 
	    NULL AS 'NumberOfByCatches',
	    NULL AS 'NumberOfCatchesPreviousYear', 
	    NULL AS 'NumberOfByCatchesPreviousYear',
	    NULL AS 'NumberOfTraps',
	    NULL AS 'TrapStatus',
	    NULL AS 'TrapTypeName',
	    TrT.[Name] AS 'TrappingTypeName',
	    NULL AS 'IsByCatch', 
	    NULL AS 'CatchTypeName',
	    O.Id AS 'OrganizationId',
	    O.[Name] AS 'OrganizationName',
	    WA.[Name] AS 'WaterAuthorityName',
	    R.[Name] AS 'RayonName',
	    CA.[Name] AS 'CatchAreaName',
	    SA.[Name] AS 'SubAreaName',
	    NULL AS 'ProvinceName',
	    HS.[Name] AS 'HourSquareName',
	    NULL AS 'FieldTestName',
	    NULL AS 'Hours', 
	    SUM(TR.[Hours]) AS 'HoursPreviousYear',
		NULL as 'CatchingNights',
		NULL as 'KmWaterway'
    FROM [TimeRegistration] AS TR
	    INNER JOIN [User] AS U ON TR.UserId = U.Id
        INNER JOIN [TrappingType] TrT ON TrT.Id = TR.TrappingTypeId
        INNER JOIN [SubAreaHourSquare] SAHS ON SAHS.Id = TR.SubAreaHourSquareId
        INNER JOIN [SubArea] SA ON SA.Id = SAHS.SubAreaId
        INNER JOIN [HourSquare] HS ON HS.Id = SAHS.HourSquareId
        INNER JOIN [CatchArea] CA ON CA.Id = SA.CatchAreaId
        INNER JOIN [Rayon] R ON R.Id = CA.RayonId
        INNER JOIN [Organization] O ON O.Id = R.OrganizationId
        INNER JOIN [WaterAuthority] WA ON WA.Id = SA.WaterAuthorityId
        WHERE YEAR(TR.Date) < YEAR(GETDATE())
    GROUP BY
        TR.Id,
	    TR.CreatedOn,
        TR.[Date],
	    TR.[Period],
	    TR.[Week],
        TR.UserId,
	    TrT.[Name],
	    SA.[Name],
	    CA.[Name],
	    R.[Name],
        O.Id,
	    O.[Name],
	    HS.[Name],
	    WA.[Name],
	    U.[Name]

	UNION 

	 -- KM of Waterways
    SELECT
	    NULL AS 'CreatedOn',
	    NULL AS 'RecordedOnYear',
	    NULL AS 'OwnerId',
	    NULL AS 'OwnerName',
	    NULL AS 'Period',
	    NULL AS 'Week',
	    NULL AS 'NumberOfCatches', 
	    NULL AS 'NumberOfByCatches',
	    NULL AS 'NumberOfCatchesPreviousYear', 
	    NULL AS 'NumberOfByCatchesPreviousYear',
	    NULL AS 'NumberOfTraps',
	    NULL AS 'TrapStatus',
	    NULL AS 'TrapTypeName',
	    NULL AS 'TrappingTypeName',
	    NULL AS 'IsByCatch', 
	    NULL AS 'CatchTypeName',
	    O.Id AS 'OrganizationId',
	    O.[Name] AS 'OrganizationName',
	    WA.[Name] AS 'WaterAuthorityName',
	    R.[Name] AS 'RayonName',
	    CA.[Name] AS 'CatchAreaName',
	    SA.[Name] AS 'SubAreaName',
	    NULL AS 'ProvinceName',
	    HS.[Name] AS 'HourSquareName',
	    NULL AS 'FieldTestName',
	    NULL AS 'Hours', 
	    NULL AS 'HoursPreviousYear',
		NULL as 'CatchingNights',
		SAHS.KmWaterway as 'KmWaterway'
    FROM 
		[SubAreaHourSquare] AS SAHS
        INNER JOIN [HourSquare] AS HS ON SAHS.HourSquareId = HS.Id
        INNER JOIN [SubArea] AS SA ON SAHS.SubAreaId = SA.Id
        INNER JOIN [CatchArea] AS CA ON SA.CatchAreaId = CA.Id
        INNER JOIN [Rayon] AS R ON CA.RayonId = R.Id
        INNER JOIN [Organization] AS O ON R.OrganizationId = O.Id
        INNER JOIN [WaterAuthority] AS WA ON SA.WaterAuthorityId = WA.Id
	
    GROUP BY
		SAHS.KmWaterway,
		SAHS.[Id],
	    SA.[Name],
	    CA.[Name],
	    R.[Name],
        O.Id,
	    O.[Name],
	    HS.[Name],
	    WA.[Name]
		
	UNION

	 -- TRAPS
    SELECT
		t.CreatedOn AS 'CreatedOn',
	    YEAR(t.RecordedOn) AS 'RecordedOnYear',
	    T.CreatedById AS 'OwnerId',
	    U.[Name] AS 'OwnerName',
	    dbo.fn_GetAbsolutePeriodOfYear(t.RecordedOn) AS 'Period',
	    dbo.fn_GetAbsoluteWeekOfYear(t.RecordedOn) AS 'Week',
	    NULL AS 'NumberOfCatches', 
	    NULL AS 'NumberOfByCatches',
	    NULL AS 'NumberOfCatchesPreviousYear', 
	    NULL AS 'NumberOfByCatchesPreviousYear',
	    SUM(T.NumberOfTraps) AS 'NumberOfTraps',
	    CASE T.[Status] WHEN 1 THEN 'Vangend' WHEN 2 THEN 'Niet-vangend' WHEN 3 THEN 'Verwijderd' ELSE 'Onbekend' END AS 'TrapStatus',
	    TT.[Name] AS 'TrapTypeName',
	    TrT.[Name] AS 'TrappingTypeName',
	    NULL AS 'IsByCatch', 
	    NULL AS 'CatchTypeName',
		O.Id AS 'OrganizationId',
	    O.[Name] AS 'OrganizationName',
	    WA.[Name] AS 'WaterAuthorityName',
	    R.[Name] AS 'RayonName',
	    CA.[Name] AS 'CatchAreaName',
	    SA.[Name] AS 'SubAreaName',
	    P.[Name] AS 'ProvinceName',
	    HS.[Name] AS 'HourSquareName',
	    NULL AS 'FieldTestName',
	    NULL AS 'Hours', 
	    NULL AS 'HoursPreviousYear',
		SUM(T.CatchingNights) as 'CatchingNights',
		NULL as 'KmWaterway'
    FROM [Trap] AS T
        INNER JOIN [TrapType] AS TT ON T.TrapTypeId = TT.Id
		INNER JOIN [User] AS U ON T.CreatedById = U.Id
        INNER JOIN [TrappingType] AS TrT ON TT.TrappingTypeId = TrT.Id
        INNER JOIN [SubAreaHourSquare] AS SAHS ON T.SubAreaHourSquareId = SAHS.Id
        INNER JOIN [HourSquare] AS HS ON SAHS.HourSquareId = HS.Id
        INNER JOIN [SubArea] AS SA ON SAHS.SubAreaId = SA.Id
        INNER JOIN [CatchArea] AS CA ON SA.CatchAreaId = CA.Id
        INNER JOIN [Rayon] AS R ON CA.RayonId = R.Id
        INNER JOIN [Organization] AS O ON R.OrganizationId = O.Id
        INNER JOIN [WaterAuthority] AS WA ON SA.WaterAuthorityId = WA.Id
        LEFT OUTER JOIN [Province] AS P ON T.ProvinceId = P.Id
    GROUP BY
	    T.[Status],
        T.[CreatedOn],
		T.[CreatedById],
		T.[RecordedOn],
	    TT.[Name],
	    TrT.[Name],
	    SA.[Name],
	    CA.[Name],
	    R.[Name],
        O.Id,
	    O.[Name],
	    HS.[Name],
	    WA.[Name],
	    P.[Name],
		U.[Name]

    ) AS Result

	INSERT INTO [report].[OwnReportData]
           ([CreatedOn]
           ,[RecordedOnYear]
           ,[NumberOfCatches]
           ,[NumberOfByCatches]
           ,[NumberOfCatchesPreviousYear]
           ,[NumberOfByCatchesPreviousYear]
           ,[Period]
           ,[Week]
           ,[OwnerId]
           ,[OwnerName]
           ,[NumberOfTraps]
           ,[TrapStatus]
           ,[TrapTypeName]
           ,[TrappingTypeName]
           ,[IsByCatch]
           ,[CatchTypeName]
           ,[OrganizationId]
           ,[OrganizationName]
           ,[WaterAuthorityName]
           ,[RayonName]
           ,[CatchAreaName]
           ,[SubAreaName]
           ,[HourSquareName]
           ,[ProvinceName]
           ,[FieldTestName]
           ,[Hours]
           ,[HoursPreviousYear]
           ,[CatchingNights]
           ,[KmWaterway])
	SELECT 
           dateadd(week, (PERIODE - 1) * 4, datefromparts(Jaar, 1, 1)) -- this results in a random day in the correct period
           ,Jaar
           ,VANGSTEN
           ,NULL
           ,NULL
           ,NULL
           ,PERIODE
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,0
           ,NULL
           ,NULL
           ,'Onbekend'
           ,'Onbekend'
           ,'Onbekend'
           ,'Onbekend'
           ,'Onbekend'
           ,UURHOK
           ,NULL
           ,NULL
           ,UREN
           ,NULL
           ,NULL
           ,NULL
	FROM report.V1Data

    INSERT INTO [report].[OwnReportData]
           ([CreatedOn]
           ,[RecordedOnYear]
           ,[NumberOfCatches]
           ,[NumberOfByCatches]
           ,[NumberOfCatchesPreviousYear]
           ,[NumberOfByCatchesPreviousYear]
           ,[Period]
           ,[Week]
           ,[OwnerId]
           ,[OwnerName]
           ,[NumberOfTraps]
           ,[TrapStatus]
           ,[TrapTypeName]
           ,[TrappingTypeName]
           ,[IsByCatch]
           ,[CatchTypeName]
           ,[OrganizationId]
           ,[OrganizationName]
           ,[WaterAuthorityName]
           ,[RayonName]
           ,[CatchAreaName]
           ,[SubAreaName]
           ,[HourSquareName]
           ,[ProvinceName]
           ,[FieldTestName]
           ,[Hours]
           ,[HoursPreviousYear]
           ,[CatchingNights]
           ,[KmWaterway])
	SELECT
           dateadd(week, (PERIODE - 1) * 4, datefromparts(Jaar, 1, 1)) -- this results in a random day in the correct period
           ,Jaar + 1
           ,NULL
           ,NULL
           ,VANGSTEN
           ,NULL
           ,PERIODE
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,0
           ,NULL
           ,NULL
           ,'Onbekend'
           ,'Onbekend'
           ,'Onbekend'
           ,'Onbekend'
           ,'Onbekend'
           ,UURHOK
           ,NULL
           ,NULL
           ,NULL
           ,UREN
           ,NULL
           ,NULL
	FROM report.V1Data
	
    SELECT @ResultValue = COUNT(*) FROM [report].[OwnReportData]

    RETURN @ResultValue
END
GO


