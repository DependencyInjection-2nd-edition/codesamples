using System;
using Ploeh.Samples.Greeter.Console;
using Ploeh.Samples.Greeter.Tests.Fakes;
using Xunit;

namespace Ploeh.Samples.Greeter.Tests
{
    public class SimpleDecoratorTests
    {
        [Fact]
        public void InitializeWithNullDecorateeThrows()
        {
            // Act
            Action action = () => new SimpleDecorator(decoratee: null);

            // Arrange
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ProducesExpectedGreet()
        {
            // Arrange
            string name = "John";
            string expectedGreet = "<Test Greet>";

            var spy = new SpyGreeter { ReturnedGreet = expectedGreet };

            var sut = new SimpleDecorator(spy);

            // Act
            string actualGreet = sut.Greet(name);

            // Assert
            Assert.Equal(expectedGreet, actualGreet);
        }

        [Fact]
        public void ForwardsTheCallToDecorateeWithTheName()
        {
            // Arrange
            string name = "Christelle";

            var spy = new SpyGreeter();

            var sut = new SimpleDecorator(spy);

            // Act
            sut.Greet(name);

            // Assert
            Assert.Equal(name, spy.SuppliedName);
        }

        [Fact]
        public void CallsTheDecorateeOnce()
        {
            // Arrange
            var spy = new SpyGreeter();

            var sut = new SimpleDecorator(spy);

            // Act
            sut.Greet("name");

            // Assert
            Assert.Equal(1, spy.CallCount);
        }
    }
}