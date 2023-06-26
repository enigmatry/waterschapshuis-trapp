IF NOT EXISTS (SELECT name FROM sys.schemas WHERE name = N'report')
BEGIN
	EXEC('CREATE SCHEMA [report]');
END
GO

-----------------------------------------------------------
IF OBJECT_ID('report.CatchData', 'U') IS NOT NULL 
	DROP TABLE report.CatchData; 
GO

CREATE TABLE [report].[CatchData]
(
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
	[CatchTypeName] [nvarchar](50) NOT NULL,
	[IsByCatch] [bit] NOT NULL,
	[TrapTypeId] [uniqueidentifier] NOT NULL,
	[TrapTypeName] [nvarchar](50) NOT NULL,
	[TrappingTypeId] [uniqueidentifier] NOT NULL,
	[TrappingTypeName] [nvarchar](50) NOT NULL,
	[SubAreaHourSquareId] [uniqueidentifier] NOT NULL,
	[SubAreaId] [uniqueidentifier] NOT NULL,
	[SubAreaName] [nvarchar](10) NOT NULL,
	[CatchAreaId] [uniqueidentifier] NOT NULL,
	[CatchAreaName] [nvarchar](50) NOT NULL,
	[RayonId] [uniqueidentifier] NOT NULL,
	[RayonName] [varchar](50) NOT NULL,
	[OrganizationId] [uniqueidentifier] NOT NULL,
	[OrganizationName] [nvarchar](50) NOT NULL,
	[WaterAuthorityId] [uniqueidentifier] NOT NULL,
	[WaterAuthorityName] [nvarchar](60) NOT NULL,
	[HourSquareId] [uniqueidentifier] NOT NULL,
	[HourSquareName] [nvarchar](5) NOT NULL,
	[ProvinceId] [uniqueidentifier] NULL,
	[ProvinceName] [nvarchar](50) NULL,
	[FieldTestId] [uniqueidentifier] NULL
)
GO

DROP INDEX IF EXISTS 
    [IX_CatchData_CatchAreaId] ON [report].[CatchData],
    [IX_CatchData_CatchId] ON [report].[CatchData],
    [IX_CatchData_CatchTypeId] ON [report].[CatchData],
    [IX_CatchData_OrganizationId] ON [report].[CatchData],
    [IX_CatchData_PeriodValue] ON [report].[CatchData],
    [IX_CatchData_PeriodValueIsByCatch] ON [report].[CatchData],
    [IX_CatchData_RayonId] ON [report].[CatchData],
    [IX_CatchData_SubAreaHourSquareId] ON [report].[CatchData],
    [IX_CatchData_SubAreaId] ON [report].[CatchData],
    [IX_CatchData_TrapId] ON [report].[CatchData],
    [IX_CatchData_TrappingTypeId] ON [report].[CatchData],
    [IX_CatchData_TrapTypeId] ON [report].[CatchData],
    [IX_CatchData_WaterAuthorityId] ON [report].[CatchData]
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
)
INCLUDE ([CatchNumber],[CatchAreaId],[OrganizationId],[RayonId],[SubAreaId],[SubAreaHourSquareId],[WaterAuthorityId],[HourSquareId])

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

CREATE TABLE [report].[TimeRegistrationData]
(
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
	[TrappingTypeName] [nvarchar](50) NOT NULL,
	[SubAreaHourSquareId] [uniqueidentifier] NOT NULL,
	[SubAreaId] [uniqueidentifier] NOT NULL,
	[SubAreaName] [nvarchar](10) NOT NULL,
	[CatchAreaId] [uniqueidentifier] NOT NULL,
	[CatchAreaName] [nvarchar](50) NOT NULL,
	[RayonId] [uniqueidentifier] NOT NULL,
	[RayonName] [varchar](50) NOT NULL,
	[OrganizationId] [uniqueidentifier] NOT NULL,
	[OrganizationName] [nvarchar](50) NOT NULL,
	[WaterAuthorityId] [uniqueidentifier] NOT NULL,
	[WaterAuthorityName] [nvarchar](60) NOT NULL,
	[HourSquareId] [uniqueidentifier] NOT NULL,
	[HourSquareName] [nvarchar](5) NOT NULL,
	[FieldTestId] [uniqueidentifier] NULL
)
GO

DROP INDEX IF EXISTS 
    [IX_TimeRegistrationData_CatchAreaId] ON [report].[TimeRegistrationData],
    [IX_TimeRegistrationData_OrganizationId] ON [report].[TimeRegistrationData],
    [IX_TimeRegistrationData_PeriodValue] ON [report].[TimeRegistrationData],
    [IX_TimeRegistrationData_RayonId] ON [report].[TimeRegistrationData],
    [IX_TimeRegistrationData_SubAreaHourSquareId] ON [report].[TimeRegistrationData],
    [IX_TimeRegistrationData_SubAreaId] ON [report].[TimeRegistrationData],
    [IX_TimeRegistrationData_TimeRegistrationId] ON [report].[TimeRegistrationData],
    [IX_TimeRegistrationData_TrappingTypeId] ON [report].[TimeRegistrationData],
    [IX_TimeRegistrationData_WaterAuthorityId] ON [report].[TimeRegistrationData]
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
DROP PROCEDURE [report].[PopulateCatchData]
GO

CREATE PROCEDURE [report].[PopulateCatchData]
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
        [CatchTypeName],
        [IsByCatch],
        [TrapTypeId],
        [TrapTypeName],
        [TrappingTypeId],
        [TrappingTypeName],
        [SubAreaHourSquareId],
        [SubAreaId],
        [SubAreaName],
        [CatchAreaId],
        [CatchAreaName],
        [RayonId],
        [RayonName],
        [OrganizationId],
        [OrganizationName],
        [WaterAuthorityId],
        [WaterAuthorityName],
		[HourSquareId],
        [HourSquareName],
		[ProvinceId],
        [ProvinceName],
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
        CT.[Name] AS CatchTypeName,
        CT.IsByCatch,
        TT.Id AS TrapTypeId,
        TT.[Name] AS TrapTypeName,
        TrT.Id AS TrappingTypeId,
        TrT.[Name] AS TrappingTypeName,
        SAHS.Id AS SubAreaHourSquareId,
        SA.Id AS SubAreaId,
        SA.[Name] AS SubAreaName,
        CA.Id AS CatchAreaId,
        CA.[Name] AS CatchAreaName,
        R.Id AS RayonId,
        R.[Name] AS RayonName,
        O.Id AS OrganizationId,
        O.[Name] AS OrganizationName,
        WA.Id AS WaterAuthorityId,
        WA.[Name] AS WaterAuthorityName,
		HS.Id AS HourSquareId,
		HS.[Name] AS HourSquareName,
		P.Id AS ProvinceId,
		P.[Name] AS ProvinceName,
		FTHS.FieldTestId
    FROM dbo.[Catch] C
        JOIN dbo.Trap T ON T.Id = C.TrapId
        JOIN dbo.TrapType TT ON TT.id = T.TrapTypeId
        JOIN dbo.TrappingType TrT ON TrT.Id = TT.TrappingTypeId
        JOIN dbo.CatchType CT ON C.CatchTypeId = CT.Id
        JOIN dbo.SubAreaHourSquare SAHS ON SAHS.Id = T.SubAreaHourSquareId
        JOIN dbo.SubArea SA ON SA.Id = SAHS.SubAreaId
        JOIN dbo.CatchArea CA ON CA.Id = SA.CatchAreaId
        JOIN dbo.Rayon R ON R.Id = CA.RayonId
        JOIN dbo.Organization O ON O.Id = R.OrganizationId
        JOIN dbo.WaterAuthority WA ON WA.Id = SA.WaterAuthorityId
        JOIN dbo.HourSquare HS ON HS.Id = SAHS.HourSquareId
		LEFT OUTER JOIN dbo.Province P ON P.Id = T.ProvinceId
		LEFT OUTER JOIN dbo.FieldTestHourSquare FTHS ON FTHS.HourSquareId = HS.Id

    SELECT @ResultValue=COUNT(*) FROM [report].[CatchData]

    RETURN @ResultValue
END
GO

-----------------------------------------------------------
DROP PROCEDURE [report].[PopulateTimeRegistrationData]
GO

CREATE PROCEDURE [report].[PopulateTimeRegistrationData]
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
        [TrappingTypeName],
        [SubAreaHourSquareId],
        [SubAreaId],
        [SubAreaName],
        [CatchAreaId],
        [CatchAreaName],
        [RayonId],
        [RayonName],
        [OrganizationId],
        [OrganizationName],
        [WaterAuthorityId],
        [WaterAuthorityName],
		[HourSquareId],
        [HourSquareName],
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
        TrT.Id AS TrappingTypeId,
        TrT.[Name] AS TrappingTypeName,
        SAHS.Id AS SubAreaHourSquareId,
        SA.Id AS SubAreaId,
        SA.[Name] AS SubAreaName,
        CA.Id AS CatchAreaId,
        CA.[Name] AS CatchAreaName,
        R.Id AS RayonId,
        R.[Name] AS RayonName,
        O.Id AS OrganizationId,
        O.[Name] AS OrganizationName,
        WA.Id AS WaterAuthorityId,
        WA.[Name] AS WaterAuthorityName,
		HS.Id AS HourSquareId,
		HS.[Name] AS HourSquareName,
		FTHS.FieldTestId
    FROM dbo.TimeRegistration TR
        JOIN dbo.TrappingType TrT ON TrT.Id = TR.TrappingTypeId
        JOIN dbo.SubAreaHourSquare SAHS ON SAHS.Id = TR.SubAreaHourSquareId
        JOIN dbo.SubArea SA ON SA.Id = SAHS.SubAreaId
        JOIN dbo.CatchArea CA ON CA.Id = SA.CatchAreaId
        JOIN dbo.Rayon R ON R.Id = CA.RayonId
        JOIN dbo.Organization O ON O.Id = R.OrganizationId
        JOIN dbo.WaterAuthority WA ON WA.Id = SA.WaterAuthorityId
        JOIN dbo.HourSquare HS ON HS.Id = SAHS.HourSquareId
		LEFT OUTER JOIN FieldTestHourSquare FTHS ON FTHS.HourSquareId = HS.Id

    SELECT @ResultValue=COUNT(*) FROM [report].[TimeRegistrationData]

    RETURN @ResultValue
END
GO

-----------------------------------------------------------
IF OBJECT_ID('[report].[vw_OwnReport]', 'V') IS NOT NULL 
  DROP VIEW [report].[vw_OwnReport]
GO

CREATE VIEW [report].[vw_OwnReport]
AS
-- CATCHES
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

-- CATCHES PREVIOUS YEAR
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

-- TIME REGISTRATIONS
SELECT
	RTRD.CreatedOn AS 'CreatedOn',
	RTRD.RecordedOn AS 'RecordedOn',
	YEAR(RTRD.RecordedOn) AS 'RecordedOnYear',
	RTRD.UserId AS 'OwnerId',
	U.[Name] AS 'OwnerName',
	RTRD.[Period] AS 'Period',
	RTRD.[Week] AS 'Week',
	NULL AS 'NumberOfCatches', 
	NULL AS 'NumberOfByCatches',
	NULL AS 'NumberOfCatchesPreviousYear', 
	NULL AS 'NumberOfByCatchesPreviousYear',
	NULL AS 'NumberOfTraps',
	NULL AS 'TrapStatus',
	NULL AS 'TrapTypeName',
	RTRD.TrappingTypeName AS 'TrappingTypeName',
	NULL AS 'IsByCatch', 
	NULL AS 'CatchTypeName',
	RTRD.OrganizationId AS 'OrganizationId',
	RTRD.OrganizationName AS 'OrganizationName',
	RTRD.WaterAuthorityName AS 'WaterAuthorityName',
	RTRD.RayonName AS 'RayonName',
	RTRD.CatchAreaName AS 'CatchAreaName',
	RTRD.SubAreaName AS 'SubAreaName',
	NULL AS 'ProvinceName',
	RTRD.HourSquareName AS 'HourSquareName',
	FT.[Name] AS 'FieldTestName',
	SUM(RTRD.[Hours]) AS 'Hours', 
	NULL AS 'HoursPreviousYear', 
	NULL AS 'KmOfWaterways'
FROM [report].TimeRegistrationData AS RTRD
	INNER JOIN [User] AS U ON RTRD.UserId = U.Id
	LEFT OUTER JOIN FieldTest AS FT ON RTRD.FieldTestId = FT.Id
GROUP BY
    RTRD.TimeRegistrationId,
	RTRD.CreatedOn,
	RTRD.RecordedOn,
	RTRD.[Period],
	RTRD.[Week],
    RTRD.UserId,
	RTRD.TrappingTypeName,
	RTRD.SubAreaName,
	RTRD.CatchAreaName,
	RTRD.RayonName,
    RTRD.OrganizationId,
	RTRD.OrganizationName,
	RTRD.HourSquareName,
	RTRD.WaterAuthorityName,
	U.[Name],
	FT.[Name]

UNION

-- TIME REGISTRATIONS PREVIOUS YEAR
SELECT
	RTRD.CreatedOn AS 'CreatedOn',
	RTRD.RecordedOn AS 'RecordedOn',
	YEAR(RTRD.RecordedOn) + 1 AS 'RecordedOnYear',
	RTRD.UserId AS 'OwnerId',
	U.[Name] AS 'OwnerName',
	RTRD.[Period] AS 'Period',
	RTRD.[Week] AS 'Week',
	NULL AS 'NumberOfCatches', 
	NULL AS 'NumberOfByCatches',
	NULL AS 'NumberOfCatchesPreviousYear', 
	NULL AS 'NumberOfByCatchesPreviousYear',
	NULL AS 'NumberOfTraps',
	NULL AS 'TrapStatus',
	NULL AS 'TrapTypeName',
	RTRD.TrappingTypeName AS 'TrappingTypeName',
	NULL AS 'IsByCatch', 
	NULL AS 'CatchTypeName',
	RTRD.OrganizationId AS 'OrganizationId',
	RTRD.OrganizationName AS 'OrganizationName',
	RTRD.WaterAuthorityName AS 'WaterAuthorityName',
	RTRD.RayonName AS 'RayonName',
	RTRD.CatchAreaName AS 'CatchAreaName',
	RTRD.SubAreaName AS 'SubAreaName',
	NULL AS 'ProvinceName',
	RTRD.HourSquareName AS 'HourSquareName',
	FT.[Name] AS 'FieldTestName',
	NULL AS 'Hours', 
	SUM(RTRD.[Hours]) AS 'HoursPreviousYear', 
	NULL AS 'KmOfWaterways'
FROM [report].TimeRegistrationData AS RTRD
	INNER JOIN [User] AS U ON RTRD.UserId = U.Id
	LEFT OUTER JOIN FieldTest AS FT ON RTRD.FieldTestId = FT.Id
GROUP BY
    RTRD.TimeRegistrationId,
	RTRD.CreatedOn,
	RTRD.RecordedOn,
	RTRD.[Period],
	RTRD.[Week],
    RTRD.UserId,
	RTRD.TrappingTypeName,
	RTRD.SubAreaName,
	RTRD.CatchAreaName,
	RTRD.RayonName,
    RTRD.OrganizationId,
	RTRD.OrganizationName,
	RTRD.HourSquareName,
	RTRD.WaterAuthorityName,
	U.[Name],
	FT.[Name]
GO