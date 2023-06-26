---update PopulateOwnReportData sp to use new fn for calculating weeks

---populate own report data--------------------------------
-----------------------------------------------------------
CREATE OR ALTER PROCEDURE [report].[PopulateOwnReportData]
AS
    DECLARE @ResultValue int
BEGIN
    TRUNCATE TABLE [report].[OwnReportData]

    -- CATCHES from old regional layout
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
		,[KmWaterway]
		,[TimeRegistrationType]
		,[VersionRegionalLayout]
		,[HoursOther])
    SELECT
	    C.CreatedOn AS 'CreatedOn',
	    YEAR(C.RecordedOn) AS 'RecordedOnYear',
	    SUM(CASE CT.IsByCatch WHEN 0 THEN C.Number ELSE 0 END) AS 'NumberOfCatches', 
	    SUM(CASE CT.IsByCatch WHEN 1 THEN C.Number ELSE 0 END) AS 'NumberOfByCatches',
	    NULL AS 'NumberOfCatchesPreviousYear', 
	    NULL AS 'NumberOfByCatchesPreviousYear',
	    C.[Period] AS 'Period',
	    C.[Week] AS 'Week',
	    C.CreatedById AS 'OwnerId',
	    U.[Name] AS 'OwnerName',
	    NULL AS 'NumberOfTraps',
	    NULL AS 'TrapStatus',
	    TT.[Name] AS 'TrapTypeName',
	    TrT.[Name] AS 'TrappingTypeName',
	    CT.IsByCatch AS 'IsByCatch', 
	    CT.[Name] AS 'CatchTypeName',
	    O.Id AS 'OrganizationId',
	    O.[Name] AS 'OrganizationName',
	    WA.[Name] AS 'WaterAuthorityName',
	    R.[Name] AS 'RayonName',
	    CA.[Name] AS 'CatchAreaName',
	    SA.[Name] AS 'SubAreaName',
	    HS.[Name] AS 'HourSquareName',
	    P.[Name] AS 'ProvinceName',
	    STRING_AGG (FT.Name, ',') WITHIN GROUP (ORDER BY FT.Name)AS 'FieldTestName',
	    NULL AS 'Hours', 
	    NULL AS 'HoursPreviousYear',
		NULL as 'CatchingNights',
		NULL as 'KmWaterway',
		NULL as 'TimeRegistrationType',
		VRL.Name AS 'VersionRegionalLayout',
		NULL AS 'HoursOther'
    FROM [Catch] AS C
	    INNER JOIN [User] AS U ON C.CreatedById = U.Id
        INNER JOIN [Trap] AS T ON C.TrapId = T.Id
        INNER JOIN [TrapType] AS TT ON T.TrapTypeId = TT.Id
        INNER JOIN [TrappingType] AS TrT ON TT.TrappingTypeId = TrT.Id
        INNER JOIN [CatchType] AS CT ON C.CatchTypeId = CT.Id
        INNER JOIN dbo.TrapSubAreaHourSquare TSAHS on TSAHS.TrapId = t.Id
        INNER JOIN dbo.SubAreaHourSquare sahs on sahs.Id = TSAHS.SubAreaHourSquareId
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
		INNER JOIN dbo.VersionRegionalLayout VRL on VRL.Id = sahs.VersionRegionalLayoutId
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
        TT.[Name],
	    SA.[Name],
	    CA.[Name],
	    R.[Name],
        O.Id,
	    O.[Name],
	    HS.[Name],
	    WA.[Name],
	    p.[Name],
	    U.[Name],
		VRL.[Name]

	-- CATCHES from current regional layout
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
		,[KmWaterway]
		,[TimeRegistrationType]
		,[VersionRegionalLayout]
		,[HoursOther])
    SELECT
	    C.CreatedOn AS 'CreatedOn',
	    YEAR(C.RecordedOn) AS 'RecordedOnYear',
	    SUM(CASE CT.IsByCatch WHEN 0 THEN C.Number ELSE 0 END) AS 'NumberOfCatches', 
	    SUM(CASE CT.IsByCatch WHEN 1 THEN C.Number ELSE 0 END) AS 'NumberOfByCatches',
	    NULL AS 'NumberOfCatchesPreviousYear', 
	    NULL AS 'NumberOfByCatchesPreviousYear',
	    C.[Period] AS 'Period',
	    C.[Week] AS 'Week',
	    C.CreatedById AS 'OwnerId',
	    U.[Name] AS 'OwnerName',
	    NULL AS 'NumberOfTraps',
	    NULL AS 'TrapStatus',
	    TT.[Name] AS 'TrapTypeName',
	    TrT.[Name] AS 'TrappingTypeName',
	    CT.IsByCatch AS 'IsByCatch', 
	    CT.[Name] AS 'CatchTypeName',
	    O.Id AS 'OrganizationId',
	    O.[Name] AS 'OrganizationName',
	    WA.[Name] AS 'WaterAuthorityName',
	    R.[Name] AS 'RayonName',
	    CA.[Name] AS 'CatchAreaName',
	    SA.[Name] AS 'SubAreaName',
	    HS.[Name] AS 'HourSquareName',
	    P.[Name] AS 'ProvinceName',
	    STRING_AGG (FT.Name, ',') WITHIN GROUP (ORDER BY FT.Name)AS 'FieldTestName',
	    NULL AS 'Hours', 
	    NULL AS 'HoursPreviousYear',
		NULL as 'CatchingNights',
		NULL as 'KmWaterway',
		NULL as 'TimeRegistrationType',
		VRL.Name AS 'VersionRegionalLayout',
		NULL AS 'HoursOther'
    FROM [Catch] AS C
	    INNER JOIN [User] AS U ON C.CreatedById = U.Id
        INNER JOIN [Trap] AS T ON C.TrapId = T.Id
        INNER JOIN [TrapType] AS TT ON T.TrapTypeId = TT.Id
        INNER JOIN [TrappingType] AS TrT ON TT.TrappingTypeId = TrT.Id
        INNER JOIN [CatchType] AS CT ON C.CatchTypeId = CT.Id
        INNER JOIN dbo.SubAreaHourSquare sahs on sahs.Id = T.SubAreaHourSquareId
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
		INNER JOIN dbo.VersionRegionalLayout VRL on VRL.Id = sahs.VersionRegionalLayoutId
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
        TT.[Name],
	    SA.[Name],
	    CA.[Name],
	    R.[Name],
        O.Id,
	    O.[Name],
	    HS.[Name],
	    WA.[Name],
	    p.[Name],
	    U.[Name],
		VRL.[Name]

    -- CATCHES PREVIOUS YEAR from current regional layout
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
		,[KmWaterway]
		,[TimeRegistrationType]
		,[VersionRegionalLayout]
		,[HoursOther])
    SELECT
	    C.CreatedOn AS 'CreatedOn',
	    YEAR(C.RecordedOn) + 1 AS 'RecordedOnYear',
	    NULL AS 'NumberOfCatches', 
	    NULL AS 'NumberOfByCatches',
	    SUM(CASE CT.IsByCatch WHEN 0 THEN C.Number ELSE 0 END) AS 'NumberOfCatchesPreviousYear', 
	    SUM(CASE CT.IsByCatch WHEN 1 THEN C.Number ELSE 0 END) AS 'NumberOfByCatchesPreviousYear',
	    C.[Period] AS 'Period',
	    C.[Week] AS 'Week',
	    C.CreatedById AS 'OwnerId',
	    U.[Name] AS 'OwnerName',
	    NULL AS 'NumberOfTraps',
	    NULL AS 'TrapStatus',
	    TT.[Name] AS 'TrapTypeName',
	    TrT.[Name] AS 'TrappingTypeName',
	    CT.IsByCatch AS 'IsByCatch', 
	    CT.[Name] AS 'CatchTypeName',
	    O.Id AS 'OrganizationId',
	    O.[Name] AS 'OrganizationName',
	    WA.[Name] AS 'WaterAuthorityName',
	    R.[Name] AS 'RayonName',
	    CA.[Name] AS 'CatchAreaName',
	    SA.[Name] AS 'SubAreaName',
	    HS.[Name] AS 'HourSquareName',
	    P.[Name] AS 'ProvinceName',
	    NULL AS 'FieldTestName',
	    NULL AS 'Hours', 
	    NULL AS 'HoursPreviousYear',
		NULL as 'CatchingNights',
		NULL as 'KmWaterway',
		NULL as 'TimeRegistrationType',
		VRL.Name AS 'VersionRegionalLayout',
		NULL AS 'HoursOther'
    FROM [Catch] AS C
	    INNER JOIN [User] AS U ON C.CreatedById = U.Id
        INNER JOIN [Trap] AS T ON C.TrapId = T.Id
        INNER JOIN [TrapType] AS TT ON T.TrapTypeId = TT.Id
        INNER JOIN [TrappingType] AS TrT ON TT.TrappingTypeId = TrT.Id
        INNER JOIN [CatchType] AS CT ON C.CatchTypeId = CT.Id
        INNER JOIN dbo.SubAreaHourSquare sahs on sahs.Id = T.SubAreaHourSquareId
        INNER JOIN [HourSquare] AS HS ON SAHS.HourSquareId = HS.Id
        INNER JOIN [SubArea] AS SA ON SAHS.SubAreaId = SA.Id
        INNER JOIN [CatchArea] AS CA ON SA.CatchAreaId = CA.Id
        INNER JOIN [Rayon] AS R ON CA.RayonId = R.Id
        INNER JOIN [Organization] AS O ON R.OrganizationId = O.Id
        INNER JOIN [WaterAuthority] AS WA ON SA.WaterAuthorityId = WA.Id
        LEFT OUTER JOIN [Province] AS P ON T.ProvinceId = P.Id
		INNER JOIN dbo.VersionRegionalLayout VRL on VRL.Id = sahs.VersionRegionalLayoutId
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
        TT.[Name],
	    SA.[Name],
	    CA.[Name],
	    R.[Name],
        O.Id,
	    O.[Name],
	    HS.[Name],
	    WA.[Name],
	    p.[Name],
	    U.[Name],
		VRL.[Name]

 -- CATCHES PREVIOUS YEAR from old regional layout
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
		,[KmWaterway]
		,[TimeRegistrationType]
		,[VersionRegionalLayout]
		,[HoursOther])
    SELECT
	    C.CreatedOn AS 'CreatedOn',
	    YEAR(C.RecordedOn) + 1 AS 'RecordedOnYear',
	    NULL AS 'NumberOfCatches', 
	    NULL AS 'NumberOfByCatches',
	    SUM(CASE CT.IsByCatch WHEN 0 THEN C.Number ELSE 0 END) AS 'NumberOfCatchesPreviousYear', 
	    SUM(CASE CT.IsByCatch WHEN 1 THEN C.Number ELSE 0 END) AS 'NumberOfByCatchesPreviousYear',
	    C.[Period] AS 'Period',
	    C.[Week] AS 'Week',
	    C.CreatedById AS 'OwnerId',
	    U.[Name] AS 'OwnerName',
	    NULL AS 'NumberOfTraps',
	    NULL AS 'TrapStatus',
	    TT.[Name] AS 'TrapTypeName',
	    TrT.[Name] AS 'TrappingTypeName',
	    CT.IsByCatch AS 'IsByCatch', 
	    CT.[Name] AS 'CatchTypeName',
	    O.Id AS 'OrganizationId',
	    O.[Name] AS 'OrganizationName',
	    WA.[Name] AS 'WaterAuthorityName',
	    R.[Name] AS 'RayonName',
	    CA.[Name] AS 'CatchAreaName',
	    SA.[Name] AS 'SubAreaName',
	    HS.[Name] AS 'HourSquareName',
	    P.[Name] AS 'ProvinceName',
	    NULL AS 'FieldTestName',
	    NULL AS 'Hours', 
	    NULL AS 'HoursPreviousYear',
		NULL as 'CatchingNights',
		NULL as 'KmWaterway',
		NULL as 'TimeRegistrationType',
		VRL.Name AS 'VersionRegionalLayout',
		NULL AS 'HoursOther'
    FROM [Catch] AS C
	    INNER JOIN [User] AS U ON C.CreatedById = U.Id
        INNER JOIN [Trap] AS T ON C.TrapId = T.Id
        INNER JOIN [TrapType] AS TT ON T.TrapTypeId = TT.Id
        INNER JOIN [TrappingType] AS TrT ON TT.TrappingTypeId = TrT.Id
        INNER JOIN [CatchType] AS CT ON C.CatchTypeId = CT.Id
        INNER JOIN dbo.TrapSubAreaHourSquare TSAHS on TSAHS.TrapId = t.Id
        INNER JOIN dbo.SubAreaHourSquare sahs on sahs.Id = TSAHS.SubAreaHourSquareId
        INNER JOIN [HourSquare] AS HS ON SAHS.HourSquareId = HS.Id
        INNER JOIN [SubArea] AS SA ON SAHS.SubAreaId = SA.Id
        INNER JOIN [CatchArea] AS CA ON SA.CatchAreaId = CA.Id
        INNER JOIN [Rayon] AS R ON CA.RayonId = R.Id
        INNER JOIN [Organization] AS O ON R.OrganizationId = O.Id
        INNER JOIN [WaterAuthority] AS WA ON SA.WaterAuthorityId = WA.Id
        LEFT OUTER JOIN [Province] AS P ON T.ProvinceId = P.Id
		INNER JOIN dbo.VersionRegionalLayout VRL on VRL.Id = sahs.VersionRegionalLayoutId
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
        TT.[Name],
	    SA.[Name],
	    CA.[Name],
	    R.[Name],
        O.Id,
	    O.[Name],
	    HS.[Name],
	    WA.[Name],
	    p.[Name],
	    U.[Name],
		VRL.[Name]

    -- TIME REGISTRATIONS
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
		,[KmWaterway]
		,[TimeRegistrationType]
		,[VersionRegionalLayout]
		,[HoursOther])
   SELECT
	    TR.CreatedOn AS 'CreatedOn',
	    YEAR(TR.[Date]) AS 'RecordedOnYear',
	    NULL AS 'NumberOfCatches', 
	    NULL AS 'NumberOfByCatches',
	    NULL AS 'NumberOfCatchesPreviousYear', 
	    NULL AS 'NumberOfByCatchesPreviousYear',
	    TR.[Period] AS 'Period',
	    TR.[Week] AS 'Week',
	    TR.UserId AS 'OwnerId',
	    U.[Name] AS 'OwnerName',
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
	    HS.[Name] AS 'HourSquareName',
	    NULL AS 'ProvinceName',
	    STRING_AGG (FT.Name, ',') WITHIN GROUP (ORDER BY FT.Name)AS 'FieldTestName',
	    SUM(TR.[Hours]) AS 'Hours', 
	    NULL AS 'HoursPreviousYear',
		NULL as 'CatchingNights',
		NULL as 'KmWaterway',
		NULL as 'TimeRegistrationType',
		VRL.Name AS 'VersionRegionalLayout',
		NULL AS 'HoursOther'
    FROM [TimeRegistration] AS TR
	    INNER JOIN [User] AS U ON TR.UserId = U.Id
        INNER JOIN [TrappingType] TrT ON TrT.Id = TR.TrappingTypeId
        INNER JOIN [SubAreaHourSquare] SAHS ON SAHS.Id = TR.SubAreaHourSquareId
		INNER JOIN dbo.VersionRegionalLayout VRL on VRL.Id = SAHS.VersionRegionalLayoutId
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
	    U.[Name],
		VRL.[Name]

    -- TIME REGISTRATIONS PREVIOUS YEAR
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
		,[KmWaterway]
		,[TimeRegistrationType]
		,[VersionRegionalLayout]
		,[HoursOther])
    SELECT
	    TR.CreatedOn AS 'CreatedOn',
	    YEAR(TR.[Date]) + 1 AS 'RecordedOnYear',
	    NULL AS 'NumberOfCatches', 
	    NULL AS 'NumberOfByCatches',
	    NULL AS 'NumberOfCatchesPreviousYear', 
	    NULL AS 'NumberOfByCatchesPreviousYear',
	    TR.[Period] AS 'Period',
	    TR.[Week] AS 'Week',
	    TR.UserId AS 'OwnerId',
	    U.[Name] AS 'OwnerName',
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
	    HS.[Name] AS 'HourSquareName',
	    NULL AS 'ProvinceName',
	    NULL AS 'FieldTestName',
	    NULL AS 'Hours', 
	    SUM(TR.[Hours]) AS 'HoursPreviousYear',
		NULL as 'CatchingNights',
		NULL as 'KmWaterway',
		NULL as 'TimeRegistrationType',
		VRL.[Name] AS 'VersionRegionalLayout',
		NULL AS 'HoursOther'
    FROM [TimeRegistration] AS TR
	    INNER JOIN [User] AS U ON TR.UserId = U.Id
        INNER JOIN [TrappingType] TrT ON TrT.Id = TR.TrappingTypeId
        INNER JOIN [SubAreaHourSquare] SAHS ON SAHS.Id = TR.SubAreaHourSquareId
		INNER JOIN dbo.VersionRegionalLayout VRL on VRL.Id = SAHS.VersionRegionalLayoutId
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
	    U.[Name],
		VRL.[Name]

	 -- KM of Waterways
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
		,[KmWaterway]
		,[TimeRegistrationType]
		,[VersionRegionalLayout]
		,[HoursOther])
   SELECT
	    NULL AS 'CreatedOn',
	    NULL AS 'RecordedOnYear',
	    NULL AS 'NumberOfCatches', 
	    NULL AS 'NumberOfByCatches',
	    NULL AS 'NumberOfCatchesPreviousYear', 
	    NULL AS 'NumberOfByCatchesPreviousYear',
	    NULL AS 'Period',
	    NULL AS 'Week',
	    NULL AS 'OwnerId',
	    NULL AS 'OwnerName',
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
	    HS.[Name] AS 'HourSquareName',
	    NULL AS 'ProvinceName',
	    NULL AS 'FieldTestName',
	    NULL AS 'Hours', 
	    NULL AS 'HoursPreviousYear',
		NULL as 'CatchingNights',
		SAHS.KmWaterway as 'KmWaterway',
		NULL as 'TimeRegistrationType',
		NULL AS 'VersionRegionalLayout',
		NULL AS 'HoursOther'
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
		
	 -- TRAPS from old regional layout
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
		,[KmWaterway]
		,[TimeRegistrationType]
		,[VersionRegionalLayout]
		,[HoursOther])
   SELECT
		t.CreatedOn AS 'CreatedOn',
	    YEAR(t.RecordedOn) AS 'RecordedOnYear',
	    NULL AS 'NumberOfCatches', 
	    NULL AS 'NumberOfByCatches',
	    NULL AS 'NumberOfCatchesPreviousYear', 
	    NULL AS 'NumberOfByCatchesPreviousYear',
	    dbo.fn_GetAbsolutePeriodOfYear(dbo.fn_GetWeekOfYearWithCustomRule(t.RecordedOn)) AS 'Period',
	    dbo.fn_GetWeekOfYearWithCustomRule(t.RecordedOn) AS 'Week',
	    T.CreatedById AS 'OwnerId',
	    U.[Name] AS 'OwnerName',
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
	    HS.[Name] AS 'HourSquareName',
	    P.[Name] AS 'ProvinceName',
	    NULL AS 'FieldTestName',
	    NULL AS 'Hours', 
	    NULL AS 'HoursPreviousYear',
		SUM(T.CatchingNights) as 'CatchingNights',
		NULL as 'KmWaterway',
		NULL as 'TimeRegistrationType',
		VRL.Name AS 'VersionRegionalLayout',
		NULL AS 'HoursOther'
    FROM [Trap] AS T
        INNER JOIN [TrapType] AS TT ON T.TrapTypeId = TT.Id
		INNER JOIN [User] AS U ON T.CreatedById = U.Id
        INNER JOIN [TrappingType] AS TrT ON TT.TrappingTypeId = TrT.Id
        INNER JOIN dbo.TrapSubAreaHourSquare TSAHS on TSAHS.TrapId = t.Id
        INNER JOIN dbo.SubAreaHourSquare SAHS on SAHS.Id = TSAHS.SubAreaHourSquareId
        INNER JOIN [HourSquare] AS HS ON SAHS.HourSquareId = HS.Id
        INNER JOIN [SubArea] AS SA ON SAHS.SubAreaId = SA.Id
        INNER JOIN [CatchArea] AS CA ON SA.CatchAreaId = CA.Id
        INNER JOIN [Rayon] AS R ON CA.RayonId = R.Id
        INNER JOIN [Organization] AS O ON R.OrganizationId = O.Id
        INNER JOIN [WaterAuthority] AS WA ON SA.WaterAuthorityId = WA.Id
        LEFT OUTER JOIN [Province] AS P ON T.ProvinceId = P.Id
		INNER JOIN dbo.VersionRegionalLayout VRL on VRL.Id = sahs.VersionRegionalLayoutId
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
		U.[Name],
		VRL.[Name]

	 -- TRAPS from current regional layout
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
		,[KmWaterway]
		,[TimeRegistrationType]
		,[VersionRegionalLayout]
		,[HoursOther])
   SELECT
		t.CreatedOn AS 'CreatedOn',
	    YEAR(t.RecordedOn) AS 'RecordedOnYear',
	    NULL AS 'NumberOfCatches', 
	    NULL AS 'NumberOfByCatches',
	    NULL AS 'NumberOfCatchesPreviousYear', 
	    NULL AS 'NumberOfByCatchesPreviousYear',
	    dbo.fn_GetAbsolutePeriodOfYear(dbo.fn_GetWeekOfYearWithCustomRule(t.RecordedOn)) AS 'Period',
	    dbo.fn_GetWeekOfYearWithCustomRule(t.RecordedOn) AS 'Week',
	    T.CreatedById AS 'OwnerId',
	    U.[Name] AS 'OwnerName',
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
	    HS.[Name] AS 'HourSquareName',
	    P.[Name] AS 'ProvinceName',
	    NULL AS 'FieldTestName',
	    NULL AS 'Hours', 
	    NULL AS 'HoursPreviousYear',
		SUM(T.CatchingNights) as 'CatchingNights',
		NULL as 'KmWaterway',
		NULL as 'TimeRegistrationType',
		VRL.Name AS 'VersionRegionalLayout',
		NULL AS 'HoursOther'
    FROM [Trap] AS T
        INNER JOIN [TrapType] AS TT ON T.TrapTypeId = TT.Id
		INNER JOIN [User] AS U ON T.CreatedById = U.Id
        INNER JOIN [TrappingType] AS TrT ON TT.TrappingTypeId = TrT.Id
        INNER JOIN dbo.SubAreaHourSquare SAHS on SAHS.Id = T.SubAreaHourSquareId
        INNER JOIN [HourSquare] AS HS ON SAHS.HourSquareId = HS.Id
        INNER JOIN [SubArea] AS SA ON SAHS.SubAreaId = SA.Id
        INNER JOIN [CatchArea] AS CA ON SA.CatchAreaId = CA.Id
        INNER JOIN [Rayon] AS R ON CA.RayonId = R.Id
        INNER JOIN [Organization] AS O ON R.OrganizationId = O.Id
        INNER JOIN [WaterAuthority] AS WA ON SA.WaterAuthorityId = WA.Id
        LEFT OUTER JOIN [Province] AS P ON T.ProvinceId = P.Id
		INNER JOIN dbo.VersionRegionalLayout VRL on VRL.Id = sahs.VersionRegionalLayoutId
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
		U.[Name],
		VRL.[Name]

	-- TIME REGISTRATION GENERAL
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
		,[KmWaterway]
		,[TimeRegistrationType]
		,[VersionRegionalLayout]
		,[HoursOther])
	SELECT
	    TRG.CreatedOn AS 'CreatedOn',
	    YEAR(TRG.[Date]) AS 'RecordedOnYear',
	    NULL AS 'NumberOfCatches', 
	    NULL AS 'NumberOfByCatches',
	    NULL AS 'NumberOfCatchesPreviousYear', 
	    NULL AS 'NumberOfByCatchesPreviousYear',
	    TRG.[Period] AS 'Period',
	    TRG.[Week] AS 'Week',
	    TRG.UserId AS 'OwnerId',
	    U.[Name] AS 'OwnerName',
	    NULL AS 'NumberOfTraps',
	    NULL AS 'TrapStatus',
	    NULL AS 'TrapTypeName',
	    NULL AS 'TrappingTypeName',
	    NULL AS 'IsByCatch', 
	    NULL AS 'CatchTypeName',
	    ORG.ID AS 'OrganizationId',
	    ORG.[NAME] AS 'OrganizationName',
	    NULL AS 'WaterAuthorityName',
	    NULL AS 'RayonName',
	    NULL AS 'CatchAreaName',
	    NULL AS 'SubAreaName',
	    NULL AS 'HourSquareName',
	    NULL AS 'ProvinceName',
	    NULL AS 'FieldTestName',
	    NULL AS 'Hours', 
	    NULL AS 'HoursPreviousYear',
		NULL as 'CatchingNights',
		NULL as 'KmWaterway',
		TrC.[Name] as 'TimeRegistrationType',
		NULL AS 'VersionRegionalLayout',
		SUM(TRG.[Hours]) AS 'HoursOther'
    FROM [TimeRegistrationGeneral] AS TRG
	    INNER JOIN [User] AS U ON TRG.UserId = U.Id
        LEFT OUTER JOIN [Organization] AS ORG ON U.OrganizationId = ORG.Id
		INNER JOIN [TimeRegistrationCategory] TrC ON TrC.Id = TRG.TimeRegistrationCategoryId
    GROUP BY
        TRG.Id,
	    TRG.CreatedOn,
        TRG.[Date],
	    TRG.[Period],
	    TRG.[Week],
        TRG.UserId,
	    U.[Name],
		TrC.[Name],
        ORG.ID,
        ORG.[NAME]

	-- import V1 data for current year
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
           ,[KmWaterway]
		   ,[TimeRegistrationType]
		   ,[VersionRegionalLayout]
		   ,[HoursOther])
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
		   ,NULL
		   ,NULL
		   ,NULL
	FROM report.V1Data

	-- import V1 data for previous year
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
           ,[KmWaterway]
		   ,[TimeRegistrationType]
		   ,[VersionRegionalLayout]
		   ,[HoursOther])
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
		   ,NULL
		   ,NULL
		   ,NULL
	FROM report.V1Data
	
    Update STATISTICS report.[OwnReportData] WITH FULLSCAN

    SELECT @ResultValue = COUNT(0) FROM [report].[OwnReportData]

    RETURN @ResultValue
END
GO