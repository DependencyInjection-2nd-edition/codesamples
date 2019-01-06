namespace Ploeh.Samples.Commerce.Domain
{
    public interface ILocationService
    {
        Warehouse[] FindWarehouses();
    }

    public class Warehouse { }
}