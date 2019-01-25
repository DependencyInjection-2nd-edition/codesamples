using System;
using System.Security.Principal;
using Ploeh.Samples.HelloDI.Console;
using Ploeh.Samples.HelloDI.Tests.Fakes;
using Xunit;

namespace Ploeh.Samples.HelloDI.Tests
{
    public class SecureMessageWriterTests
    {
        // Tests missing? Send us a pull request.

        private static readonly IIdentity AuthenticatedIdentity = new TestIdentity { IsAuthenticated = true };
        private static readonly IIdentity AnonymousIdentity = new TestIdentity { IsAuthenticated = false };

        [Fact]
        public void SutIsMessageWriter()
        {
            Assert.IsAssignableFrom<IMessageWriter>(CreateSecureMessageWriter());
        }

        [Fact]
        public void InitializeWithNullWriterThrows()
        {
            // Act
            Action action = () => new SecureMessageWriter(writer: null, identity: WindowsIdentity.GetCurrent());

            // Arrange
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void InitializeWithNullIdentityThrows()
        {
            // Act
            Action action = () => new SecureMessageWriter(writer: new SpyMessageWriter(), identity: null);

            // Arrange
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void WriteInvokesDecoratedWriterWhenPrincipalIsAuthenticated()
        {
            // Arrange
            string expectedMessage = "Whatever";
            var writer = new SpyMessageWriter();

            SecureMessageWriter sut = CreateSecureMessageWriter(writer: writer, identity: AuthenticatedIdentity);

            // Act
            sut.Write(expectedMessage);

            // Assert
            Assert.Equal(expected: expectedMessage, actual: writer.WrittenMessage);
        }

        [Fact]
        public void WriteDoesNotInvokeWriterWhenPrincipalIsNotAuthenticated()
        {
            // Arrange
            var writer = new SpyMessageWriter();

            SecureMessageWriter sut = CreateSecureMessageWriter(writer: writer, identity: AnonymousIdentity);

            // Act
            sut.Write("Anonymous value");

            // Assert
            Assert.Equal(expected: 0, actual: writer.MessageCount);
        }

        private static SecureMessageWriter CreateSecureMessageWriter(
            IMessageWriter writer = null, IIdentity identity = null)
        {
            return new SecureMessageWriter(
                writer: writer ?? new SpyMessageWriter(),
                identity: identity ?? WindowsIdentity.GetCurrent());
        }

        public class TestIdentity : IIdentity
        {
            public string AuthenticationType { get; set; }
            public bool IsAuthenticated { get; set; }
            public string Name { get; set; }
        }
    }
}