IF (NOT EXISTS (SELECT * FROM dbo.gt_pk_metadata WHERE table_name = 'vw_Observations_Geo'))
BEGIN
	INSERT dbo.gt_pk_metadata
	VALUES ('dbo', 'vw_Observations_Geo', 'Id', NULL, 'assigned', NULL);
	PRINT('Inserted configuration for ""vw_Observations_Geo"" view PK');
END

IF (NOT EXISTS (SELECT * FROM dbo.gt_pk_metadata WHERE table_name = 'vw_SubAreaHourSquare_Catch_Geo'))
BEGIN
	INSERT dbo.gt_pk_metadata
	VALUES ('dbo', 'vw_SubAreaHourSquare_Catch_Geo', 'SubAreaHourSquareId', NULL, 'assigned', NULL);
	PRINT('Inserted configuration for ""vw_SubAreaHourSquare_Catch_Geo"" view PK');
END

IF (NOT EXISTS (SELECT * FROM dbo.gt_pk_metadata WHERE table_name = 'vw_TrackingLines_Geo'))
BEGIN
	INSERT dbo.gt_pk_metadata
	VALUES ('dbo', 'vw_TrackingLines_Geo', 'Id', NULL, 'assigned', NULL);
	PRINT('Inserted configuration for ""vw_TrackingLines_Geo"" view PK');
END

IF (NOT EXISTS (SELECT * FROM dbo.gt_pk_metadata WHERE table_name = 'vw_HistoricalTraps_Geo'))
BEGIN
	INSERT dbo.gt_pk_metadata
	VALUES ('dbo', 'vw_HistoricalTraps_Geo', 'TrapId', NULL, 'assigned', NULL);
	PRINT('Inserted configuration for ""vw_HistoricalTraps_Geo"" view PK');
END

IF (NOT EXISTS (SELECT * FROM dbo.gt_pk_metadata WHERE table_name = 'vw_PublicTrackings_Geo'))
BEGIN
	INSERT dbo.gt_pk_metadata
	VALUES ('dbo', 'vw_PublicTrackings_Geo', 'Id', NULL, 'assigned', NULL);
	PRINT('Inserted configuration for ""vw_PublicTrackings_Geo"" view PK');
END