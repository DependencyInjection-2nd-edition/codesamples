using System;
using Xunit;

namespace Ploeh.Samples.Commerce.SqlDataAccess.Tests.Integration
{
    public class SqlOrderRepositoryTests
    {
        // Tests missing? Send us a pull request.

        [Fact]
        public void CreateWithNullContextWillThrow()
        {
            // Act
            Action action = () => new SqlOrderRepository(context: null);

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }
    }
}