namespace Ploeh.Samples.Commerce.Domain
{
    public interface IUserContext
    {
        bool IsInRole(Role role);
    }
}