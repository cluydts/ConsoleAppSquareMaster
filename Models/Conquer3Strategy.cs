using ConsoleAppSquareMaster.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster.Model
{
    public class Conquer3Strategy : IConquerStrategy
    {


        public void Conquer(WorldConquer conquer, int empireId, int turns)
        {
            var world = conquer.GetWorld();
            var maxx = world.GetLength(0);
            var maxy = world.GetLength(1);
            var worldempires = conquer.getWorldEmpires();
            var random = new Random();

            // Vind startpositie voor het rijk
            int x, y;
            List<(int, int)> controlledCells = new();
            bool startPositionFound = false;
            while (!startPositionFound)
            {
                x = random.Next(maxx);
                y = random.Next(maxy);
                if (world[x, y] && worldempires[x, y] == 0)
                {
                    startPositionFound = true;
                    worldempires[x, y] = empireId;
                    controlledCells.Add((x, y));
                }
            }

            // Simuleer verovering
            for (int i = 0; i < turns; i++)
            {
                if (controlledCells.Count == 0) break;
                int selectedIndex = random.Next(controlledCells.Count);
                var cell = controlledCells[selectedIndex];

                // Zoek vrije aangrenzende locaties
                var neighbours = GetEmptyNeighbours(cell.Item1, cell.Item2, worldempires, world, maxx, maxy);
                if (neighbours.Count > 0)
                {
                    var selectedNeighbour = neighbours[random.Next(neighbours.Count)];
                    worldempires[selectedNeighbour.Item1, selectedNeighbour.Item2] = empireId;
                    controlledCells.Add(selectedNeighbour);
                }
            }
        }

        private List<(int, int)> GetEmptyNeighbours(int x, int y, int[,] worldempires, bool[,] world, int maxx, int maxy)
        {
            // Hergebruik de methode uit Conquer2Strategy
            Conquer2Strategy helper = new();
            return helper.GetEmptyNeighbours(x, y, worldempires, world, maxx, maxy);
        }
    }
}

