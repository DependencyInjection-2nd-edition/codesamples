using System;

namespace ElectricalAppliances.Console
{
    // Composite
    public class FourOutletPowerStrip : ITypeAPluggableAppliance
    {
        private readonly ITypeAPluggableAppliance outlet1;
        private readonly ITypeAPluggableAppliance outlet2;
        private readonly ITypeAPluggableAppliance outlet3;
        private readonly ITypeAPluggableAppliance outlet4;

        public FourOutletPowerStrip(
            ITypeAPluggableAppliance outlet1,
            ITypeAPluggableAppliance outlet2,
            ITypeAPluggableAppliance outlet3,
            ITypeAPluggableAppliance outlet4)
        {
            if (outlet1 == null) throw new ArgumentNullException(nameof(outlet1));
            if (outlet2 == null) throw new ArgumentNullException(nameof(outlet2));
            if (outlet3 == null) throw new ArgumentNullException(nameof(outlet3));
            if (outlet4 == null) throw new ArgumentNullException(nameof(outlet4));

            this.outlet1 = outlet1;
            this.outlet2 = outlet2;
            this.outlet3 = outlet3;
            this.outlet4 = outlet4;
        }
    }
}