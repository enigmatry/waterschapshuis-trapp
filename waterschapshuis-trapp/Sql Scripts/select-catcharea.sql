SELECT
	cd.CatchAreaId as CatchAreaId,
	SUM(cd.CatchNumber) AS CatchNumber,
	SUM(cd.SubAreaHourSquareKmWaterway) as Km
FROM
	[report].[CatchData] cd
	JOIN CatchArea ca on ca.Id = cd.CatchAreaId
GROUP BY cd.CatchAreaId