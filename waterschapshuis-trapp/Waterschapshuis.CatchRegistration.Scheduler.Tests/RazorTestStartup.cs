using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Waterschapshuis.CatchRegistration.Scheduler.Infrastructure;

namespace Waterschapshuis.CatchRegistration.Scheduler.Tests
{
    // only needed so that startup is in the same assembly as the host
    // this way we avoid Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation.CompilationFailedException in the tests
    public class RazorTestStartup : RazorConsoleStartup
    {
        public RazorTestStartup(IConfiguration configuration, IHostEnvironment environment): base(configuration, environment)
        {
        }
    }
}
