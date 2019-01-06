using Ploeh.Samples.Commerce.Domain;

namespace Ploeh.Samples.Commerce.SqlDataAccess.Tests.Unit.Fakes
{
    public class StubCommandService<TCommand> : ICommandService<TCommand>
    {
        public void Execute(TCommand command)
        {
        }
    }
}