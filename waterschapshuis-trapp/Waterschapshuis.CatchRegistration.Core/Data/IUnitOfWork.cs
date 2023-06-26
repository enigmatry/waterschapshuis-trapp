using System.Threading;
using System.Threading.Tasks;

namespace Waterschapshuis.CatchRegistration.Core.Data
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        void CancelSaving();
        public void Detach();
    }
}
