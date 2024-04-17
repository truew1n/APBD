using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;


namespace LAB03
{
    public class ContainerShip
    {
        public List<Container> Containers { get; private set; } = new List<Container>();
        public double MaxSpeed { get; private set; }
        public int MaxContainerCount { get; private set; }
        public double MaxWeight { get; private set; } // Maksymalna waga w tonach

        public ContainerShip(double maxSpeed, int maxContainerCount, double maxWeight)
        {
            MaxSpeed = maxSpeed;
            MaxContainerCount = maxContainerCount;
            MaxWeight = maxWeight;
        }

        public void LoadContainer(Container container)
        {
            if (Containers.Count >= MaxContainerCount)
            {
                throw new InvalidOperationException("Cannot load more containers than the ship's capacity.");
            }

            if (Containers.Sum(c => c.CargoMass + c.TareWeight) + container.CargoMass + container.TareWeight > MaxWeight * 1000)
            {
                throw new InvalidOperationException("Loading this container would exceed the ship's maximum weight capacity.");
            }

            Containers.Add(container);
        }

        public void UnloadContainer(string serialNumber)
        {
            var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
            if (container != null)
            {
                Containers.Remove(container);
            }
        }

        // Możliwe jest dodanie więcej metod do obsługi wymaganych operacji, takich jak zamiana kontenerów, przenoszenie między statkami itd.
    }
}
