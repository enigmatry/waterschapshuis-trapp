using JetBrains.Annotations;
using System;
using Waterschapshuis.CatchRegistration.DomainModel.Common;

namespace Waterschapshuis.CatchRegistration.DomainModel.VersionRegionalLayouts
{
    [PublicAPI]
    public class ImportVersionRegionalLayout
    {
        public string Name { get; set; } = String.Empty;
        public FileUpload File { get; set; } = new FileUpload();
    }
}
