using System;
using System.Linq;
using Ploeh.Samples.Commerce.Domain;
using Ploeh.Samples.Commerce.SqlDataAccess.Aspects;
using Ploeh.Samples.Commerce.SqlDataAccess.Tests.Unit.Fakes;
using Xunit;

namespace Ploeh.Samples.Commerce.SqlDataAccess.Tests.Unit.Aspects
{
    public class AuditingCommandServiceDecoratorTests
    {
        // Tests missing? Send us a pull request.

        [Fact]
        public void ForwardsTheCallToTheDecoratee()
        {
            // Arrange
            var decoratee = new SpyCommandService<object>();

            AuditingCommandServiceDecorator<object> sut =
                CreateAuditingDecorator(decoratee: decoratee);

            // Act
            sut.Execute(command: new object());

            // Assert
            Assert.True(decoratee.ExecutedOnce);
        }

        [Fact]
        public void AppendsAuditEntryWithExpectedUserId()
        {
            // Arrange
            var context = new SpyCommerceContext();
            var userContext = new StubUserContext { CurrentUserId = Guid.NewGuid() };

            AuditingCommandServiceDecorator<object> sut =
                CreateAuditingDecorator<object>(userContext: userContext, context: context);

            // Act
            sut.Execute(command: new object());

            // Assert
            Assert.Equal(
                expected: userContext.CurrentUserId,
                actual: GetAppendedAuditEntry(context).UserId);
        }

        [Fact]
        public void AppendsAuditEntryWithExpectedTimeOfExecution()
        {
            // Arrange
            var context = new SpyCommerceContext();
            var timeProvider = new StubTimeProvider { Now = DateTime.Parse("2018-12-17") };

            AuditingCommandServiceDecorator<object> sut =
                CreateAuditingDecorator<object>(timeProvider: timeProvider, context: context);

            // Act
            sut.Execute(command: new object());

            // Assert
            Assert.Equal(
                expected: timeProvider.Now,
                actual: GetAppendedAuditEntry(context).TimeOfExecution);
        }

        private static AuditEntry GetAppendedAuditEntry(CommerceContext context)
        {
            var entries = context.ChangeTracker.Entries<AuditEntry>();
            Assert.Single(entries);
            return entries.Single().Entity;
        }

        private static AuditingCommandServiceDecorator<TCommand> CreateAuditingDecorator<TCommand>(
            IUserContext userContext = null,
            ITimeProvider timeProvider = null,
            CommerceContext context = null,
            ICommandService<TCommand> decoratee = null)
        {
            return new AuditingCommandServiceDecorator<TCommand>(
                userContext: userContext ?? new StubUserContext(),
                timeProvider: timeProvider ?? new StubTimeProvider(),
                context: context ?? new SpyCommerceContext(),
                decoratee: decoratee ?? new StubCommandService<TCommand>());
        }
    }
}
