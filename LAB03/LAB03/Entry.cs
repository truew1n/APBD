namespace LAB03
{
    internal class Entry
    {
        static void Main(string[] args)
        {
            var ship = new ContainerShip(20, 100, 40000);

            try
            {
                var liquidContainer = new LiquidContainer(1000, 200, 100, 300, "KON-L-1", 5000, isHazardous: false);
                ship.LoadContainer(liquidContainer);
            }
            catch (OverfillException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Operational Error: {ex.Message}");
            }
        }
    }
}
