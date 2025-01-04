using ConsoleAppSquareMaster.Helper;
using ConsoleAppSquareMaster.Model;

namespace ConsoleAppSquareMaster
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Start met simulaties vanuit de database...");


            MongoDBHelper dbHelper = new MongoDBHelper();
            AnalyzeHelper analyze = new AnalyzeHelper(dbHelper);


            await analyze.RunSimulationsFromDatabase(3);


            var averages = await AnalyzeHelper.CalculateAveragePerformance(dbHelper);
            foreach (var strategy in averages)
            {
                Console.WriteLine($"Gemiddelde bezetting voor {strategy.Key}: {strategy.Value:F2} cellen");
            }

            Console.WriteLine("Analyse voltooid!");
        }
    }
}
