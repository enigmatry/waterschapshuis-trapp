/****** Object:  View [dbo].[vw_PublicTrackings_Geo] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER   VIEW [dbo].[vw_PublicTrackings_Geo] AS
SELECT 
	Id,
    [Location],
	CreatedById
FROM Tracking
WHERE 
	isTrackingPrivate = 0 AND 
	IsTrackingMap = 1 AND 
	CAST(RecordedOn AS Date) = CAST(GETDATE() AS Date)
GO

/****** Object:  UserDefinedFunction [dbo].[fn_GetTrackingsByUser] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER   FUNCTION [dbo].[fn_GetTrackingsByUser]
(	
	@userId uniqueidentifier
)
RETURNS TABLE AS
RETURN 
(
    SELECT 
        Id AS TrackingId, 
        Location,
        CreatedById
    FROM Tracking
    WHERE 
		CreatedById = @userId
        AND IsTrackingMap = 1
        AND CAST(RecordedOn AS Date) = CAST(GETDATE() AS Date)
)
GO