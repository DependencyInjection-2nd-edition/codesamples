using System.Diagnostics;
using Ploeh.Samples.Commerce.Domain;

namespace Ploeh.Samples.Commerce.ExternalConnections
{
    public class SmtpMailMessageService : IMessageService
    {
        public SmtpMailMessageService()
        {
        }

        public void SendTermsAndConditions(string mailAddress, string text)
        {
            // TODO Implementation
            Debug.WriteLine($"Mail sent to {mailAddress}.");
        }
    }
}