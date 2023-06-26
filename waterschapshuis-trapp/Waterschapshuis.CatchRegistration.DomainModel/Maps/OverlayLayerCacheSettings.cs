namespace Waterschapshuis.CatchRegistration.DomainModel.Maps
{
    public class OverlayLayerCacheSettings
    {
        public OverlayLayerCacheStrategy CacheStrategy { get; set; }
        public long DurationSeconds { get; set; }

        public static OverlayLayerCacheSettings NoCache()
        {
            return new OverlayLayerCacheSettings {CacheStrategy = OverlayLayerCacheStrategy.NoCache};
        }

        public static OverlayLayerCacheSettings CacheForSeconds(long seconds)
        {
            return new OverlayLayerCacheSettings
            {
                CacheStrategy = OverlayLayerCacheStrategy.CacheFirst, DurationSeconds = seconds
            };
        }

        public static OverlayLayerCacheSettings CacheForMinutes(long minutes) => CacheForSeconds(minutes * 60);

        public static OverlayLayerCacheSettings CacheForHours(long hours) => CacheForMinutes(hours * 60);

        public static OverlayLayerCacheSettings CacheForDays(long days) => CacheForHours(days * 24);
    }
}
