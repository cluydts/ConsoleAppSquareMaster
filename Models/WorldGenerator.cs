using ConsoleAppSquareMaster.Helper;
using ConsoleAppSquareMaster.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster.Models
{
    public class WorldGenerator
    {
        private readonly MongoDBHelper _dbHelper;

        public WorldGenerator()
        {
            _dbHelper = new MongoDBHelper();
        }

        public async Task GenerateAndSaveWorldsAsync()
        {
            MongoDBHelper dbHelper = new MongoDBHelper();

            // Wereldgenerator
            World worldGenerator = new World();
            List<WorldModel> worlds = new();

            // Genereer 10 werelden
            for (int i = 1; i <= 10; i++)
            {
                string name = $"GeneratedWorld_{i}";
                int maxX = 100;
                int maxY = 100;
                double coverage = 0.50 + (i * 0.05); // Varieert van 50% tot 95%

                // Genereer wereld met BuildWorld2
                var data = worldGenerator.BuildWorld2(maxY, maxX, coverage);

                // Maak WorldModel-object
                WorldModel world = new WorldModel
                {
                    Id = i,
                    Name = name,
                    Type = "Seed-based",
                    MaxX = maxX,
                    MaxY = maxY,
                    Coverage = coverage,
                    Data = data
                };

                worlds.Add(world);

                Console.WriteLine($"Wereld '{name}' gegenereerd met {coverage:P} dekking.");
            }

            // Sla alle werelden op in MongoDB
            foreach (var world in worlds)
            {
                await dbHelper.SaveWorldAsync(world);
                Console.WriteLine($"Wereld '{world.Name}' succesvol opgeslagen in de database.");
            }

            Console.WriteLine("Alle 10 werelden zijn gegenereerd en opgeslagen!");
        }
    }
}
