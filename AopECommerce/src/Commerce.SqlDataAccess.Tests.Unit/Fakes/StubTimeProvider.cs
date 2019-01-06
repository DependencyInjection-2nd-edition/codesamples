using System;
using Ploeh.Samples.Commerce.Domain;

namespace Ploeh.Samples.Commerce.SqlDataAccess.Tests.Unit.Fakes
{
    public class StubTimeProvider : ITimeProvider
    {
        public DateTime Now { get; set; } = DateTime.Parse("2019-01-01");
    }
}