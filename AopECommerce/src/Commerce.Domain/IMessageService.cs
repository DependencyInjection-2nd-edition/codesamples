namespace Ploeh.Samples.Commerce.Domain
{
    public interface IMessageService
    {
        void SendTermsAndConditions(string mailAddress, string text);
    }
}