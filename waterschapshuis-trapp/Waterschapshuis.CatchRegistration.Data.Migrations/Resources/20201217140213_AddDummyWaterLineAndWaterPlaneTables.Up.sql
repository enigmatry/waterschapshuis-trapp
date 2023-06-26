﻿IF OBJECT_ID('dbo.water_lines', 'U') IS NULL 
CREATE TABLE  [dbo].[water_lines](
	[ogr_fid] [int] NOT NULL,
	[ogr_geometry] [geometry] NULL,
	[gml_id] [nvarchar](max) NOT NULL,
	[namespace] [nvarchar](max) NULL,
	[lokaalid] [bigint]  NULL,
	[brontype] [nvarchar](max) NULL,
	[bronactualiteit] [nvarchar](max) NULL,
	[bronbeschrijving] [nvarchar](max) NULL,
	[bronnauwkeurigheid] [float] NULL,
	[objectbegintijd] [nvarchar](max) NULL,
	[tijdstipregistratie] [nvarchar](max) NULL,
	[tdncode] [bigint] NULL,
	[visualisatiecode] [bigint] NULL,
	[typewater] [nvarchar](max) NULL,
	[hoofdafwatering] [nvarchar](max) NULL,
	[hoogteniveau] [bigint] NULL,
	[functie] [nvarchar](max) NULL,
	[getijdeinvloed] [nvarchar](max) NULL,
	[mutatietype] [nvarchar](max) NULL,
	[naamnl] [nvarchar](max) NULL,
	[breedteklasse] [nvarchar](max) NULL,
	[fysiekvoorkomen] [nvarchar](max) NULL,
	[voorkomen] [nvarchar](max) NULL,
	[sluisnaam] [nvarchar](max) NULL,
	[naamOfficieel] [nvarchar](max) NULL,
	[naamFries] [nvarchar](max) NULL,
	[naamNL1] [nvarchar](max) NULL,
	[naamNL2] [nvarchar](max) NULL,
	[brugnaam] [nvarchar](max) NULL,
	[isBAGnaam] [nvarchar](max) NULL,
	[vaarwegklasse] [nvarchar](max) NULL,
	[naamFries1] [nvarchar](max) NULL,
	[naamFries2] [nvarchar](max) NULL,
	[naamNL3] [nvarchar](max) NULL,
	[naamFries3] [nvarchar](max) NULL)

IF OBJECT_ID('dbo.water_planes', 'U') IS NULL 
CREATE TABLE [dbo].[water_planes](
	[ogr_fid] [int] NOT NULL,
	[ogr_geometry] [geometry] NULL,
	[gml_id] [nvarchar](max) NOT NULL,
	[namespace] [nvarchar](max) NULL,
	[lokaalid] [bigint] NULL,
	[brontype] [nvarchar](max) NULL,
	[bronactualiteit] [nvarchar](max) NULL,
	[bronbeschrijving] [nvarchar](max) NULL,
	[bronnauwkeurigheid] [float] NULL,
	[objectbegintijd] [nvarchar](max) NULL,
	[tijdstipregistratie] [nvarchar](max) NULL,
	[tdncode] [bigint] NULL,
	[visualisatiecode] [bigint] NULL,
	[typewater] [nvarchar](max) NULL,
	[hoofdafwatering] [nvarchar](max) NULL,
	[hoogteniveau] [bigint] NULL,
	[functie] [nvarchar](max) NULL,
	[getijdeinvloed] [nvarchar](max) NULL,
	[mutatietype] [nvarchar](max) NULL,
	[naamnl] [nvarchar](max) NULL,
	[breedteklasse] [nvarchar](max) NULL,
	[fysiekvoorkomen] [nvarchar](max) NULL,
	[voorkomen] [nvarchar](max) NULL,
	[sluisnaam] [nvarchar](max) NULL,
	[naamOfficieel] [nvarchar](max) NULL,
	[naamFries] [nvarchar](max) NULL,
	[naamNL1] [nvarchar](max) NULL,
	[naamNL2] [nvarchar](max) NULL,
	[brugnaam] [nvarchar](max) NULL,
	[isBAGnaam] [nvarchar](max) NULL,
	[vaarwegklasse] [nvarchar](max) NULL,
	[naamFries1] [nvarchar](max) NULL,
	[naamFries2] [nvarchar](max) NULL,
	[naamNL3] [nvarchar](max) NULL,
	[naamFries3] [nvarchar](max) NULL)