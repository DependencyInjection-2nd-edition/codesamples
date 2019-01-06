using System;
using Ploeh.Samples.Greeter.Console;
using Xunit;

namespace Ploeh.Samples.Greeter.Tests
{
    public class NiceToMeetYouGreeterDecoratorTests
    {
        [Fact]
        public void InitializeWithNullDecorateeThrows()
        {
            // Act
            Action action = () => new NiceToMeetYouGreeterDecorator(decoratee: null);

            // Arrange
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void ProducesExpectedGreet()
        {
            // Arrange
            string name = "John";
            string expectedGreet = "Hello, John. Nice to meet you.";

            var sut = new NiceToMeetYouGreeterDecorator(new FormalGreeter());

            // Act
            string actualGreet = sut.Greet(name);

            // Assert
            Assert.Equal(expectedGreet, actualGreet);
        }
    }
}