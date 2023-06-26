BEGIN TRANSACTION

DECLARE @versionRegionalLayoutId AS UNIQUEIDENTIFIER;
SET @versionRegionalLayoutId='8110e841-f71a-43e5-ab12-8f5eb85676f5';

INSERT INTO [dbo].[VersionRegionalLayout] ([Id], [Name], [StartDate]) VALUES(@versionRegionalLayoutId, 'V2 indeling', CONVERT(DATE, '19/10/2020', 103));

UPDATE [dbo].[SubAreaHourSquare] SET [VersionRegionalLayoutId] = @versionRegionalLayoutId;
UPDATE [dbo].[SubArea] SET [VersionRegionalLayoutId] = @versionRegionalLayoutId;
UPDATE [dbo].[CatchArea] SET [VersionRegionalLayoutId] = @versionRegionalLayoutId;
UPDATE [dbo].[Rayon] SET [VersionRegionalLayoutId] = @versionRegionalLayoutId;
UPDATE [dbo].[WaterAuthority] SET [VersionRegionalLayoutId] = @versionRegionalLayoutId;

COMMIT TRANSACTION
