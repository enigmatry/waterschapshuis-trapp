using JetBrains.Annotations;
using System;

namespace Waterschapshuis.CatchRegistration.DomainModel.Common
{
    [PublicAPI]
    public class FileUpload
    {
        public string Name { get; set; } = String.Empty;
        public string Size { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        public long LastModifiedTime { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string DataAsBase64 { get; set; } = String.Empty;
        public byte[] DataAsByteArray { get; set; } = new byte[] { };
    }
}
