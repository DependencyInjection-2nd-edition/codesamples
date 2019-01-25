namespace Ploeh.Samples.Commerce.Domain
{
    // ---- Code snippet after Listing 3.9 ----
    public interface IUserContext
    {
        bool IsInRole(Role role);
    }
}