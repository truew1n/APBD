using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB03
{
    public abstract class Container
    {
        public double CargoMass { get; protected set; }
        public int Height { get; private set; }
        public double TareWeight { get; private set; } // Waga własna kontenera
        public int Depth { get; private set; }
        public string SerialNumber { get; private set; }
        public double MaxLoad { get; private set; }

        public Container(double cargoMass, int height, double tareWeight, int depth, string serialNumber, double maxLoad)
        {
            CargoMass = cargoMass;
            Height = height;
            TareWeight = tareWeight;
            Depth = depth;
            SerialNumber = serialNumber;
            MaxLoad = maxLoad;
        }

        public abstract void LoadCargo(double mass);
        public abstract void UnloadCargo();
    }

}
