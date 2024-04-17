using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB03
{
    public class GasContainer : Container, IHazardNotifier
    {
        public double Pressure { get; private set; } // Ciśnienie w atmosferach

        public GasContainer(double cargoMass, int height, double tareWeight, int depth, string serialNumber, double maxLoad, double pressure)
            : base(cargoMass, height, tareWeight, depth, serialNumber, maxLoad)
        {
            Pressure = pressure;
        }

        public override void LoadCargo(double mass)
        {
            if (mass > MaxLoad)
            {
                SendHazardNotification("Attempted to overload a gas container.");
                throw new OverfillException("Cannot load beyond max capacity.");
            }
            CargoMass = mass;
        }

        public override void UnloadCargo()
        {
            CargoMass *= 0.05; // Pozostaw 5% ładunku
        }

        public void SendHazardNotification(string message)
        {
            Console.WriteLine($"Hazard Notification for {SerialNumber}: {message}");
        }
    }
}
