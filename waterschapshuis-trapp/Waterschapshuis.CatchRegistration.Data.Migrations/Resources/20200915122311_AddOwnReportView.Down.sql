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
	[Date] [datetimeoffset](7) NOT NULL,
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
	[Date] [datetimeoffset](7) NULL,
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
DROP PROCEDURE [report].[PopulateCatchData]
GO

CREATE PROCEDURE [report].[PopulateCatchData]
AS
    DECLARE @ResultValue int
BEGIN

    TRUNCATE TABLE [report].[CatchData]

    INSERT INTO [report].[CatchData]
        ([CatchId], [CatchNumber], [Date], [Period], [Week], [Year],
        [PeriodValue], [CatchStatus],
        [TrapId] ,[NumberOfTraps],
        [CatchTypeId], [IsByCatch],
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
        c.Id as CatchId, c.Number as CatchNumber, c.RecordedOn, c.Period, c.[Week], c.[Year],
        dbo.fn_GetPeriodValue(c.[Year], c.Period, 0) as PeriodValue, c.Status as CatchStatus,
        t.Id as TrapId, t.NumberOfTraps,
        ct.Id as CatchTypeId, ct.IsByCatch,
        tt.Id as TrapTypeId,
        trpt.Id as TrappingTypeId,
        sahs.Id as SubAreaHourSquareId,
        sa.Id as SubAreaId,
        ca.Id as CatchAreaId,
        r.Id as RayonId,
        r.OrganizationId,
        wa.Id as WaterAuthorityId
    FROM
        dbo.Catch c
        JOIN dbo.Trap t on t.Id = c.TrapId
        JOIN dbo.TrapType tt on tt.id = t.TrapTypeId
        JOIN dbo.TrappingType trpt on trpt.Id = tt.TrappingTypeId
        JOIN dbo.CatchType ct on c.CatchTypeId = ct.Id
        JOIN dbo.SubAreaHourSquare sahs on sahs.Id = t.SubAreaHourSquareId
        JOIN dbo.SubArea sa on sa.Id = sahs.SubAreaId
        JOIN dbo.CatchArea ca on ca.Id = sa.CatchAreaId
        JOIN dbo.Rayon r on r.Id = ca.RayonId
        JOIN dbo.WaterAuthority wa on wa.Id = sa.WaterAuthorityId

    SELECT @ResultValue=COUNT(*)
    FROM [report].[CatchData]

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

    INSERT INTO [report].[TimeRegistrationData]
        ([TimeRegistrationId], [Date], [Hours], [Period], [Week], [Year], [PeriodValue],
        [TrappingTypeId],
        [SubAreaHourSquareId],
        [SubAreaId],
        [CatchAreaId],
        [RayonId],
        [OrganizationId],
        [WaterAuthorityId]
        )
    SELECT
        tr.Id as TimeRegistrationId, tr.Date, tr.[Hours], tr.Period, tr.[Week], tr.[Year],
        dbo.fn_GetPeriodValue(tr.[Year], tr.Period, 0) as PeriodValue,
        trpt.Id as TrappingTypeId,
        sahs.Id as SubAreaHourSquareId,
        sa.Id as SubAreaId,
        ca.Id as CatchAreaId,
        r.Id as RayonId,
        r.OrganizationId,
        wa.Id as WaterAuthorityId
    FROM
        dbo.TimeRegistration tr
        JOIN dbo.TrappingType trpt on trpt.Id = tr.TrappingTypeId
        JOIN dbo.SubAreaHourSquare sahs on sahs.Id = tr.SubAreaHourSquareId
        JOIN dbo.SubArea sa on sa.Id = sahs.SubAreaId
        JOIN dbo.CatchArea ca on ca.Id = sa.CatchAreaId
        JOIN dbo.Rayon r on r.Id = ca.RayonId
        JOIN dbo.WaterAuthority wa on wa.Id = sa.WaterAuthorityId

    SELECT @ResultValue=COUNT(*)
    FROM [report].[TimeRegistrationData]

    RETURN @ResultValue
END
GO