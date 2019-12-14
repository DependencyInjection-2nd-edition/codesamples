﻿using System;
using Ploeh.Samples.HelloDI.Console;
using Ploeh.Samples.HelloDI.Tests.Fakes;
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
            var exception = Assert.Throws<ArgumentNullException>(() => new Salutation(null));
            Assert.Equal("Value cannot be null.\r\nParameter name: writer", exception.Message);
        }
    }
}