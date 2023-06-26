using System.Threading.Tasks;

namespace Waterschapshuis.CatchRegistration.Scheduler.Services
{
    public interface ITrapCatchingNightsRecorder
    {
        Task TryRecord();
    }
}
