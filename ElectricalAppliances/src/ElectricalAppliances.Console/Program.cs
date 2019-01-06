using ElectricalAppliances.Console.TypeAAppliances;
using ElectricalAppliances.Console.TypeGAppliances;

namespace ElectricalAppliances.Console
{
    public static class Program
    {
        public static void Main()
        {
            ITypeAPluggableAppliance appliance =
                new FourOutletPowerStrip(
                    outlet1: new UPS(new Computer()),
                    outlet2: new HairDryer(),
                    outlet3: new TypeAToGAdapter(new UKCamera()),
                    outlet4: new ChildrensSafetyOutletPlug());

            var room = new HotelRoom(appliance);
        }
    }
}