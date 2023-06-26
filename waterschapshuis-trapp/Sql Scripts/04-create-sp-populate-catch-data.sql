-- =============================================
-- Description:	Drop and re-inserts the catch data table
-- =============================================
CREATE OR ALTER PROCEDURE [report].PopulateCatchData
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
