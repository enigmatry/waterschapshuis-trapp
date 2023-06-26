using System;
using JetBrains.Annotations;

namespace Waterschapshuis.CatchRegistration.Core.Settings
{
    [UsedImplicitly]
    public class UserSessionSettings
    {
        public bool SessionsEnabled { get; set; } = true;
        public TimeSpan SessionDurationTimespan { get; set; }
        public UserSessionOrigin SessionOrigin { get; set; }
    }
}
