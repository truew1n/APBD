using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB03
{
    public class RefrigeratedContainer : Container
    {
        public string ProductType { get; private set; }
        public double Temperature { get; private set; }

        public RefrigeratedContainer(double cargoMass, int height, double tareWeight, int depth, string serialNumber, double maxLoad, string productType, double temperature)
            : base(cargoMass, height, tareWeight, depth, serialNumber, maxLoad)
        {
            ProductType = productType;
            Temperature = temperature;
        }

        public override void LoadCargo(double mass)
        {
            if (mass > MaxLoad)
            {
                throw new OverfillException("Cannot load beyond max capacity.");
            }
            CargoMass = mass;
        }

        public override void UnloadCargo()
        {
            CargoMass = 0;
        }
    }
}
