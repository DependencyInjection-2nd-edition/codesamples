using Ploeh.Samples.Commerce.Domain;

namespace Ploeh.Samples.Commerce.ExternalConnections
{
    // Dummy implementation
    public class WcfLocationService : ILocationService
    {
        public Warehouse[] FindWarehouses() => new[] { new Warehouse() };
    }
}