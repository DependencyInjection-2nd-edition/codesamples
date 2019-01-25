namespace Ploeh.Samples.Commerce.Domain
{
    // ---- Code Listing 10.12 ----
    public interface ICommandService<TCommand>
    {
        void Execute(TCommand command);
    }
}