# Offline map update

Mb-util tool (**modifications of source code that were made are stated at the end of this document**) is part of this project and it is used for converting Netherland osm map from .mbtiles format to pbf x/y/z structured format to desired location.

## Prerequisites

You need to have [Python](https://www.python.org/downloads/) installed on your local machine.

You also need to download and login to [Microsoft Azure Storage Explorer](https://azure.microsoft.com/en-us/features/storage-explorer/) for offline-map zip file upload to blob storage.

## Getting the file

You should get the latest version of Netherland osm map from [openmaptiles](https://openmaptiles.com/downloads/europe/netherlands/) by clicking on download button next to "OpenStreetMap vector tiles" label.


You will be asked to choose purpose of your download and will have to login using your personal account.

Your download should start automatically after doing so.

## Converting the file

After file is downloaded open **cmd prompt**, position yourself inside mb-util root folder (ex. ```C:\Repo\waterschapshuis-catch-registration\mbtiles-to-pbfs\mbutil-master```) and use following mb-util command:

```
python mb-util --image_format=pbf "source_path" "destination_path"
```

**Note**: Make sure to include .mbtiles extension in "source" part (ex. ```C:\Users\user\Desktop\2017-07-03_europe_netherlands.mbtiles```)

**Note**: Destinations final directory shouldn't exist on disk (ex. ```C:\Users\user\Desktop\TestPbfs``` - make sure that "TestPbfs" directory doesn't exist on you filesystem. 

## Uploading pbfs to azure blob storage

Once conversion is done, go into destination directory (called TestPbfs in above example), select all 14 zoom levels and zip them into archive with name "**openMapPbfTiles.zip"**.

Use [Microsoft Azure Storage Explorer](https://azure.microsoft.com/en-us/features/storage-explorer/), login to your enigmatry account and upload zip file to appropriate storage account blob container.

## *ADDITIONAL INFO*  - Mb-util source code changes made

There is a known issue with mb-util, gzip compressed mbtiles and openlayers (mbtiles downloaded from [openmaptiles](https://openmaptiles.com/downloads/europe/netherlands/) are gzipped and openlayers cannot show em as such) solution to this issue can be found as part of following PR - https://github.com/mapbox/mbutil/pull/109. 

Source code of mb-util is changed as suggested in above mentioned pull request, modifications were made on file ```...\waterschapshuis-catch-registration\mbtiles-to-pbfs\mbutil-master\mbutil\util.py``` (imports and uncompress function were added).

**Note**: If you skip this step openlayers won't be able to preview compressed pbfs and you will start seeing Uncaught Error: Unimplemented type: 3.