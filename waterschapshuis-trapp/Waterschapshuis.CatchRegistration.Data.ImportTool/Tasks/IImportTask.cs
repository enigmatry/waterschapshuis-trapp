using System.Threading;
using System.Threading.Tasks;

namespace Waterschapshuis.CatchRegistration.Data.ImportTool.Tasks
{
    public interface IImportTask
    {
        Task ExecuteImportAsync(CancellationToken cancellationToken);
    }
}
