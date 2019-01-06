using System;
using Ploeh.Samples.Greeter.Console;
using Ploeh.Samples.Greeter.Tests.Fakes;
using Xunit;

namespace Ploeh.Samples.Greeter.Tests
{
    public class TitledGreeterDecoratorTests
    {
        [Fact]
        public void InitializeWithNullDecorateeThrows()
        {
            // Act
            Action action = () => new TitledGreeterDecorator(decoratee: null);

            // Arrange
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ProducesExpectedGreet()
        {
            // Arrange
            string name = "John";
            string expectedGreet = "Hello, Mr. John.";

            var sut = new TitledGreeterDecorator(new FormalGreeter());

            // Act
            string actualGreet = sut.Greet(name);

            // Assert
            Assert.Equal(expectedGreet, actualGreet);
        }

        [Fact]
        public void ForwardsTheCallToDecorateeWithTheNamePrefixedWithTheTitle()
        {
            // Arrange
            string name = "Peter";
            string expectedSuppliedTitledName = "Mr. Peter";

            var spy = new SpyGreeter();

            var sut = new TitledGreeterDecorator(spy);

            // Act
            sut.Greet(name);

            // Assert
            Assert.Equal(expectedSuppliedTitledName, spy.SuppliedName);
        }

        [Fact]
        public void CallsTheDecorateeOnce()
        {
            // Arrange
            var spy = new SpyGreeter();

            var sut = new TitledGreeterDecorator(spy);

            // Act
            sut.Greet("name");

            // Assert
            Assert.Equal(1, spy.CallCount);
        }
    }
}