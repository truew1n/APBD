using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB03
{
    public class LiquidContainer : Container, IHazardNotifier
    {
        public bool IsHazardous { get; private set; }

        public LiquidContainer(double cargoMass, int height, double tareWeight, int depth, string serialNumber, double maxLoad, bool isHazardous)
            : base(cargoMass, height, tareWeight, depth, serialNumber, maxLoad)
        {
            IsHazardous = isHazardous;
        }

        public override void LoadCargo(double mass)
        {
            double maxCapacity = IsHazardous ? MaxLoad * 0.5 : MaxLoad * 0.9;
            if (mass > maxCapacity)
            {
                SendHazardNotification($"Attempted to overload a {(IsHazardous ? "hazardous" : "non-hazardous")} liquid container.");
                throw new OverfillException("Cannot load beyond max capacity.");
            }
            CargoMass = mass;
        }

        public override void UnloadCargo()
        {
            CargoMass = 0;
        }

        public void SendHazardNotification(string message)
        {
            Console.WriteLine($"Hazard Notification for {SerialNumber}: {message}");
        }
    }
}
