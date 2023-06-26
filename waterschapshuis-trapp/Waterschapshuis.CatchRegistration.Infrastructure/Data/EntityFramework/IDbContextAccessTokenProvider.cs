using System.Threading.Tasks;

namespace Waterschapshuis.CatchRegistration.Infrastructure.Data.EntityFramework
{
    public interface IDbContextAccessTokenProvider
    {
        Task<string> GetAccessTokenAsync();
    }
}
