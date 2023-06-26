using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Infrastructure
{
    public interface IJsonReader
    {
        IAsyncEnumerable<IJsonResult> ReadAsync(string path,
            Regex regex = default,
            CancellationToken cancellationToken = default);
    }
}
