namespace Waterschapshuis.CatchRegistration.DomainModel.Maps
{
    public enum OverlayLayerCacheStrategy
    {
        NoCache = 0,

        // Cache falling back to network
        CacheFirst = 1,

        // Network falling back to cache
        NetworkFirst = 2
    }
}
