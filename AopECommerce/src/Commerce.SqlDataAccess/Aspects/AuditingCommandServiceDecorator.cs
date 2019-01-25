using System;
using Ploeh.Samples.Commerce.Domain;

namespace Ploeh.Samples.Commerce.SqlDataAccess.Aspects
{
    // ---- Code Listing 10.16 ----
    // In contrast to listing 10.16, this decorator does not call SaveChanges anymore.
    // This is moved to the SaveChangesCommandServiceDecorator<TCommand>.
    public class AuditingCommandServiceDecorator<TCommand> : ICommandService<TCommand>
    {
        private readonly IUserContext userContext;
        private readonly ITimeProvider timeProvider;
        private readonly CommerceContext context;
        private readonly ICommandService<TCommand> decoratee;

        public AuditingCommandServiceDecorator(
            IUserContext userContext,
            ITimeProvider timeProvider,
            CommerceContext context,
            ICommandService<TCommand> decoratee)
        {
            if (userContext == null) throw new ArgumentNullException(nameof(userContext));
            if (timeProvider == null) throw new ArgumentNullException(nameof(timeProvider));
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (decoratee == null) throw new ArgumentNullException(nameof(decoratee));

            this.userContext = userContext;
            this.timeProvider = timeProvider;
            this.context = context;
            this.decoratee = decoratee;
        }

        public void Execute(TCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            this.decoratee.Execute(command);
            this.AppendToAuditTrail(command);
        }

        private void AppendToAuditTrail(TCommand command)
        {
            var entry = new AuditEntry
            {
                UserId = this.userContext.CurrentUserId,
                TimeOfExecution = this.timeProvider.Now,
                Operation = command.GetType().Name,
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(command)
            };

            this.context.AuditEntries.Add(entry);
        }
    }
}