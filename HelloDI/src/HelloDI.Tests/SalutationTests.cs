using Ploeh.Samples.HelloDI.Console;
using Ploeh.Samples.HelloDI.Tests.Fakes;
using System;
using Xunit;

namespace Ploeh.Samples.HelloDI.Tests
{
    public class SalutationTests
    {
        // Tests missing? Send us a pull request.

        // ---- Code Listing 1.4 ----
        [Fact]
        public void ExclaimWillWriteCorrectMessageToMessageWriter()
        {
            // Arrange
            var writer = new SpyMessageWriter();

            var sut = new Salutation(writer);

            // Act
            sut.Exclaim();

            // Assert
            Assert.Equal(
                expected: "Hello DI!",
                actual: writer.WrittenMessage);
        }

        [Fact]
        public void NullWriterInConstructorThrowsException()
        {
            // Arrange
            Action action = () => new Salutation(writer: null);

            // Assert
            var exception = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("Value cannot be null.\r\nParameter name: writer", exception.Message);
        }
    }
}