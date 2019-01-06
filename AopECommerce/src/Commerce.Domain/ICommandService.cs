namespace Ploeh.Samples.Commerce.Domain
{
    public interface ICommandService<TCommand>
    {
        void Execute(TCommand command);
    }
}