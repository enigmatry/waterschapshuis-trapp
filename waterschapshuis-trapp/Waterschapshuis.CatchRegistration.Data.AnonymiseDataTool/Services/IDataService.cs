using System.Threading;
using System.Threading.Tasks;

namespace Waterschapshuis.CatchRegistration.Data.AnonymiseDataTool.Services
{
    public interface IDataService
    {
        Task RunProcessingAsync(CancellationToken cancellationToken);
    }
}
