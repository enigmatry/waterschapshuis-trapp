using System.Collections.Generic;
using System.Threading.Tasks;

namespace Waterschapshuis.CatchRegistration.ApplicationServices.Common
{
    public class ListResponse<TItem>
    {
        public IEnumerable<TItem> Items { get; private set; } = new List<TItem>();

        public static async Task<ListResponse<TItem>> Create(Task<List<TItem>> items)
        {
            return new ListResponse<TItem> { Items = await items };
        }

        public static ListResponse<TItem> Create(List<TItem> items)
        {
            return new ListResponse<TItem> { Items = items };
        }
    }
}
