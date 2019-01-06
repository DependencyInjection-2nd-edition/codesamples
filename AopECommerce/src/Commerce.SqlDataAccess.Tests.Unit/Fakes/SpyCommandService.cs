using System;
using System.Collections.Generic;
using Ploeh.Samples.Commerce.Domain;
using Xunit;

namespace Ploeh.Samples.Commerce.SqlDataAccess.Tests.Unit.Fakes
{
    public class SpyCommandService<TCommand> : ICommandService<TCommand>
    {
        public List<TCommand> ExecutedCommands { get; } = new List<TCommand>();

        public bool ExecutedOnce => this.ExecutedCommands.Count == 1;

        public event Action Executed = () => { };

        public void Execute(TCommand command)
        {
            Assert.NotNull(command);

            this.ExecutedCommands.Add(command);

            this.Executed();
        }
    }
}