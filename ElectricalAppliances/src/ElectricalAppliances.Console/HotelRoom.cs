using System;

namespace ElectricalAppliances.Console
{
    public class HotelRoom
    {
        private readonly ITypeAPluggableAppliance outlet;

        public HotelRoom(ITypeAPluggableAppliance outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));

            this.outlet = outlet;
        }
    }
}
