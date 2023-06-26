using System.Collections.Generic;
using System.Threading.Tasks;

namespace Waterschapshuis.CatchRegistration.Core.Email
{
    public interface IEmailClient
    {
        void Send(EmailMessage email);

        Task SendBulkAsync(IEnumerable<EmailMessage> emails);
    }
}
