namespace Ploeh.Samples.Commerce.SqlDataAccess.Tests.Unit.Fakes
{
    public class SpyCommerceContext : CommerceContext
    {
        public SpyCommerceContext() : base("Non-empty value")
        {
        }

        public bool HasChangedSaved { get; private set; }

        public override int SaveChanges()
        {
            this.HasChangedSaved = true;
            return 0;
        }
    }
}