-----------------------------------------------------------
CREATE OR ALTER   PROCEDURE [report].[PopulateTimeRegistrationData]
AS
    DECLARE @ResultValue int
BEGIN
    TRUNCATE TABLE [report].[TimeRegistrationData]

    INSERT INTO [report].[TimeRegistrationData] (
        [TimeRegistrationId], 
        [RecordedOn], 
        [Hours], 
        [Period], 
        [Week], 
        [Year], 
        [PeriodValue],
        [TrappingTypeId],
        [SubAreaHourSquareId],
        [SubAreaId],
        [CatchAreaId],
        [RayonId],
        [OrganizationId],
        [WaterAuthorityId]
    )
    SELECT
        tr.Id as TimeRegistrationId, 
        tr.[Date], 
        tr.[Hours], 
        tr.[Period], 
        tr.[Week], 
        tr.[Year],
        dbo.fn_GetPeriodValue(tr.[Year], tr.[Period], 0) as PeriodValue,
        trpt.Id as TrappingTypeId,
        sahs.Id as SubAreaHourSquareId,
        sa.Id as SubAreaId,
        ca.Id as CatchAreaId,
        r.Id as RayonId,
        r.OrganizationId,
        wa.Id as WaterAuthorityId
    FROM dbo.TimeRegistration tr
        INNER JOIN dbo.TrappingType trpt on trpt.Id = tr.TrappingTypeId
        INNER JOIN dbo.SubAreaHourSquare sahs on sahs.Id = tr.SubAreaHourSquareId
        INNER JOIN dbo.SubArea sa on sa.Id = sahs.SubAreaId
        INNER JOIN dbo.CatchArea ca on ca.Id = sa.CatchAreaId
        INNER JOIN dbo.Rayon r on r.Id = ca.RayonId
        INNER JOIN dbo.WaterAuthority wa on wa.Id = sa.WaterAuthorityId

    SELECT @ResultValue = COUNT(*) FROM [report].[TimeRegistrationData]

    RETURN @ResultValue
END
GO

-----------------------------------------------------------
CREATE OR ALTER   PROCEDURE [report].[PopulateCatchData]
AS
    DECLARE @ResultValue int
BEGIN
    TRUNCATE TABLE [report].[CatchData]

    INSERT INTO [report].[CatchData] (
        [CatchId], 
        [CatchNumber], 
        [RecordedOn], 
        [Period], 
        [Week], 
        [Year],
        [PeriodValue], 
        [CatchStatus],
        [TrapId],
        [NumberOfTraps],
        [CatchTypeId],
        [IsByCatch],
        [TrapTypeId],
        [TrappingTypeId],
        [SubAreaHourSquareId],
        [SubAreaId],
        [CatchAreaId],
        [RayonId],
        [OrganizationId],
        [WaterAuthorityId]
    )
    SELECT
        c.Id as CatchId, 
        c.Number as CatchNumber, 
        c.RecordedOn, 
        c.[Period], 
        c.[Week], 
        c.[Year],
        dbo.fn_GetPeriodValue(c.[Year], c.[Period], 0) as PeriodValue, 
        c.[Status] as CatchStatus,
        t.Id as TrapId, 
        t.NumberOfTraps,
        ct.Id as CatchTypeId, 
        ct.IsByCatch,
        tt.Id as TrapTypeId,
        trpt.Id as TrappingTypeId,
        sahs.Id as SubAreaHourSquareId,
        sa.Id as SubAreaId,
        ca.Id as CatchAreaId,
        r.Id as RayonId,
        r.OrganizationId,
        wa.Id as WaterAuthorityId
    FROM dbo.[Catch] c
        INNER JOIN dbo.Trap t on t.Id = c.TrapId
        INNER JOIN dbo.TrapType tt on tt.id = t.TrapTypeId
        INNER JOIN dbo.TrappingType trpt on trpt.Id = tt.TrappingTypeId
        INNER JOIN dbo.CatchType ct on c.CatchTypeId = ct.Id
        INNER JOIN dbo.SubAreaHourSquare sahs on sahs.Id = t.SubAreaHourSquareId
        INNER JOIN dbo.SubArea sa on sa.Id = sahs.SubAreaId
        INNER JOIN dbo.CatchArea ca on ca.Id = sa.CatchAreaId
        INNER JOIN dbo.Rayon r on r.Id = ca.RayonId
        INNER JOIN dbo.WaterAuthority wa on wa.Id = sa.WaterAuthorityId

    SELECT @ResultValue = COUNT(*) FROM [report].[CatchData]

    RETURN @ResultValue
END
GO

-----------------------------------------------------------------------------
------- Populate Own report data -----------------

-----------------------------------------------------------
CREATE OR ALTER PROCEDURE [report].[PopulateOwnReportData]
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
		,[KmWaterway]
		,[TimeRegistrationType])
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
		Result.KmWaterway,
		Result.TimeRegistrationType
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
		NULL as 'KmWaterway',
		NULL as 'TimeRegistrationType'
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
		NULL as 'KmWaterway',
		NULL as 'TimeRegistrationType'
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
		NULL as 'KmWaterway',
		'Velduren' as 'TimeRegistrationType'
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
		NULL as 'KmWaterway',
		'Velduren' as 'TimeRegistrationType'
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
		SAHS.KmWaterway as 'KmWaterway',
		NULL as 'TimeRegistrationType'
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
		NULL as 'KmWaterway',
		NULL as 'TimeRegistrationType'
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

	UNION

	-- TIME REGISTRATION GENERAL
	SELECT
	    TRG.CreatedOn AS 'CreatedOn',
	    YEAR(TRG.[Date]) AS 'RecordedOnYear',
	    TRG.UserId AS 'OwnerId',
	    U.[Name] AS 'OwnerName',
	    TRG.[Period] AS 'Period',
	    TRG.[Week] AS 'Week',
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
	    NULL AS 'OrganizationId',
	    NULL AS 'OrganizationName',
	    NULL AS 'WaterAuthorityName',
	    NULL AS 'RayonName',
	    NULL AS 'CatchAreaName',
	    NULL AS 'SubAreaName',
	    NULL AS 'ProvinceName',
	    NULL AS 'HourSquareName',
	    NULL AS 'FieldTestName',
	    SUM(TRG.[Hours]) AS 'Hours', 
	    NULL AS 'HoursPreviousYear',
		NULL as 'CatchingNights',
		NULL as 'KmWaterway',
		TrC.[Name] as 'TimeRegistrationType'
    FROM [TimeRegistrationGeneral] AS TRG
	    INNER JOIN [User] AS U ON TRG.UserId = U.Id
		INNER JOIN [TimeRegistrationCategory] TrC ON TrC.Id = TRG.TimeRegistrationCategoryId
    GROUP BY
        TRG.Id,
	    TRG.CreatedOn,
        TRG.[Date],
	    TRG.[Period],
	    TRG.[Week],
        TRG.UserId,
	    U.[Name],
		TrC.[Name]
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
           ,[KmWaterway]
			,[TimeRegistrationType])
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
           ,[KmWaterway]
			,[TimeRegistrationType])
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
	FROM report.V1Data
	
    SELECT @ResultValue = COUNT(*) FROM [report].[OwnReportData]

    RETURN @ResultValue
END
GO



------- Map reports functions-------------------------------------------------
--CatchAreaCatches

CREATE OR ALTER   FUNCTION [dbo].[fn_MapCatchesReport_CatchArea]
(	
	@measurement int,
	@trappingType varchar(36),
	@yearFrom int,
	@periodFrom int,
	@yearTo int,
	@periodTo int,
	@versionRegionalLayout varchar(36)
)
RETURNS TABLE 
AS
RETURN 
(
    SELECT
        ca.Id as RegionId,
        ca.Geometry as Geometry,
        CASE WHEN @measurement in (0,1) THEN COALESCE(Result.CatchNumber, 0) ELSE null END AS CatchNumber,
        CASE WHEN @measurement in (2,3) THEN ROUND(COALESCE(CAST(Result.CatchNumber AS float)/CAST(TotalKm.Km AS float),0),2) ELSE null END AS CatchesPerKM,
        CASE WHEN @measurement = 4 THEN ROUND(COALESCE(CAST(Hours.Hours AS float)/CAST(TotalKm.Km AS float),0), 2) ELSE null END AS HoursPerKM
    FROM dbo.CatchArea ca
        LEFT JOIN (
            SELECT SUM(sahs.KmWaterWay) as Km, sa.CatchAreaId
            FROM dbo.SubAreaHourSquare sahs
                JOIN dbo.SubArea sa on sa.id = sahs.SubAreaId
                JOIN dbo.CatchArea ca on ca.Id = sa.CatchAreaId
            WHERE sahs.VersionRegionalLayoutId = @versionRegionalLayout
            GROUP BY CatchAreaId
        ) as TotalKm ON TotalKm.CatchAreaId = ca.Id
        LEFT JOIN (
            SELECT cd.CatchAreaId as CatchAreaId, SUM(cd.CatchNumber) AS CatchNumber
            FROM [report].[CatchData] cd
                JOIN CatchArea ca on ca.Id = cd.CatchAreaId
            WHERE 
                @measurement != 4
                AND cd.IsByCatch = (CASE WHEN @measurement in (1,3) THEN 1 Else 0 END)
                AND (@trappingType IS NULL OR @trappingType = '0' OR cd.TrappingTypeId = @trappingType)
                AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= cd.PeriodValue
                AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= cd.PeriodValue
				--AND cd.VersionRegionalLayoutId = @versionRegionalLayout 
            GROUP BY CatchAreaId
        ) AS Result ON Result.CatchAreaId = ca.Id
        LEFT JOIN (
            SELECT td.CatchAreaId as CatchAreaId, SUM(td.Hours) AS Hours
            FROM [report].[TimeRegistrationData] td
                JOIN CatchArea ca on ca.Id = td.CatchAreaId
            WHERE @measurement = 4
                AND (@trappingType IS NULL OR @trappingType = '0' OR td.TrappingTypeId = @trappingType)
                AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= td.PeriodValue
                AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= td.PeriodValue
                --AND td.VersionRegionalLayoutId = @versionRegionalLayout 
            GROUP BY CatchAreaId
        ) AS Hours ON Hours.CatchAreaId = ca.Id 
    WHERE TotalKm.Km > (CASE WHEN @measurement in (2,3,4) THEN 0 Else -1 END)
)
GO

-----------------------------------
---- Rayon Catches

CREATE   FUNCTION [dbo].[fn_MapCatchesReport_Rayon]
(	
	@measurement int,
	@trappingType varchar(36),
	@yearFrom int,
	@periodFrom int,
	@yearTo int,
	@periodTo int,
	@versionRegionalLayout varchar(36)
)
RETURNS TABLE 
AS
RETURN 
(
    SELECT
        r.Id as RegionId,
        r.Geometry as Geometry,
        CASE WHEN @measurement in (0,1) THEN COALESCE(Result.CatchNumber, 0) ELSE null END AS CatchNumber,
        CASE WHEN @measurement in (2,3) THEN ROUND(COALESCE(CAST(Result.CatchNumber AS float)/CAST(TotalKm.Km AS float),0),2) ELSE null END AS CatchesPerKM,
        CASE WHEN @measurement = 4 THEN ROUND(COALESCE(CAST(Hours.Hours AS float)/CAST(TotalKm.Km AS float),0), 2) ELSE null END AS HoursPerKM
    FROM dbo.Rayon r
        LEFT JOIN (
            SELECT SUM(sahs.KmWaterWay) as Km, ca.RayonId
            FROM dbo.SubAreaHourSquare sahs
                JOIN dbo.SubArea sa on sa.id = sahs.SubAreaId
		        JOIN dbo.CatchArea ca on ca.Id = sa.CatchAreaId
            WHERE sahs.VersionRegionalLayoutId = @versionRegionalLayout
            GROUP BY RayonId
        ) as TotalKm ON TotalKm.RayonId = r.Id
        LEFT JOIN (
            SELECT cd.RayonId as RayonId, SUM(cd.CatchNumber) AS CatchNumber
            FROM [report].[CatchData] cd
                JOIN dbo.Rayon r on r.Id = cd.RayonId
            WHERE 
                @measurement != 4
                AND cd.IsByCatch = (CASE WHEN @measurement in (1,3) THEN 1 Else 0 END)
                AND (@trappingType IS NULL OR @trappingType = '0' OR cd.TrappingTypeId = @trappingType)
                AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= cd.PeriodValue
                AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= cd.PeriodValue
            --WHERE cd.VersionRegionalLayoutId = @versionRegionalLayout  TODO WVR-1293
            GROUP BY RayonId
        ) AS Result ON Result.RayonId = r.Id
        LEFT JOIN (
            SELECT td.RayonId as RayonId, SUM(td.Hours) AS Hours
            FROM [report].[TimeRegistrationData] td
                JOIN dbo.Rayon r on r.Id = td.RayonId
            WHERE @measurement = 4
                AND (@trappingType IS NULL OR @trappingType = '0' OR td.TrappingTypeId = @trappingType)
                AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= td.PeriodValue
                AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= td.PeriodValue
                --AND td.VersionRegionalLayoutId = @versionRegionalLayout  TODO WVR-1293
            GROUP BY RayonId
        ) AS Hours ON Hours.RayonId = r.Id 
    WHERE TotalKm.Km > (CASE WHEN @measurement in (2,3,4) THEN 0 Else -1 END)
)
GO

-------------------------------------------
-------- SubArea Catches

CREATE OR ALTER   FUNCTION [dbo].[fn_MapCatchesReport_SubArea]
(	
	@measurement int,
	@trappingType varchar(36),
	@yearFrom int,
	@periodFrom int,
	@yearTo int,
	@periodTo int,
	@versionRegionalLayout varchar(36)
)
RETURNS TABLE 
AS
RETURN 
(
    SELECT
        sa.Id as RegionId,
        sa.Geometry as Geometry,
        CASE WHEN @measurement in (0,1) THEN COALESCE(Result.CatchNumber, 0) ELSE null END AS CatchNumber,
        CASE WHEN @measurement in (2,3) THEN ROUND(COALESCE(CAST(Result.CatchNumber AS float)/CAST(TotalKm.Km AS float),0),2) ELSE null END AS CatchesPerKM,
        CASE WHEN @measurement = 4 THEN ROUND(COALESCE(CAST(Hours.Hours AS float)/CAST(TotalKm.Km AS float),0), 2) ELSE null END AS HoursPerKM
    FROM dbo.SubArea sa
        LEFT JOIN (
            SELECT SUM(sahs.KmWaterWay) as Km, sahs.SubAreaId
            FROM dbo.SubAreaHourSquare sahs
            WHERE sahs.VersionRegionalLayoutId = @versionRegionalLayout
            GROUP BY SubAreaId
        ) as TotalKm ON TotalKm.SubAreaId = sa.Id
        LEFT JOIN (
            SELECT cd.SubAreaId as SubAreaId, SUM(cd.CatchNumber) AS CatchNumber
            FROM [report].[CatchData] cd
                JOIN dbo.SubArea sa on sa.Id = cd.SubAreaId
            WHERE 
                @measurement != 4
                AND cd.IsByCatch = (CASE WHEN @measurement in (1,3) THEN 1 Else 0 END)
                AND (@trappingType IS NULL OR @trappingType = '0' OR cd.TrappingTypeId = @trappingType)
                AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= cd.PeriodValue
                AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= cd.PeriodValue
            --WHERE cd.VersionRegionalLayoutId = @versionRegionalLayout  TODO WVR-1293
            GROUP BY SubAreaId
        ) AS Result ON Result.SubAreaId = sa.Id
        LEFT JOIN (
            SELECT td.SubAreaId as SubAreaId, SUM(td.Hours) AS Hours
            FROM [report].[TimeRegistrationData] td
                JOIN dbo.SubArea sa on sa.Id = td.SubAreaId
            WHERE @measurement = 4
                AND (@trappingType IS NULL OR @trappingType = '0' OR td.TrappingTypeId = @trappingType)
                AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= td.PeriodValue
                AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= td.PeriodValue
                --AND td.VersionRegionalLayoutId = @versionRegionalLayout  TODO WVR-1293
            GROUP BY SubAreaId
        ) AS Hours ON Hours.SubAreaId = sa.Id 
    WHERE TotalKm.Km > (CASE WHEN @measurement in (2,3,4) THEN 0 Else -1 END)
)
GO

----------------------------------------------------
-------SubAreaHourSquare Catches
CREATE OR ALTER   FUNCTION [dbo].[fn_MapCatchesReport_SubAreaHourSquare]
(	
	@measurement int,
	@trappingType varchar(36),
	@yearFrom int,
	@periodFrom int,
	@yearTo int,
	@periodTo int,
	@versionRegionalLayout varchar(36)
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT
        sahs.Id as RegionId,
        sahs.Geometry as Geometry,
        CASE WHEN @measurement in (0,1) THEN COALESCE(Result.CatchNumber, 0) ELSE null END AS CatchNumber,
        CASE WHEN @measurement in (2,3) THEN ROUND(COALESCE(CAST(Result.CatchNumber AS float)/CAST(sahs.KmWaterway AS float),0),2) ELSE null END AS CatchesPerKM,
        CASE WHEN @measurement = 4 THEN ROUND(COALESCE(CAST(Hours.Hours AS float)/CAST(sahs.KmWaterway AS float),0), 2) ELSE null END AS HoursPerKM
    FROM dbo.SubAreaHourSquare sahs
        LEFT JOIN (
            SELECT cd.SubAreaHourSquareId as SubAreaHourSquareId, SUM(cd.CatchNumber) AS CatchNumber
            FROM [report].[CatchData] cd
                JOIN dbo.SubAreaHourSquare sahs on sahs.Id = cd.SubAreaHourSquareId
            WHERE 
                @measurement != 4
                AND cd.IsByCatch = (CASE WHEN @measurement in (1,3) THEN 1 Else 0 END)
                AND (@trappingType IS NULL OR @trappingType = '0' OR cd.TrappingTypeId = @trappingType)
                AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= cd.PeriodValue
                AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= cd.PeriodValue
            --WHERE cd.VersionRegionalLayoutId = @versionRegionalLayout  TODO WVR-1293
            GROUP BY SubAreaHourSquareId
        ) AS Result ON Result.SubAreaHourSquareId = sahs.Id
        LEFT JOIN (
            SELECT td.SubAreaHourSquareId as SubAreaHourSquareId, SUM(td.Hours) AS Hours
            FROM [report].[TimeRegistrationData] td
                JOIN dbo.SubAreaHourSquare sahs on sahs.Id = td.SubAreaHourSquareId
            WHERE @measurement = 4
                AND (@trappingType IS NULL OR @trappingType = '0' OR td.TrappingTypeId = @trappingType)
                AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= td.PeriodValue
                AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= td.PeriodValue
                --AND td.VersionRegionalLayoutId = @versionRegionalLayout  TODO WVR-1293
            GROUP BY SubAreaHourSquareId
        ) AS Hours ON Hours.SubAreaHourSquareId = sahs.Id  
    WHERE 
        sahs.KmWaterway > (CASE WHEN @measurement in (2,3,4) THEN 0 Else -1 END)
        AND sahs.VersionRegionalLayoutId = @versionRegionalLayout
)
GO

--------------------------------------------------
----- WaterAuthority Catches

CREATE OR ALTER   FUNCTION [dbo].[fn_MapCatchesReport_WaterAuthority]
(	
	@measurement int,
	@trappingType varchar(36),
	@yearFrom int,
	@periodFrom int,
	@yearTo int,
	@periodTo int,
	@versionRegionalLayout varchar(36)
)
RETURNS TABLE 
AS
RETURN 
(
    SELECT
        wa.Id as RegionId,
        wa.Geometry as Geometry,
        CASE WHEN @measurement in (0,1) THEN COALESCE(Result.CatchNumber, 0) ELSE null END AS CatchNumber,
        CASE WHEN @measurement in (2,3) THEN ROUND(COALESCE(CAST(Result.CatchNumber AS float)/CAST(TotalKm.Km AS float),0),2) ELSE null END AS CatchesPerKM,
        CASE WHEN @measurement = 4 THEN ROUND(COALESCE(CAST(Hours.Hours AS float)/CAST(TotalKm.Km AS float),0), 2) ELSE null END AS HoursPerKM
    FROM dbo.WaterAuthority wa 
        LEFT JOIN (
            SELECT SUM(sahs.KmWaterWay) as Km, sa.WaterAuthorityId
            FROM dbo.SubAreaHourSquare sahs
                JOIN dbo.SubArea sa on sa.id = sahs.SubAreaId
            WHERE sahs.VersionRegionalLayoutId = @versionRegionalLayout
            GROUP BY WaterAuthorityId
        ) as TotalKm ON TotalKm.WaterAuthorityId = wa.Id
        LEFT JOIN (
            SELECT cd.WaterAuthorityId as WaterAuthorityId, SUM(cd.CatchNumber) AS CatchNumber
            FROM [report].[CatchData] cd
                JOIN dbo.WaterAuthority wa on wa.Id = cd.WaterAuthorityId
            WHERE 
                @measurement != 4
                AND cd.IsByCatch = (CASE WHEN @measurement in (1,3) THEN 1 Else 0 END)
                AND (@trappingType IS NULL OR @trappingType = '0' OR cd.TrappingTypeId = @trappingType)
                AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= cd.PeriodValue
                AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= cd.PeriodValue
            --WHERE cd.VersionRegionalLayoutId = @versionRegionalLayout  TODO WVR-1293
            GROUP BY WaterAuthorityId
        ) AS Result ON Result.WaterAuthorityId = wa.Id
        LEFT JOIN (
            SELECT td.WaterAuthorityId as WaterAuthorityId, SUM(td.Hours) AS Hours
            FROM [report].[TimeRegistrationData] td
                JOIN dbo.WaterAuthority wa on wa.Id = td.WaterAuthorityId
            WHERE @measurement = 4
                AND (@trappingType IS NULL OR @trappingType = '0' OR td.TrappingTypeId = @trappingType)
                AND dbo.fn_GetPeriodValue(@yearFrom, @periodFrom, 0) <= td.PeriodValue
                AND dbo.fn_GetPeriodValue(@yearTo, @periodTo, 1) >= td.PeriodValue
                --AND td.VersionRegionalLayoutId = @versionRegionalLayout  TODO WVR-1293
            GROUP BY WaterAuthorityId
        ) AS Hours ON Hours.WaterAuthorityId = wa.Id 
    WHERE TotalKm.Km > (CASE WHEN @measurement in (2,3,4) THEN 0 Else -1 END)
)
GO
-------------------------------------------------
-- HourSquare catches

DROP FUNCTION [dbo].[fn_MapCatchesReport_HourSquare]
GO
