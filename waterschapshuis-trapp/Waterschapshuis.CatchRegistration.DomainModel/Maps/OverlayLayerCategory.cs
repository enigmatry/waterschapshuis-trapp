using System;
using System.Collections.Generic;

namespace Waterschapshuis.CatchRegistration.DomainModel.Maps
{
    public class OverlayLayerCategory
    {
        public Guid Id { get; private set; }
        public OverlayLayerCategoryCode Code { get; private set; }
        public string DisplayName { get; private set; } = String.Empty;
        public ICollection<OverlayLayerLayerCategory> CategoryLayers { get; private set; } = new List<OverlayLayerLayerCategory>();

        public static OverlayLayerCategory Create(OverlayLayerCategoryCode code, string displayName)
        {
            var result = new OverlayLayerCategory
            {
                Code = code,
                DisplayName = displayName
            };

            return result;
        }
    }
}
