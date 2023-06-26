DROP INDEX IF EXISTS 
    [IX_OwnReport_RecordedOnYear] ON [report].[OwnReportData],
    [IX_OwnReport_Owner] ON [report].[OwnReportData],
    [IX_OwnReport_Organization] ON [report].[OwnReportData],
    [IX_OwnReport_WaterAuthorityName] ON [report].[OwnReportData],
    [IX_OwnReport_RayonName] ON [report].[OwnReportData],
    [IX_OwnReport_SubAreaName] ON [report].[OwnReportData],
    [IX_OwnReport_HourSquareName] ON [report].[OwnReportData],
    [IX_OwnReport_CatchAreaName] ON [report].[OwnReportData]
GO

CREATE NONCLUSTERED INDEX [IX_OwnReport_RecordedOnYear] ON [report].[OwnReportData]
(
	[RecordedOnYear] ASC
)

CREATE NONCLUSTERED INDEX [IX_OwnReport_Owner] ON [report].[OwnReportData]
(
	[OwnerId] ASC,
	[OwnerName] ASC
)

CREATE NONCLUSTERED INDEX [IX_OwnReport_Organization] ON [report].[OwnReportData]
(
	[OrganizationId] ASC,
	[OrganizationName] ASC
)

CREATE NONCLUSTERED INDEX [IX_OwnReport_WaterAuthorityName] ON [report].[OwnReportData]
(
	[WaterAuthorityName] ASC
)

CREATE NONCLUSTERED INDEX [IX_OwnReport_RayonName] ON [report].[OwnReportData]
(
	[RayonName] ASC
)

CREATE NONCLUSTERED INDEX [IX_OwnReport_CatchAreaName] ON [report].[OwnReportData]
(
	[CatchAreaName] ASC
)

CREATE NONCLUSTERED INDEX [IX_OwnReport_SubAreaName] ON [report].[OwnReportData]
(
	[SubAreaName] ASC
)

CREATE NONCLUSTERED INDEX [IX_OwnReport_HourSquareName] ON [report].[OwnReportData]
(
	[HourSquareName] ASC
)
GO


-----------------------------------------------------------

CREATE OR ALTER PROCEDURE [report].[PopulateOwnReportData]
AS
    DECLARE @ResultValue int
BEGIN
    TRUNCATE TABLE [report].[OwnReportData]

    INSERT INTO [report].[OwnReportData]
	    ([CreatedOn]
	    ,[RecordedOn]
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
	    Result.RecordedOn,
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
	    RCD.RecordedOn AS 'RecordedOn',
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
	    RCD.TrapTypeName AS 'TrapTypeName',
	    RCD.TrappingTypeName AS 'TrappingTypeName',
	    RCD.IsByCatch AS 'IsByCatch', 
	    RCD.CatchTypeName AS 'CatchTypeName',
	    RCD.OrganizationId AS 'OrganizationId',
	    RCD.OrganizationName AS 'OrganizationName',
	    RCD.WaterAuthorityName AS 'WaterAuthorityName',
	    RCD.RayonName AS 'RayonName',
	    RCD.CatchAreaName AS 'CatchAreaName',
	    RCD.SubAreaName AS 'SubAreaName',
	    RCD.ProvinceName AS 'ProvinceName',
	    RCD.HourSquareName AS 'HourSquareName',
	    FT.[Name] AS 'FieldTestName',
	    NULL AS 'Hours', 
	    NULL AS 'HoursPreviousYear', 
	    NULL AS 'KmOfWaterways'
    FROM [report].CatchData AS RCD
	    INNER JOIN [User] AS U ON RCD.CreatedById = U.Id
	    LEFT OUTER JOIN FieldTest AS FT ON RCD.FieldTestId = FT.Id
    GROUP BY
        RCD.CatchId,
	    RCD.CreatedOn,
	    RCD.RecordedOn,
	    RCD.[Period],
	    RCD.[Week],
        RCD.CreatedById,
	    RCD.IsByCatch, 
	    RCD.CatchTypeName,
	    RCD.TrapStatus,
	    RCD.TrapTypeName,
	    RCD.TrappingTypeName,
	    RCD.SubAreaName,
	    RCD.CatchAreaName,
	    RCD.RayonName,
        RCD.OrganizationId,
	    RCD.OrganizationName,
	    RCD.HourSquareName,
	    RCD.WaterAuthorityName,
	    RCD.ProvinceName,
	    U.[Name],
	    FT.[Name]

    UNION

    -- CatchData PREVIOUS YEAR
    SELECT
	    RCD.CreatedOn AS 'CreatedOn',
	    RCD.RecordedOn AS 'RecordedOn',
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
	    RCD.TrapTypeName AS 'TrapTypeName',
	    RCD.TrappingTypeName AS 'TrappingTypeName',
	    RCD.IsByCatch AS 'IsByCatch', 
	    RCD.CatchTypeName AS 'CatchTypeName',
	    RCD.OrganizationId AS 'OrganizationId',
	    RCD.OrganizationName AS 'OrganizationName',
	    RCD.WaterAuthorityName AS 'WaterAuthorityName',
	    RCD.RayonName AS 'RayonName',
	    RCD.CatchAreaName AS 'CatchAreaName',
	    RCD.SubAreaName AS 'SubAreaName',
	    RCD.ProvinceName AS 'ProvinceName',
	    RCD.HourSquareName AS 'HourSquareName',
	    FT.[Name] AS 'FieldTestName',
	    NULL AS 'Hours', 
	    NULL AS 'HoursPreviousYear', 
	    NULL AS 'KmOfWaterways'
    FROM [report].CatchData AS RCD
	    INNER JOIN [User] AS U ON RCD.CreatedById = U.Id
	    LEFT OUTER JOIN FieldTest AS FT ON RCD.FieldTestId = FT.Id
    GROUP BY
        RCD.CatchId,
	    RCD.CreatedOn,
	    RCD.RecordedOn,
	    RCD.[Period],
	    RCD.[Week],
        RCD.CreatedById,
	    RCD.IsByCatch, 
	    RCD.CatchTypeName,
	    RCD.TrapStatus,
	    RCD.TrapTypeName,
	    RCD.TrappingTypeName,
	    RCD.SubAreaName,
	    RCD.CatchAreaName,
	    RCD.RayonName,
        RCD.OrganizationId,
	    RCD.OrganizationName,
	    RCD.HourSquareName,
	    RCD.WaterAuthorityName,
	    RCD.ProvinceName,
	    U.[Name],
	    FT.[Name]

    UNION

    -- TimeRegistration
    SELECT
	    TR.CreatedOn AS 'CreatedOn',
	    TR.[Date] AS 'RecordedOn',
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
	    FT.[Name] AS 'FieldTestName',
	    SUM(TR.[Hours]) AS 'Hours', 
	    NULL AS 'HoursPreviousYear', 
	    NULL AS 'KmOfWaterways'
    FROM dbo.TimeRegistration AS TR
        JOIN dbo.TrappingType TrT ON TrT.Id = TR.TrappingTypeId
        JOIN dbo.SubAreaHourSquare SAHS ON SAHS.Id = TR.SubAreaHourSquareId
        JOIN dbo.SubArea SA ON SA.Id = SAHS.SubAreaId
        JOIN dbo.CatchArea CA ON CA.Id = SA.CatchAreaId
        JOIN dbo.Rayon R ON R.Id = CA.RayonId
        JOIN dbo.Organization O ON O.Id = R.OrganizationId
        JOIN dbo.WaterAuthority WA ON WA.Id = SA.WaterAuthorityId
        JOIN dbo.HourSquare HS ON HS.Id = SAHS.HourSquareId
	    JOIN dbo.[User] AS U ON TR.UserId = U.Id
		LEFT OUTER JOIN dbo.FieldTestHourSquare FTHS ON FTHS.HourSquareId = HS.Id
	    LEFT OUTER JOIN dbo.FieldTest AS FT ON FTHS.FieldTestId = FT.Id
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
	    FT.[Name]

    UNION

    -- TimeRegistration PREVIOUS YEAR
    SELECT
	    TR.CreatedOn AS 'CreatedOn',
	    TR.[Date] AS 'RecordedOn',
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
	    FT.[Name] AS 'FieldTestName',
	    NULL AS 'Hours', 
	    SUM(TR.[Hours]) AS 'HoursPreviousYear', 
	    NULL AS 'KmOfWaterways'
    FROM dbo.TimeRegistration AS TR
        JOIN dbo.TrappingType TrT ON TrT.Id = TR.TrappingTypeId
        JOIN dbo.SubAreaHourSquare SAHS ON SAHS.Id = TR.SubAreaHourSquareId
        JOIN dbo.SubArea SA ON SA.Id = SAHS.SubAreaId
        JOIN dbo.CatchArea CA ON CA.Id = SA.CatchAreaId
        JOIN dbo.Rayon R ON R.Id = CA.RayonId
        JOIN dbo.Organization O ON O.Id = R.OrganizationId
        JOIN dbo.WaterAuthority WA ON WA.Id = SA.WaterAuthorityId
        JOIN dbo.HourSquare HS ON HS.Id = SAHS.HourSquareId
	    JOIN dbo.[User] AS U ON TR.UserId = U.Id
		LEFT OUTER JOIN dbo.FieldTestHourSquare FTHS ON FTHS.HourSquareId = HS.Id
	    LEFT OUTER JOIN dbo.FieldTest AS FT ON FTHS.FieldTestId = FT.Id
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
	    FT.[Name]
    ) AS Result

    SELECT @ResultValue = COUNT(*) FROM [report].[OwnReportData]

    RETURN @ResultValue
END
GO