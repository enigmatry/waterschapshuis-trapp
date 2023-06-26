using System.Threading.Tasks;

namespace Waterschapshuis.CatchRegistration.ApplicationServices
{
   public interface ITemplatingEngine
    {
        Task<string> RenderFromFileAsync<T>(string path, T model);
    }
}
