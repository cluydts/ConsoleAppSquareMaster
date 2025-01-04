using ConsoleAppSquareMaster.Model;
using ConsoleAppSquareMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster.Helper
{
    public class AnalyzeHelper
    {
        private readonly MongoDBHelper _dbHelper;

        public AnalyzeHelper(MongoDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public static WorldResult AnalyzeWorld(int[,] worldEmpires, WorldModel world, List<Empire> empires)
        {
            var result = new WorldResult
            {
                WorldId = world.Id.ToString(),
            };

            int totalCells = world.MaxX * world.MaxY;

            foreach (var empire in empires)
            {
                int conqueredCells = 0;

                // Tel veroverde cellen
                for (int x = 0; x < world.MaxX; x++)
                {
                    for (int y = 0; y < world.MaxY; y++)
                    {
                        if (worldEmpires[x, y] == empire.Id)
                        {
                            conqueredCells++;
                        }
                    }
                }

                double percentage = (double)conqueredCells / totalCells * 100;

                result.Empires.Add(new EmpireResult
                {
                    EmpireId = empire.Id,
                    Strategy = empire.Strategy.GetType().Name,
                    ConqueredCells = conqueredCells,
                    PercentageOfWorld = percentage
                });
            }

            return result;
        }

        public async Task RunSimulationsFromDatabase(int simulationsPerWorld)
        {
            // Haal alle werelden op
            var worlds = await _dbHelper.GetAllWorldModelsAsync();

            foreach (var world in worlds)
            {


                for (int sim = 0; sim < simulationsPerWorld; sim++)
                {



                    var worldCopy = (bool[,])world.Data.Clone();
                    WorldConquer conquer = new WorldConquer(worldCopy);


                    List<Empire> empires = new()
                    {
                        EmpireFactory.CreateEmpire(1, "Conquer1"),
                        EmpireFactory.CreateEmpire(2, "Conquer2"),
                        EmpireFactory.CreateEmpire(3, "Conquer3")
                    };


                    foreach (var empire in empires)
                    {
                        empire.ExecuteConquer(conquer, 10000);
                    }


                    var result = AnalyzeHelper.AnalyzeWorld(conquer.getWorldEmpires(), world, empires);


                    await _dbHelper.SaveWorldResultAsync(result);

                    Console.WriteLine($"Resultaten voor simulatie {sim + 1} opgeslagen.");
                }
            }

            Console.WriteLine("Alle simulaties zijn voltooid!");
        }

        public static async Task<Dictionary<string, double>> CalculateAveragePerformance(MongoDBHelper dbHelper)
        {
            var allResults = await dbHelper.GetAllWorldResultsAsync();

            Dictionary<string, (int TotalCells, int Count)> strategyStats = new();

            foreach (var result in allResults)
            {
                foreach (var empire in result.Empires)
                {
                    if (!strategyStats.ContainsKey(empire.Strategy))
                    {
                        strategyStats[empire.Strategy] = (0, 0);
                    }

                    strategyStats[empire.Strategy] = (
                        strategyStats[empire.Strategy].TotalCells + empire.ConqueredCells,
                        strategyStats[empire.Strategy].Count + 1
                    );
                }
            }

            return strategyStats.ToDictionary(
                kvp => kvp.Key,
                kvp => (double)kvp.Value.TotalCells / kvp.Value.Count
            );
        }
    }
}
