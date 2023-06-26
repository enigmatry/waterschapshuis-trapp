-----------------------------------------------------------
IF OBJECT_ID('report.CatchData', 'U') IS NOT NULL 
  DROP TABLE report.CatchData;
GO

CREATE TABLE [report].[CatchData]
(
	[CatchId] [uniqueidentifier] NOT NULL,
	[CatchNumber] [int] NOT NULL,
	[RecordedOn] [datetimeoffset](7) NOT NULL,
	[Period] [int] NOT NULL,
	[Week] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[PeriodValue] [int] NOT NULL,
	[CatchStatus] [tinyint] NOT NULL,
	[TrapId] [uniqueidentifier] NOT NULL,
	[NumberOfTraps] [int] NOT NULL,
	[CatchTypeId] [uniqueidentifier] NOT NULL,
	[IsByCatch] [bit] NOT NULL,
	[TrapTypeId] [uniqueidentifier] NOT NULL,
	[TrappingTypeId] [uniqueidentifier] NOT NULL,
	[SubAreaHourSquareId] [uniqueidentifier] NOT NULL,
	[SubAreaId] [uniqueidentifier] NOT NULL,
	[CatchAreaId] [uniqueidentifier] NOT NULL,
	[RayonId] [uniqueidentifier] NOT NULL,
	[OrganizationId] [uniqueidentifier] NOT NULL,
	[WaterAuthorityId] [uniqueidentifier] NOT NULL
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
) INCLUDE ([CatchNumber],[CatchAreaId],[OrganizationId],[RayonId],[SubAreaId],[SubAreaHourSquareId],[WaterAuthorityId])

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
GO

--------------------------------------------------------------
IF OBJECT_ID('report.TimeRegistrationData', 'U') IS NOT NULL 
  DROP TABLE report.TimeRegistrationData;
GO

CREATE TABLE [report].[TimeRegistrationData]
(
	[TimeRegistrationId] [uniqueidentifier] NOT NULL,
	[RecordedOn] [datetimeoffset](7) NULL,
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
	[WaterAuthorityId] [uniqueidentifier] NOT NULL
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
INCLUDE ([Hours],[CatchAreaId],[OrganizationId],[RayonId],[SubAreaId],[SubAreaHourSquareId],[WaterAuthorityId])

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

-----------------------------------------------------------
CREATE OR ALTER PROCEDURE [report].[PopulateTimeRegistrationData]
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
	    SUM(T.NumberOfTraps) AS 'NumberOfTraps',
	    CASE T.[Status] WHEN 1 THEN 'Vangend' WHEN 2 THEN 'Niet-vangend' WHEN 3 THEN 'Verwijderd' ELSE 'Onbekend' END AS 'TrapStatus',
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
	    P.[Name] AS 'ProvinceName',
	    HS.[Name] AS 'HourSquareName',
	    NULL AS 'FieldTestName',
	    NULL AS 'Hours', 
	    NULL AS 'HoursPreviousYear'
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
    GROUP BY
        C.Id,
	    C.CreatedOn,
        C.RecordedOn,
	    C.[Period],
	    C.[Week],
        C.CreatedById,
	    CT.IsByCatch, 
	    CT.[Name],
	    T.[Status],
	    TT.[Name],
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
	    SUM(T.NumberOfTraps) AS 'NumberOfTraps',
	    CASE T.[Status] WHEN 1 THEN 'Vangend' WHEN 2 THEN 'Niet-vangend' WHEN 3 THEN 'Verwijderd' ELSE 'Onbekend' END AS 'TrapStatus',
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
	    P.[Name] AS 'ProvinceName',
	    HS.[Name] AS 'HourSquareName',
	    NULL AS 'FieldTestName',
	    NULL AS 'Hours', 
	    NULL AS 'HoursPreviousYear'
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
    GROUP BY
        C.Id,
	    C.CreatedOn,
        C.RecordedOn,
	    C.[Period],
	    C.[Week],
        C.CreatedById,
	    CT.IsByCatch, 
	    CT.[Name],
	    T.[Status],
	    TT.[Name],
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
	    NULL AS 'FieldTestName',
	    SUM(TR.[Hours]) AS 'Hours', 
	    NULL AS 'HoursPreviousYear'
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
	    SUM(TR.[Hours]) AS 'HoursPreviousYear'
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

    ) AS Result

    SELECT @ResultValue = COUNT(*) FROM [report].[OwnReportData]

    RETURN @ResultValue
END
GO
