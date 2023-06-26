
-----------------------------------------------------------
CREATE OR ALTER   PROCEDURE [report].[PopulateOwnReportData]
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
	    ,[HoursPreviousYear])
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
	    Result.HoursPreviousYear
    FROM (

    -- CatchData
    SELECT
	    RCD.CreatedOn AS 'CreatedOn',
	    YEAR(RCD.RecordedOn) AS 'RecordedOnYear',
	    RCD.CreatedById AS 'OwnerId',
	    U.[Name] AS 'OwnerName',
	    RCD.[Period] AS 'Period',
	    RCD.[Week] AS 'Week',
	    SUM(CASE RCD.IsByCatch WHEN 0 THEN RCD.CatchNumber ELSE 0 END) AS 'NumberOfCatches', 
	    SUM(CASE RCD.IsByCatch WHEN 1 THEN RCD.CatchNumber ELSE 0 END) AS 'NumberOfByCatches',
	    NULL AS 'NumberOfCatchesPreviousYear', 
	    NULL AS 'NumberOfByCatchesPreviousYear',
	    SUM(RCD.NumberOfTraps) AS 'NumberOfTraps',
	    CASE RCD.TrapStatus WHEN 1 THEN 'Vangend' WHEN 2 THEN 'Niet-vangend' WHEN 3 THEN 'Verwijderd' ELSE 'Onbekend' END AS 'TrapStatus',
	    TT.[Name] AS 'TrapTypeName',
	    TrT.[Name] AS 'TrappingTypeName',
	    RCD.IsByCatch AS 'IsByCatch', 
	    CT.[Name] AS 'CatchTypeName',
	    RCD.OrganizationId AS 'OrganizationId',
	    O.[Name] AS 'OrganizationName',
	    WA.[Name] AS 'WaterAuthorityName',
	    R.[Name] AS 'RayonName',
	    CA.[Name] AS 'CatchAreaName',
	    SA.[Name] AS 'SubAreaName',
	    P.[Name] AS 'ProvinceName',
	    HS.[Name] AS 'HourSquareName',
	    FT.[Name] AS 'FieldTestName',
	    NULL AS 'Hours', 
	    NULL AS 'HoursPreviousYear'
    FROM [report].CatchData AS RCD
	    INNER JOIN [User] AS U ON RCD.CreatedById = U.Id
        INNER JOIN [TrapType] AS TT ON RCD.TrapTypeId = TT.Id
        INNER JOIN [TrappingType] AS TrT ON RCD.TrappingTypeId = TrT.Id
        INNER JOIN [CatchType] AS CT ON RCD.CatchTypeId = CT.Id
        INNER JOIN [Organization] AS O ON RCD.OrganizationId = O.Id
        INNER JOIN [WaterAuthority] AS WA ON RCD.WaterAuthorityId = WA.Id
        INNER JOIN [Rayon] AS R ON RCD.RayonId = R.Id
        INNER JOIN [CatchArea] AS CA ON RCD.CatchAreaId = CA.Id
        INNER JOIN [SubArea] AS SA ON RCD.SubAreaId = SA.Id
        INNER JOIN [HourSquare] AS HS ON RCD.HourSquareId = HS.Id
        LEFT OUTER JOIN [Province] AS P ON RCD.ProvinceId = P.Id
	    LEFT OUTER JOIN FieldTest AS FT ON RCD.FieldTestId = FT.Id
    GROUP BY
        RCD.CatchId,
	    RCD.CreatedOn,
        RCD.RecordedOn,
	    RCD.[Period],
	    RCD.[Week],
        RCD.CreatedById,
	    RCD.IsByCatch, 
	    CT.[Name],
	    RCD.TrapStatus,
	    TT.[Name],
	    TrT.[Name],
	    SA.[Name],
	    CA.[Name],
	    R.[Name],
        RCD.OrganizationId,
	    O.[Name],
	    HS.[Name],
	    WA.[Name],
	    p.[Name],
	    U.[Name],
	    FT.[Name]

    UNION

    -- CatchData PREVIOUS YEAR
    SELECT
	    RCD.CreatedOn AS 'CreatedOn',
	    YEAR(RCD.RecordedOn) + 1 AS 'RecordedOnYear',
	    RCD.CreatedById AS 'OwnerId',
	    U.[Name] AS 'OwnerName',
	    RCD.[Period] AS 'Period',
	    RCD.[Week] AS 'Week',
	    NULL AS 'NumberOfCatches', 
	    NULL AS 'NumberOfByCatches',
	    SUM(CASE RCD.IsByCatch WHEN 0 THEN RCD.CatchNumber ELSE 0 END) AS 'NumberOfCatchesPreviousYear', 
	    SUM(CASE RCD.IsByCatch WHEN 1 THEN RCD.CatchNumber ELSE 0 END) AS 'NumberOfByCatchesPreviousYear',
	    SUM(RCD.NumberOfTraps) AS 'NumberOfTraps',
	    CASE RCD.TrapStatus WHEN 1 THEN 'Vangend' WHEN 2 THEN 'Niet-vangend' WHEN 3 THEN 'Verwijderd' ELSE 'Onbekend' END AS 'TrapStatus',
	    TT.[Name] AS 'TrapTypeName',
	    TrT.[Name] AS 'TrappingTypeName',
	    RCD.IsByCatch AS 'IsByCatch', 
	    CT.[Name] AS 'CatchTypeName',
	    RCD.OrganizationId AS 'OrganizationId',
	    O.[Name] AS 'OrganizationName',
	    WA.[Name] AS 'WaterAuthorityName',
	    R.[Name] AS 'RayonName',
	    CA.[Name] AS 'CatchAreaName',
	    SA.[Name] AS 'SubAreaName',
	    P.[Name] AS 'ProvinceName',
	    HS.[Name] AS 'HourSquareName',
	    FT.[Name] AS 'FieldTestName',
	    NULL AS 'Hours', 
	    NULL AS 'HoursPreviousYear'
    FROM [report].CatchData AS RCD
	    INNER JOIN [User] AS U ON RCD.CreatedById = U.Id
        INNER JOIN [TrapType] AS TT ON RCD.TrapTypeId = TT.Id
        INNER JOIN [TrappingType] AS TrT ON RCD.TrappingTypeId = TrT.Id
        INNER JOIN [CatchType] AS CT ON RCD.CatchTypeId = CT.Id
        INNER JOIN [Organization] AS O ON RCD.OrganizationId = O.Id
        INNER JOIN [WaterAuthority] AS WA ON RCD.WaterAuthorityId = WA.Id
        INNER JOIN [Rayon] AS R ON RCD.RayonId = R.Id
        INNER JOIN [CatchArea] AS CA ON RCD.CatchAreaId = CA.Id
        INNER JOIN [SubArea] AS SA ON RCD.SubAreaId = SA.Id
        INNER JOIN [HourSquare] AS HS ON RCD.HourSquareId = HS.Id
        LEFT OUTER JOIN [Province] AS P ON RCD.ProvinceId = P.Id
	    LEFT OUTER JOIN FieldTest AS FT ON RCD.FieldTestId = FT.Id
    GROUP BY
        RCD.CatchId,
	    RCD.CreatedOn,
        RCD.RecordedOn,
	    RCD.[Period],
	    RCD.[Week],
        RCD.CreatedById,
	    RCD.IsByCatch, 
	    CT.[Name],
	    RCD.TrapStatus,
	    TT.[Name],
	    TrT.[Name],
	    SA.[Name],
	    CA.[Name],
	    R.[Name],
        RCD.OrganizationId,
	    O.[Name],
	    HS.[Name],
	    WA.[Name],
	    p.[Name],
	    U.[Name],
	    FT.[Name]

    UNION

    -- TimeRegistrationData
    SELECT
	    TRD.CreatedOn AS 'CreatedOn',
	    YEAR(TRD.RecordedOn) AS 'RecordedOnYear',
	    TRD.UserId AS 'OwnerId',
	    U.[Name] AS 'OwnerName',
	    TRD.[Period] AS 'Period',
	    TRD.[Week] AS 'Week',
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
	    FT.[Name] AS 'FieldTestName',
	    SUM(TRD.[Hours]) AS 'Hours', 
	    NULL AS 'HoursPreviousYear'
    FROM report.TimeRegistrationData AS TRD
        INNER JOIN dbo.TrappingType TrT ON TrT.Id = TRD.TrappingTypeId
        INNER JOIN dbo.SubAreaHourSquare SAHS ON SAHS.Id = TRD.SubAreaHourSquareId
        INNER JOIN dbo.SubArea SA ON SA.Id = SAHS.SubAreaId
        INNER JOIN dbo.CatchArea CA ON CA.Id = SA.CatchAreaId
        INNER JOIN dbo.Rayon R ON R.Id = CA.RayonId
        INNER JOIN dbo.Organization O ON O.Id = R.OrganizationId
        INNER JOIN dbo.WaterAuthority WA ON WA.Id = SA.WaterAuthorityId
        INNER JOIN dbo.HourSquare HS ON HS.Id = SAHS.HourSquareId
	    INNER JOIN dbo.[User] AS U ON TRD.UserId = U.Id
		LEFT OUTER JOIN dbo.FieldTestHourSquare FTHS ON FTHS.HourSquareId = HS.Id
	    LEFT OUTER JOIN dbo.FieldTest AS FT ON FTHS.FieldTestId = FT.Id
    GROUP BY
        TRD.TimeRegistrationId,
	    TRD.CreatedOn,
        TRD.RecordedOn,
	    TRD.[Period],
	    TRD.[Week],
        TRD.UserId,
	    TrT.[Name],
	    SA.[Name],
	    CA.[Name],
	    R.[Name],
        O.Id,
	    O.[Name],
	    HS.[Name],
	    WA.[Name],
	    U.[Name],
	    FT.[Name]

    UNION

    -- TimeRegistrationData PREVIOUS YEAR
    SELECT
	    TRD.CreatedOn AS 'CreatedOn',
	    YEAR(TRD.RecordedOn) + 1 AS 'RecordedOnYear',
	    TRD.UserId AS 'OwnerId',
	    U.[Name] AS 'OwnerName',
	    TRD.[Period] AS 'Period',
	    TRD.[Week] AS 'Week',
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
	    FT.[Name] AS 'FieldTestName',
	    NULL AS 'Hours', 
	    SUM(TRD.[Hours]) AS 'HoursPreviousYear'
    FROM report.TimeRegistrationData AS TRD
        INNER JOIN dbo.TrappingType TrT ON TrT.Id = TRD.TrappingTypeId
        INNER JOIN dbo.SubAreaHourSquare SAHS ON SAHS.Id = TRD.SubAreaHourSquareId
        INNER JOIN dbo.SubArea SA ON SA.Id = SAHS.SubAreaId
        INNER JOIN dbo.CatchArea CA ON CA.Id = SA.CatchAreaId
        INNER JOIN dbo.Rayon R ON R.Id = CA.RayonId
        INNER JOIN dbo.Organization O ON O.Id = R.OrganizationId
        INNER JOIN dbo.WaterAuthority WA ON WA.Id = SA.WaterAuthorityId
        INNER JOIN dbo.HourSquare HS ON HS.Id = SAHS.HourSquareId
	    INNER JOIN dbo.[User] AS U ON TRD.UserId = U.Id
		LEFT OUTER JOIN dbo.FieldTestHourSquare FTHS ON FTHS.HourSquareId = HS.Id
	    LEFT OUTER JOIN dbo.FieldTest AS FT ON FTHS.FieldTestId = FT.Id
    GROUP BY
        TRD.TimeRegistrationId,
	    TRD.CreatedOn,
        TRD.RecordedOn,
	    TRD.[Period],
	    TRD.[Week],
        TRD.UserId,
	    TrT.[Name],
	    SA.[Name],
	    CA.[Name],
	    R.[Name],
        O.Id,
	    O.[Name],
	    HS.[Name],
	    WA.[Name],
	    U.[Name],
	    FT.[Name]
    ) AS Result

    SELECT @ResultValue = COUNT(*) FROM [report].[OwnReportData]

    RETURN @ResultValue
END
GO