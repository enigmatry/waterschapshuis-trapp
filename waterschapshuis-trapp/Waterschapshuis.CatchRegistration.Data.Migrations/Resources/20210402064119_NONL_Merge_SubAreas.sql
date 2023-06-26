BEGIN TRY
BEGIN TRANSACTION

DECLARE @SubAreaNewId UNIQUEIDENTIFIER
DECLARE @SubAreaHourSquareId UNIQUEIDENTIFIER
DECLARE @CatchAreaName NVARCHAR(50)
DECLARE @NewSubAreaName NVARCHAR(10)
DECLARE @WaterAuthorityId UNIQUEIDENTIFIER
DECLARE @VersionLayoutV2Id UNIQUEIDENTIFIER

SET @WaterAuthorityId = (
		SELECT Id
		FROM WaterAuthority
		WHERE Name = N'Waterschap Drents Overijsselse Delta'
		)
SET @VersionLayoutV2Id = dbo.fn_GetLatestVersionRegionalLayoutId()

--renaming subareras
DECLARE @TempTableSubAreaToBeRenamed TABLE (
	CatchAreaName NVARCHAR(50)
	,OldSubAreaName NVARCHAR(10)
	,NewSubAreaName NVARCHAR(10)
	);

INSERT INTO @TempTableSubAreaToBeRenamed VALUES ('NON-300', 'RenW-071', 'WDOD-300')
INSERT INTO @TempTableSubAreaToBeRenamed VALUES ('NON-301', 'RenW-100', 'WDOD-301')
INSERT INTO @TempTableSubAreaToBeRenamed VALUES ('NON-302', 'RenW-101', 'WDOD-302')
INSERT INTO @TempTableSubAreaToBeRenamed VALUES ('NON-303', 'RenW-110', 'WDOD-303')
INSERT INTO @TempTableSubAreaToBeRenamed VALUES ('NON-304', 'RenW-120', 'WDOD-304')
INSERT INTO @TempTableSubAreaToBeRenamed VALUES ('NON-305', 'RenW-130', 'WDOD-305')
INSERT INTO @TempTableSubAreaToBeRenamed VALUES ('NON-306', 'RenW-140', 'WDOD-306')
INSERT INTO @TempTableSubAreaToBeRenamed VALUES ('NON-307', 'RenW-150', 'WDOD-307')
INSERT INTO @TempTableSubAreaToBeRenamed VALUES ('NON-308', 'RenW-151', 'WDOD-308')
INSERT INTO @TempTableSubAreaToBeRenamed VALUES ('NON-309', 'RenW-200', 'WDOD-309')
INSERT INTO @TempTableSubAreaToBeRenamed VALUES ('NON-310', 'RenW-300', 'WDOD-310')
INSERT INTO @TempTableSubAreaToBeRenamed VALUES ('NON-311', 'RenW-400', 'WDOD-311')
INSERT INTO @TempTableSubAreaToBeRenamed VALUES ('NON-312', 'RenW-500', 'WDOD-312')
INSERT INTO @TempTableSubAreaToBeRenamed VALUES ('NON-313', 'RenW-600', 'WDOD-313')
INSERT INTO @TempTableSubAreaToBeRenamed VALUES ('NON-314', 'RenW-700', 'WDOD-314')
INSERT INTO @TempTableSubAreaToBeRenamed VALUES ('NON-315', 'RenW-800', 'WDOD-315')
INSERT INTO @TempTableSubAreaToBeRenamed VALUES ('NON-316', 'RenW-900', 'WDOD-316')

UPDATE SA
SET SA.Name = TBL.NewSubAreaName
FROM SubArea SA 
INNER JOIN CatchArea CA ON CA.Id = SA.CatchAreaId
INNER JOIN @TempTableSubAreaToBeRenamed TBL ON TBL.OldSubAreaName = SA.Name
WHERE CA.Name IN (SELECT CatchAreaName FROM @TempTableSubAreaToBeRenamed)


--merging
DECLARE @TempTableSubArea TABLE (
	RowId INT
	,CatchAreaName NVARCHAR(50)
	,NewSubAreaName NVARCHAR(10)
	);

INSERT INTO @TempTableSubArea VALUES (1, 'NON-401', 'WDOD-401')
INSERT INTO @TempTableSubArea VALUES (2, 'NON-402', 'WDOD-402')
INSERT INTO @TempTableSubArea VALUES (3, 'NON-403', 'WDOD-403')
INSERT INTO @TempTableSubArea VALUES (4, 'NON-404', 'WDOD-404')
INSERT INTO @TempTableSubArea VALUES (5, 'NON-405', 'WDOD-405')
INSERT INTO @TempTableSubArea VALUES (6, 'NON-406', 'WDOD-406')
INSERT INTO @TempTableSubArea VALUES (7, 'NON-407', 'WDOD-407')
INSERT INTO @TempTableSubArea VALUES (8, 'NON-408', 'WDOD-408')
INSERT INTO @TempTableSubArea VALUES (9, 'NON-409', 'WDOD-409')
INSERT INTO @TempTableSubArea VALUES (10,'NON-410', 'WDOD-410')
INSERT INTO @TempTableSubArea VALUES (11,'NON-411', 'WDOD-411')
INSERT INTO @TempTableSubArea VALUES (12,'NON-412', 'WDOD-412')
INSERT INTO @TempTableSubArea VALUES (13,'NON-413', 'WDOD-413')
INSERT INTO @TempTableSubArea VALUES (14,'NON-414', 'WDOD-414')
INSERT INTO @TempTableSubArea VALUES (15,'NON-415', 'WDOD-415')
INSERT INTO @TempTableSubArea VALUES (16,'NON-416', 'WDOD-416')
INSERT INTO @TempTableSubArea VALUES (17,'NON-417', 'WDOD-417')
INSERT INTO @TempTableSubArea VALUES (18,'NON-418', 'WDOD-418')

DECLARE @TotalRecords INT
DECLARE @Index INT

SET @Index = 1

SELECT @TotalRecords = COUNT(*)
	FROM @TempTableSubArea

WHILE (@Index <= @TotalRecords)
	BEGIN
		SELECT @CatchAreaName = CatchAreaName
			,@NewSubAreaName = NewSubAreaName
		FROM @TempTableSubArea
		WHERE RowId = @Index

		SET @Index = @Index + 1;
		SET @SubAreaNewId = NEWID();

		INSERT INTO [dbo].[SubArea] (
			[Id]
			,[Name]
			,[Geometry]
			,[CatchAreaId]
			,[WaterAuthorityId]
			)
			SELECT @SubAreaNewId
				,@NewSubAreaName
				,CA.[Geometry]
				,CA.Id
				,@WaterAuthorityId
			FROM CatchArea CA
			WHERE CA.Name = @CatchAreaName;

		WITH CTE_OldSubareaHourSquares
			AS (
				SELECT HourSquareId AS OldHourSquareId
					,SUM(KmWaterway) AS SumKmWaterway
					,SUM(Ditch) AS SumDitch
					,SUM(WetDitch) AS SumWetDitch
					,MIN(PercentageDitch) AS MinPercentageDitch
				FROM SubAreaHourSquare SAHS
					INNER JOIN SubArea SA ON SA.Id = SAHS.SubAreaId
					INNER JOIN CatchArea CA ON ca.Id = SA.CatchAreaId
				WHERE CA.Name = @CatchAreaName
					AND SAHS.VersionRegionalLayoutId = @VersionLayoutV2Id
				GROUP BY HourSquareId
			)

		INSERT INTO [dbo].[SubAreaHourSquare] (
			[Id]
			,[SubAreaId]
			,[HourSquareId]
			,[KmWaterway]
			,[PercentageDitch]
			,[Ditch]
			,[WetDitch]
			,[Geometry]
			,[VersionRegionalLayoutId]
			)
			SELECT NEWID()
				,SA.Id
				,HS.Id
				,OSAHS.SumKmWaterway
				,OSAHS.MinPercentageDitch
				,OSAHS.SumDitch
				,OSAHS.SumWetDitch
				,SA.Geometry.STIntersection(HS.Geometry)
				,@VersionLayoutV2Id
			FROM SubArea SA
				INNER JOIN HourSquare HS ON SA.Geometry.STIntersects(HS.Geometry) = 1
				INNER JOIN CTE_OldSubareaHourSquares OSAHS ON OSAHS.OldHourSquareId = HS.Id
			WHERE SA.Id IN (@SubAreaNewId)
	END

--update all other records
DECLARE @TableSubareaHourSquaresReplacements TABLE (
	OldSubareaHourSquareId UNIQUEIDENTIFIER NOT NULL
	,NewSubareaHourSquareId UNIQUEIDENTIFIER NOT NULL
	);

INSERT INTO @TableSubareaHourSquaresReplacements
	SELECT SAHS.Id
		,SAHSNew.Id
	FROM SubAreaHourSquare SAHS
		JOIN SubAreaHourSquare SAHSNew ON SAHSNew.HourSquareId = SAHS.HourSquareId
		INNER JOIN SubArea SA ON SA.Id = SAHS.SubAreaId
		INNER JOIN CatchArea CA ON CA.Id = SA.CatchAreaId
		INNER JOIN SubArea SANew ON SANew.Id = SAHSNew.SubAreaId
		INNER JOIN CatchArea CANew ON CANew.Id = SANew.CatchAreaId
	WHERE CA.Name IN (
			SELECT CatchAreaName
			FROM @TempTableSubArea
			)
		AND SAHS.SubAreaId IN (
			SELECT Id
			FROM SubArea
			WHERE Name NOT IN (
					SELECT NewSubAreaName
						FROM @TempTableSubArea
					)
			)
		AND SAHSNew.SubAreaId IN (
			SELECT Id
			FROM SubArea
			WHERE Name IN (
					SELECT NewSubAreaName
						FROM @TempTableSubArea
					)
			)
		AND CANew.Id = CA.Id
		
DECLARE @TableNewSubaereasHourSquares TABLE (
	RowId INT IDENTITY(1, 1) NOT NULL
	,NewSubareaHourSquareId UNIQUEIDENTIFIER NOT NULL
	);

INSERT INTO @TableNewSubaereasHourSquares
	SELECT DISTINCT tbl.NewSubareaHourSquareId
		FROM @TableSubareaHourSquaresReplacements tbl

DECLARE @TotalRecordsNewSubareaHourSquaresIds INT
DECLARE @NewSubareaHourSquareId UNIQUEIDENTIFIER
DECLARE @TableTimeRegistrationsForUpdate TABLE (
	RN INT NOT NULL
	,TimeRegistrationId UNIQUEIDENTIFIER NOT NULL
	,SumHours FLOAT NOT NULL
	,Status TINYINT NOT NULL
	,IsCreatedFromTrackings BIT NOT NULL
	);

SET @Index = 1

SELECT @TotalRecordsNewSubareaHourSquaresIds = COUNT(*)
				FROM @TableNewSubaereasHourSquares

WHILE (@Index <= @TotalRecordsNewSubareaHourSquaresIds)
	BEGIN
		SELECT @NewSubareaHourSquareId = NewSubareaHourSquareId
		FROM @TableNewSubaereasHourSquares
		WHERE RowId = @Index

		SET @Index = @Index + 1;

		INSERT INTO @TableTimeRegistrationsForUpdate
			SELECT ROW_NUMBER() OVER (
					PARTITION BY t.DATE
					,t.TrappingTypeId
					,t.UserId ORDER BY t.CreatedOn
					) AS RN
				,t.Id
				,Multiplied.SumHours
				,Multiplied.Status
				,Multiplied.IsCreatedFromTrackings
			FROM TimeRegistration t
			INNER JOIN (
				SELECT t.DATE
					,t.UserId
					,t.TrappingTypeId
					,COUNT(*) AS CountMultiplied
					,SUM(t.Hours) AS SumHours
					,MIN(t.Status) AS Status
					,MIN(t.IsCreatedFromTrackings + 0) AS IsCreatedFromTrackings
				FROM TimeRegistration t
				INNER JOIN @TableSubareaHourSquaresReplacements tbl ON tbl.OldSubareaHourSquareId = t.SubAreaHourSquareId
				WHERE tbl.NewSubareaHourSquareId = @NewSubareaHourSquareId
				GROUP BY t.UserId
					,t.Date
					,t.TrappingTypeId
				HAVING COUNT(*) > 1
				) AS Multiplied ON Multiplied.DATE = t.DATE
				AND Multiplied.TrappingTypeId = t.TrappingTypeId
				AND Multiplied.UserId = t.UserId
			WHERE t.SubAreaHourSquareId IN (
					SELECT OldSubareaHourSquareId
					FROM @TableSubareaHourSquaresReplacements
					WHERE NewSubareaHourSquareId = @NewSubareaHourSquareId
					)

		UPDATE t
		SET t.Hours = m.SumHours,
			t.Status = m.Status,
			t.IsCreatedFromTrackings = m.IsCreatedFromTrackings
		FROM TimeRegistration t
		INNER JOIN @TableTimeRegistrationsForUpdate m ON m.TimeRegistrationId = t.Id
		WHERE m.RN = 1

		DELETE TimeRegistration
		FROM TimeRegistration t
		INNER JOIN @TableTimeRegistrationsForUpdate m ON m.TimeRegistrationId = t.Id
		WHERE m.RN > 1;

		DELETE
		FROM @TableTimeRegistrationsForUpdate;
	END

UPDATE t
SET t.SubAreaHourSquareId = oldnew.NewSubareaHourSquareId
FROM TimeRegistration t
INNER JOIN @TableSubareaHourSquaresReplacements oldnew ON oldnew.OldSubareaHourSquareid = t.SubAreaHourSquareId

UPDATE t
SET t.SubAreaHourSquareId = oldnew.NewSubareaHourSquareId
FROM trap t
INNER JOIN @TableSubareaHourSquaresReplacements oldnew ON oldnew.OldSubareaHourSquareid = t.SubAreaHourSquareId

UPDATE o
SET o.SubAreaHourSquareId = oldnew.NewSubareaHourSquareId
FROM Observation o
INNER JOIN @TableSubareaHourSquaresReplacements oldnew ON oldnew.OldSubareaHourSquareid = o.SubAreaHourSquareId

UPDATE t
SET t.SubAreaHourSquareId = oldnew.NewSubareaHourSquareId
FROM TrapSubAreaHourSquare t
INNER JOIN @TableSubareaHourSquaresReplacements oldnew ON oldnew.OldSubareaHourSquareid = t.SubAreaHourSquareId

DELETE SubArea
FROM SubArea sa
INNER JOIN CatchArea ca ON ca.Id = sa.CatchAreaId
WHERE ca.Name IN (
		SELECT CatchAreaName
		FROM @TempTableSubArea
		)
	AND sa.Id IN (
		SELECT id
		FROM subarea
		WHERE name NOT IN (
				SELECT NewSubAreaName
				FROM @TempTableSubArea
				)
		)

--DELETE SubAreaHourSquare
--FROM SubAreaHourSquare s
--INNER JOIN @TableSubareaHourSquaresReplacements t ON t.OldSubareaHourSquareId = s.Id

	COMMIT TRANSACTION
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
    BEGIN
		--print 'rolling back';
        ROLLBACK TRANSACTION;
    END;
	THROW;
END CATCH