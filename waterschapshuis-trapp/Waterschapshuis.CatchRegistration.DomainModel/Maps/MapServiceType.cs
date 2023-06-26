namespace Waterschapshuis.CatchRegistration.DomainModel.Maps
{
    public enum MapServiceType
    {
        Wmts = 1, // Web Map Tile Service
        Mvt = 2 // Mapbox Vector Tiles
    }

    public enum MapNetworkType
    {
        Online = 1, 
        Offline = 2
    }
}
