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


-----------------------------------------------------------
DROP PROCEDURE [report].[PopulateOwnReportData]
GO