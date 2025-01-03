using ConsoleAppSquareMaster.Helper;
using ConsoleAppSquareMaster.Model;

namespace ConsoleAppSquareMaster
{
    internal class Program
    {
        static void Main(string[] args)
        {
            World Worldgenrator = new World(100, 100, 0.60);
            WorldConquer conquer = new WorldConquer(generatedWorld);

            List<Empire> empires = new()
            {
                EmpireFactory.CreateEmpire(1, "Conquer1"),
                EmpireFactory.CreateEmpire(2, "Conquer1"),
                EmpireFactory.CreateEmpire(3, "Conquer3"),
                EmpireFactory.CreateEmpire(4, "Conquer2")
            };

            foreach (var empire in empires)
            {
                empire.ExecuteConquer(conquer, 10000);
            }

            // Bereken en sla resultaten op
            var worldResult = CalculateResults(conquer.GetWorldEmpires(), worldModel, empires);
            await dbHelper.SaveWorldResultAsync(worldResult);

            Console.WriteLine("Resultaten succesvol opgeslagen in MongoDB.");
        }
    }
}
