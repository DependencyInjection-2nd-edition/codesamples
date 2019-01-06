using System;

namespace ElectricalAppliances.Console
{
    // UK to USA Adapter 
    public class TypeAToGAdapter : ITypeAPluggableAppliance
    {
        private readonly ITypeGPluggableAppliance appliance;

        public TypeAToGAdapter(ITypeGPluggableAppliance appliance)
        {
            if (appliance == null) throw new ArgumentNullException(nameof(appliance));

            this.appliance = appliance;
        }
    }
}