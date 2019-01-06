using System;
using Ploeh.Samples.Commerce.Domain;

namespace Ploeh.Samples.Commerce.SqlDataAccess.Aspects
{
    // This decorator is in charge of calling SaveChanges on the DbContext. This way,
    // business operations and repositories don't have to call save changes.
    public class SaveChangesCommandServiceDecorator<TCommand> : ICommandService<TCommand>
    {
        private readonly CommerceContext context;
        private readonly ICommandService<TCommand> decoratee;

        public SaveChangesCommandServiceDecorator(
            CommerceContext context, ICommandService<TCommand> decoratee)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (decoratee == null) throw new ArgumentNullException(nameof(decoratee));

            this.context = context;
            this.decoratee = decoratee;
        }

        public void Execute(TCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            this.decoratee.Execute(command);

            this.context.SaveChanges();
        }
    }
}