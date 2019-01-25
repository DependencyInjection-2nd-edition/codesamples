using System;
using System.Transactions;
using Ploeh.Samples.Commerce.Domain;

namespace Ploeh.Samples.Commerce.SqlDataAccess.Aspects
{
    // ---- Code Listing 10.15 ----
    public class TransactionCommandServiceDecorator<TCommand>
        : ICommandService<TCommand>
    {
        private readonly ICommandService<TCommand> decoratee;

        public TransactionCommandServiceDecorator(
            ICommandService<TCommand> decoratee)
        {
            if (decoratee == null) throw new ArgumentNullException(nameof(decoratee));

            this.decoratee = decoratee;
        }

        public void Execute(TCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            using (var scope = new TransactionScope())
            {
                this.decoratee.Execute(command);

                scope.Complete();
            }
        }
    }
}