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
            var worlds = await _dbHelper.GetAllWorldResultsAsync();

            foreach (var world in worlds)
            {


                for (int sim = 0; sim < simulationsPerWorld; sim++)
                {


                    // Genereer een kopie van de wereld
                    var worldCopy = (bool[,])world.Data.Clone();
                    WorldConquer conquer = new WorldConquer(worldCopy);

                    // Maak rijken met verschillende strategieën
                    List<Empire> empires = new()
                {
                    EmpireFactory.CreateEmpire(1, "Conquer1"),
                    EmpireFactory.CreateEmpire(2, "Conquer2"),
                    EmpireFactory.CreateEmpire(3, "Conquer3")
                };

                    // Simuleer verovering
                    foreach (var empire in empires)
                    {
                        empire.ExecuteConquer(conquer, 10000);
                    }

                    // Bereken resultaten
                    var result = AnalyzeHelper.AnalyzeWorld(conquer.getWorldEmpires(), world, empires);

                    // Sla resultaten op
                    await _dbHelper.SaveWorldResultAsync(result);

                    Console.WriteLine($"Resultaten voor simulatie {sim + 1} opgeslagen.");
                }
            }

            Console.WriteLine("Alle simulaties zijn voltooid!");
        }
    }
}
