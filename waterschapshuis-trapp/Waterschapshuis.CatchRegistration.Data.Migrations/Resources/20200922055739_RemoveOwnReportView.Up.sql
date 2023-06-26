IF OBJECT_ID('[report].[vw_OwnReport]', 'V') IS NOT NULL 
  DROP VIEW [report].[vw_OwnReport]
GO

-----------------------------------------------------------
IF OBJECT_ID('report.CatchData', 'U') IS NOT NULL 
  DROP TABLE report.CatchData;
GO

CREATE TABLE [report].[CatchData](
	[CatchId] [uniqueidentifier] NOT NULL,
	[CatchNumber] [int] NOT NULL,
	[CreatedById] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[RecordedOn] [datetimeoffset](7) NOT NULL,
	[Period] [int] NOT NULL,
	[Week] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[PeriodValue] [int] NOT NULL,
	[CatchStatus] [tinyint] NOT NULL,
	[TrapId] [uniqueidentifier] NOT NULL,
	[NumberOfTraps] [int] NOT NULL,
	[TrapStatus] [tinyint] NOT NULL,
	[CatchTypeId] [uniqueidentifier] NOT NULL,
	[IsByCatch] [bit] NOT NULL,
	[TrapTypeId] [uniqueidentifier] NOT NULL,
	[TrappingTypeId] [uniqueidentifier] NOT NULL,
	[SubAreaHourSquareId] [uniqueidentifier] NOT NULL,
	[SubAreaId] [uniqueidentifier] NOT NULL,
	[CatchAreaId] [uniqueidentifier] NOT NULL,
	[RayonId] [uniqueidentifier] NOT NULL,
	[OrganizationId] [uniqueidentifier] NOT NULL,
	[WaterAuthorityId] [uniqueidentifier] NOT NULL,
	[HourSquareId] [uniqueidentifier] NOT NULL,
	[ProvinceId] [uniqueidentifier] NULL,
	[FieldTestId] [uniqueidentifier] NULL
)
GO

CREATE NONCLUSTERED INDEX [IX_CatchData_CatchId] ON [report].[CatchData]
(
	[CatchId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_PeriodValue] ON [report].[CatchData]
(
	[PeriodValue] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_PeriodValueIsByCatch] ON [report].[CatchData]
(
	[IsByCatch] ASC,
	[PeriodValue] ASC
) INCLUDE ([CatchNumber],[CatchAreaId],[OrganizationId],[RayonId],[SubAreaId],[SubAreaHourSquareId],[WaterAuthorityId],[HourSquareId])

CREATE NONCLUSTERED INDEX [IX_CatchData_TrapId] ON [report].[CatchData]
(
	[TrapId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_CatchTypeId] ON [report].[CatchData]
(
	[CatchTypeId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_TrapTypeId] ON [report].[CatchData]
(
	[TrapTypeId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_TrappingTypeId] ON [report].[CatchData]
(
	[TrappingTypeId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_CatchAreaId] ON [report].[CatchData]
(
	[CatchAreaId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_SubAreaHourSquareId] ON [report].[CatchData]
(
	[SubAreaHourSquareId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_SubAreaId] ON [report].[CatchData]
(
	[SubAreaId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_RayonId] ON [report].[CatchData]
(
	[RayonId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_WaterAuthorityId] ON [report].[CatchData]
(
	[WaterAuthorityId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_OrganizationId] ON [report].[CatchData]
(
	[OrganizationId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_CreatedById] ON [report].[CatchData]
(
	[CreatedById] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_HourSquareId] ON [report].[CatchData]
(
	[HourSquareId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_ProvinceId] ON [report].[CatchData]
(
	[ProvinceId] ASC
)

CREATE NONCLUSTERED INDEX [IX_CatchData_FieldTestId] ON [report].[CatchData]
(
	[FieldTestId] ASC
)
GO

--------------------------------------------------------------
IF OBJECT_ID('report.TimeRegistrationData', 'U') IS NOT NULL 
  DROP TABLE report.TimeRegistrationData;
GO

CREATE TABLE [report].[TimeRegistrationData](
	[TimeRegistrationId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[RecordedOn] [datetimeoffset](7) NOT NULL,
	[Hours] [float] NOT NULL,
	[Period] [int] NOT NULL,
	[Week] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[PeriodValue] [int] NOT NULL,
	[TrappingTypeId] [uniqueidentifier] NOT NULL,
	[SubAreaHourSquareId] [uniqueidentifier] NOT NULL,
	[SubAreaId] [uniqueidentifier] NOT NULL,
	[CatchAreaId] [uniqueidentifier] NOT NULL,
	[RayonId] [uniqueidentifier] NOT NULL,
	[OrganizationId] [uniqueidentifier] NOT NULL,
	[WaterAuthorityId] [uniqueidentifier] NOT NULL,
	[HourSquareId] [uniqueidentifier] NOT NULL,
	[FieldTestId] [uniqueidentifier] NULL
)
GO

CREATE NONCLUSTERED INDEX [IX_TimeRegistrationData_TimeRegistrationId] ON [report].[TimeRegistrationData]
(
	[TimeRegistrationId] ASC
)

CREATE NONCLUSTERED INDEX [IX_TimeRegistrationData_PeriodValue] ON [report].[TimeRegistrationData] 
(
	[PeriodValue]
)
INCLUDE ([Hours],[CatchAreaId],[OrganizationId],[RayonId],[SubAreaId],[SubAreaHourSquareId],[WaterAuthorityId],[HourSquareId])

CREATE NONCLUSTERED INDEX [IX_TimeRegistrationData_TrappingTypeId] ON [report].[TimeRegistrationData]
(
	[TrappingTypeId] ASC
)

CREATE NONCLUSTERED INDEX [IX_TimeRegistrationData_SubAreaHourSquareId] ON [report].[TimeRegistrationData]
(
	[SubAreaHourSquareId] ASC
)

CREATE NONCLUSTERED INDEX [IX_TimeRegistrationData_SubAreaId] ON [report].[TimeRegistrationData]
(
	[SubAreaId] ASC
)

CREATE NONCLUSTERED INDEX [IX_TimeRegistrationData_CatchAreaId] ON [report].[TimeRegistrationData]
(
	[CatchAreaId] ASC
)

CREATE NONCLUSTERED INDEX [IX_TimeRegistrationData_RayonId] ON [report].[TimeRegistrationData]
(
	[RayonId] ASC
)

CREATE NONCLUSTERED INDEX [IX_TimeRegistrationData_OrganizationId] ON [report].[TimeRegistrationData]
(
	[OrganizationId] ASC
)

CREATE NONCLUSTERED INDEX [IX_TimeRegistrationData_WaterAuthorityId] ON [report].[TimeRegistrationData]
(
	[WaterAuthorityId] ASC
)

CREATE NONCLUSTERED INDEX [IX_TimeRegistrationData_UserId] ON [report].[TimeRegistrationData]
(
	[UserId] ASC
)

CREATE NONCLUSTERED INDEX [IX_TimeRegistrationData_HourSquareId] ON [report].[TimeRegistrationData]
(
	[HourSquareId] ASC
)

CREATE NONCLUSTERED INDEX [IX_TimeRegistrationData_FieldTestId] ON [report].[TimeRegistrationData]
(
	[FieldTestId] ASC
)
GO

-----------------------------------------------------------
CREATE OR ALTER PROCEDURE [report].[PopulateCatchData]
AS
    DECLARE @ResultValue int
BEGIN
    TRUNCATE TABLE [report].[CatchData]

    INSERT INTO [report].[CatchData] (
        [CatchId], 
        [CatchNumber],
        [CreatedById],
	    [CreatedOn],
	    [RecordedOn],
        [Period], 
        [Week], 
        [Year],
        [PeriodValue], 
        [CatchStatus],
        [TrapId],
        [NumberOfTraps],
        [TrapStatus],
        [CatchTypeId],
        [IsByCatch],
        [TrapTypeId],
        [TrappingTypeId],
        [SubAreaHourSquareId],
        [SubAreaId],
        [CatchAreaId],
        [RayonId],
        [OrganizationId],
        [WaterAuthorityId],
		[HourSquareId],
		[ProvinceId],
		[FieldTestId]
    )
    SELECT
        C.Id AS CatchId, 
        C.Number AS CatchNumber, 
        C.CreatedById,
        C.CreatedOn,
        C.RecordedOn, 
        C.[Period], 
        C.[Week], 
        C.[Year],
        dbo.fn_GetPeriodValue(C.[Year], C.[Period], 0) AS PeriodValue, 
        C.[Status] AS CatchStatus,
        T.Id AS TrapId, 
        T.NumberOfTraps,
        T.[Status],
        CT.Id AS CatchTypeId,
        CT.IsByCatch,
        TT.Id AS TrapTypeId,
        TT.TrappingTypeId AS TrappingTypeId,
        SAHS.Id AS SubAreaHourSquareId,
        SA.Id AS SubAreaId,
        CA.Id AS CatchAreaId,
        R.Id AS RayonId,
        R.OrganizationId AS OrganizationId,
        SA.WaterAuthorityId AS WaterAuthorityId,
		SAHS.HourSquareId AS HourSquareId,
		T.ProvinceId AS ProvinceId,
		FTHS.FieldTestId
    FROM dbo.[Catch] C
        JOIN dbo.Trap T ON T.Id = C.TrapId
        JOIN dbo.TrapType TT ON TT.id = T.TrapTypeId
        JOIN dbo.CatchType CT ON C.CatchTypeId = CT.Id
        JOIN dbo.SubAreaHourSquare SAHS ON SAHS.Id = T.SubAreaHourSquareId
        JOIN dbo.SubArea SA ON SA.Id = SAHS.SubAreaId
        JOIN dbo.CatchArea CA ON CA.Id = SA.CatchAreaId
        JOIN dbo.Rayon R ON R.Id = CA.RayonId
		LEFT OUTER JOIN dbo.FieldTestHourSquare FTHS ON FTHS.HourSquareId = SAHS.HourSquareId

    SELECT @ResultValue=COUNT(*) FROM [report].[CatchData]

    RETURN @ResultValue
END
GO


-----------------------------------------------------------
CREATE OR ALTER PROCEDURE [report].[PopulateTimeRegistrationData]
AS
    DECLARE @ResultValue int
BEGIN
    TRUNCATE TABLE [report].[TimeRegistrationData]

    INSERT INTO [report].[TimeRegistrationData] (
        [TimeRegistrationId], 
        [UserId],
	    [CreatedOn],
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
        [WaterAuthorityId],
		[HourSquareId],
		[FieldTestId]
    )
    SELECT
        TR.Id AS TimeRegistrationId,
        TR.UserId,
        TR.CreatedOn,
        TR.[Date], 
        TR.[Hours], 
        TR.[Period], 
        TR.[Week], 
        TR.[Year],
        dbo.fn_GetPeriodValue(TR.[Year], TR.[Period], 0) AS PeriodValue,
        TR.TrappingTypeId AS TrappingTypeId,
        SAHS.Id AS SubAreaHourSquareId,
        SA.Id AS SubAreaId,
        CA.Id AS CatchAreaId,
        R.Id AS RayonId,
        R.OrganizationId AS OrganizationId,
        SA.WaterAuthorityId AS WaterAuthorityId,
		SAHS.HourSquareId AS HourSquareId,
		FTHS.FieldTestId
    FROM dbo.TimeRegistration TR
        JOIN dbo.SubAreaHourSquare SAHS ON SAHS.Id = TR.SubAreaHourSquareId
        JOIN dbo.SubArea SA ON SA.Id = SAHS.SubAreaId
        JOIN dbo.CatchArea CA ON CA.Id = SA.CatchAreaId
        JOIN dbo.Rayon R ON R.Id = CA.RayonId
        JOIN dbo.WaterAuthority WA ON WA.Id = SA.WaterAuthorityId
		LEFT OUTER JOIN FieldTestHourSquare FTHS ON FTHS.HourSquareId = SAHS.HourSquareId

    SELECT @ResultValue=COUNT(*) FROM [report].[TimeRegistrationData]

    RETURN @ResultValue
END
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
	    TRD.RecordedOn AS 'RecordedOn',
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
        JOIN dbo.TrappingType TrT ON TrT.Id = TRD.TrappingTypeId
        JOIN dbo.SubAreaHourSquare SAHS ON SAHS.Id = TRD.SubAreaHourSquareId
        JOIN dbo.SubArea SA ON SA.Id = SAHS.SubAreaId
        JOIN dbo.CatchArea CA ON CA.Id = SA.CatchAreaId
        JOIN dbo.Rayon R ON R.Id = CA.RayonId
        JOIN dbo.Organization O ON O.Id = R.OrganizationId
        JOIN dbo.WaterAuthority WA ON WA.Id = SA.WaterAuthorityId
        JOIN dbo.HourSquare HS ON HS.Id = SAHS.HourSquareId
	    JOIN dbo.[User] AS U ON TRD.UserId = U.Id
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
	    TRD.RecordedOn AS 'RecordedOn',
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
        JOIN dbo.TrappingType TrT ON TrT.Id = TRD.TrappingTypeId
        JOIN dbo.SubAreaHourSquare SAHS ON SAHS.Id = TRD.SubAreaHourSquareId
        JOIN dbo.SubArea SA ON SA.Id = SAHS.SubAreaId
        JOIN dbo.CatchArea CA ON CA.Id = SA.CatchAreaId
        JOIN dbo.Rayon R ON R.Id = CA.RayonId
        JOIN dbo.Organization O ON O.Id = R.OrganizationId
        JOIN dbo.WaterAuthority WA ON WA.Id = SA.WaterAuthorityId
        JOIN dbo.HourSquare HS ON HS.Id = SAHS.HourSquareId
	    JOIN dbo.[User] AS U ON TRD.UserId = U.Id
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


-----------------------------------------------------------
---MapReport Heatmap
CREATE OR ALTER FUNCTION [dbo].[fn_HeatMapReport]
(	
	@isBeverrat int,
	@organizationId varchar(36),
	@startDate datetime,
	@endDate datetime
)

RETURNS TABLE 
AS
RETURN 
(
	SELECT 
		t.Id AS TrapId,
		t.[Location] AS TrapLocation,
		Result.NumberOfCatches AS Catches
	FROM Trap t
	INNER JOIN
	(
		SELECT
			cd.TrapId AS TrapId,
			SUM (cd.CatchNumber) AS NumberOfCatches
		FROM [report].[CatchData] cd
		WHERE 
			cd.IsByCatch = 0
			AND (@organizationId IS NULL OR @organizationId='0' OR cd.OrganizationId = @organizationId) 
			AND (@startDate is null OR @startDate < cd.RecordedOn)
			AND (@endDate is null OR cd.RecordedOn < @endDate )
			AND ((@isBeverrat is null OR @isBeverrat = 0)
				OR (@isBeverrat = 1 AND cd.CatchTypeId in ('85803328-15E7-92EF-528F-00E91B6D4815',
														'2539B02A-9298-7B9F-4273-3E8AC99D7C63',
														'7A8199E8-21DF-7556-1F0A-549E94645B6F',
														'49B51935-918B-5A38-2493-A4141FEF8C52',
														'8957CB9D-936C-29CB-8511-A9C9A7EC6A7E')) 
				OR (@isBeverrat = 2 AND cd.CatchTypeId in ('645F7089-7F21-50C5-30C4-5FE30CC693F1',
														'3D1358F4-61D4-21D8-9438-90096EEEA47E',
														'44711E96-25B8-0AF6-669B-CCDC8ABA9017',
														'E72CCB01-65BB-A1AA-A5E8-EB909FE77374',
														'C8783519-41C6-5654-1977-F6956ABA2EF4'))
			)
		GROUP BY cd.TrapId
	) Result ON Result.TrapId = t.Id  
)
GO