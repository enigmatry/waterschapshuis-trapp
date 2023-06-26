using System.Threading;
using System.Threading.Tasks;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Services
{
    public interface IImportDataService
    {
        Task RunImportAsync(CancellationToken cancellationToken);
    }
}
