using System;

namespace Waterschapshuis.CatchRegistration.Core
{
    public interface ITimeProvider
    {
        DateTimeOffset Now { get; }
    }
}
