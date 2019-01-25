using System;
using System.Reflection;
using System.Security;
using Ploeh.Samples.Commerce.Domain;

namespace Ploeh.Samples.Commerce.SqlDataAccess.Aspects
{
    // ---- Code Listing 10.19 ----
    public class SecureCommandServiceDecorator<TCommand>
        : ICommandService<TCommand>
    {
        private static readonly Role PermittedRole = GetPermittedRole();

        private readonly IUserContext userContext;
        private readonly ICommandService<TCommand> decoratee;

        public SecureCommandServiceDecorator(
            IUserContext userContext,
            ICommandService<TCommand> decoratee)
        {
            if (userContext == null) throw new ArgumentNullException(nameof(userContext));
            if (decoratee == null) throw new ArgumentNullException(nameof(decoratee));

            this.decoratee = decoratee;
            this.userContext = userContext;
        }

        public void Execute(TCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            this.CheckAuthorization();
            this.decoratee.Execute(command);
        }

        private void CheckAuthorization()
        {
            if (!this.userContext.IsInRole(PermittedRole))
            {
                throw new SecurityException();
            }
        }

        private static Role GetPermittedRole()
        {
            var attribute = typeof(TCommand)
                .GetCustomAttribute<PermittedRoleAttribute>();

            if (attribute == null)
            {
                throw new InvalidOperationException(
                    $"[PermittedRole] missing from {typeof(TCommand).Name}.");
            }

            return attribute.Role;
        }
    }
}