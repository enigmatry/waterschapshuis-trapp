-- =============================================
-- Description:	Drop and re-inserts the time registration data table
-- =============================================
CREATE OR ALTER PROCEDURE [report].PopulateTimeRegistrationData
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
