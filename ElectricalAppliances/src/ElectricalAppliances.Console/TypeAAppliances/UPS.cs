using System;

namespace ElectricalAppliances.Console.TypeAAppliances
{
    // Decorator
    public class UPS : ITypeAPluggableAppliance
    {
        private readonly ITypeAPluggableAppliance appliance;

        public UPS(ITypeAPluggableAppliance appliance)
        {
            if (appliance == null) throw new ArgumentNullException(nameof(appliance));

            this.appliance = appliance;
        }
    }
}