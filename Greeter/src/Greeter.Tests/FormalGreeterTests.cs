using Ploeh.Samples.Greeter.Console;
using Xunit;

namespace Ploeh.Samples.Greeter.Tests
{
    public class FormalGreeterTests
    {
        [Fact]
        public void ProducesExpectedGreet()
        {
            // Arrange
            string name = "John";
            string expectedGreet = "Hello, John.";

            var sut = new FormalGreeter();

            // Act
            string actualGreet = sut.Greet(name);

            // Assert
            Assert.Equal(expectedGreet, actualGreet);
        }
    }
}