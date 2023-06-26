using System;
using System.Collections.Generic;
using Waterschapshuis.CatchRegistration.Core;

namespace Waterschapshuis.CatchRegistration.DomainModel.Maps
{
    public class OverlayLayer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = String.Empty;
        public string Workspace { get; private set; } = String.Empty;
        public OverlayLayerType Type { get; private set; }
        public OverlayLayerFormat Format { get; private set; }
        public OverlayLayerRequest Request { get; private set; }
        public ICollection<OverlayLayerLayerCategory> LayerCategories { get; private set; } = new List<OverlayLayerLayerCategory>();

        public static OverlayLayer Create(
            string name,
            string workspace = LayerConstants.WorkspaceName.V3,
            OverlayLayerType type = OverlayLayerType.Wfs,
            OverlayLayerFormat format = OverlayLayerFormat.Json,
            OverlayLayerRequest request = OverlayLayerRequest.Feature)
        {
            var result = new OverlayLayer
            {
                Name = name,
                Workspace = workspace,
                Type = type,
                Format = format,
                Request = request
            };

            return result;
        }
    }
}
