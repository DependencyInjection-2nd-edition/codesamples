using System;
using Ploeh.Samples.Commerce.SqlDataAccess.Aspects;
using Ploeh.Samples.Commerce.SqlDataAccess.Tests.Unit.Fakes;
using Xunit;

namespace Ploeh.Samples.Commerce.SqlDataAccess.Tests.Unit.Aspects
{
    public class SaveChangesCommandServiceDecoratorTests
    {
        // Tests missing? Send us a pull request.

        [Fact]
        public void CreateWithNullContextWillThrow()
        {
            // Act
            Action action = () => new SaveChangesCommandServiceDecorator<object>(
                context: null,
                decoratee: new StubCommandService<object>());

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void CreateWithNullDecorateeWillThrow()
        {
            // Act
            Action action = () => new SaveChangesCommandServiceDecorator<object>(
                context: new SpyCommerceContext(),
                decoratee: null);

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ExecuteWillSaveChangesAfterInvokingTheDecoratee()
        {
            // Arrange
            var context = new SpyCommerceContext();
            var decoratee = new SpyCommandService<object>();

            var sut = new SaveChangesCommandServiceDecorator<object>(context, decoratee);

            decoratee.Executed += () =>
            {
                // Decoratee should be invoked before changes are saved
                Assert.False(context.HasChangedSaved);
            };

            // Act
            sut.Execute(command: new object());

            // Assert
            Assert.True(context.HasChangedSaved);
            Assert.True(decoratee.ExecutedOnce);
        }
    }
}