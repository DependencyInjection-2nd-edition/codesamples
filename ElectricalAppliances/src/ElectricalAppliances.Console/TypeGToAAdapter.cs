using System;

namespace ElectricalAppliances.Console
{
    // UK (Type G) to USA (Type A) Adapter 
    public class TypeGToAAdapter : ITypeAPluggableAppliance
    {
        private readonly ITypeGPluggableAppliance appliance;

        public TypeAToGAdapter(ITypeGPluggableAppliance appliance)
        {
            if (appliance == null) throw new ArgumentNullException(nameof(appliance));

            this.appliance = appliance;
        }
    }
}