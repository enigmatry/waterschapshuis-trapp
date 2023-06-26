using System;
using JetBrains.Annotations;
using Waterschapshuis.CatchRegistration.Core;

namespace Waterschapshuis.CatchRegistration.Infrastructure
{
    [UsedImplicitly]
    public class TimeProvider : ITimeProvider
    {
        private readonly Lazy<DateTimeOffset> _now = new Lazy<DateTimeOffset>(DateTimeOffset.Now);
        public DateTimeOffset Now => _now.Value;
    }
}
