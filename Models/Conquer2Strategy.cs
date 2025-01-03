using ConsoleAppSquareMaster.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster.Model
{
    public class Conquer2Strategy : IConquerStrategy
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
            List<(int, int)> controlledCells = new(); // Cellen die dit rijk controleert

            bool startPositionFound = false;
            while (!startPositionFound)
            {
                x = random.Next(maxx);
                y = random.Next(maxy);
                if (world[x, y] && worldempires[x, y] == 0) // Vrije cel in de wereld
                {
                    startPositionFound = true;
                    worldempires[x, y] = empireId;
                    controlledCells.Add((x, y));
                }
            }

            // Simuleer verovering
            for (int i = 0; i < turns; i++)
            {
                if (controlledCells.Count == 0) break; // Geen controle over cellen meer
                int selectedIndex = FindWithMostEmptyNeighbours(controlledCells, worldempires, world, maxx, maxy);

                x = controlledCells[selectedIndex].Item1;
                y = controlledCells[selectedIndex].Item2;

                // Zoek een vrije aangrenzende locatie
                List<(int, int)> neighbours = GetEmptyNeighbours(x, y, worldempires, world, maxx, maxy);
                if (neighbours.Count > 0)
                {
                    var selectedNeighbour = neighbours[random.Next(neighbours.Count)];
                    worldempires[selectedNeighbour.Item1, selectedNeighbour.Item2] = empireId;
                    controlledCells.Add(selectedNeighbour);
                }
            }
        }

        private int FindWithMostEmptyNeighbours(List<(int, int)> controlledCells, int[,] worldempires, bool[,] world, int maxx, int maxy)
        {
            int maxNeighbours = -1;
            List<int> candidates = new();
            for (int i = 0; i < controlledCells.Count; i++)
            {
                var cell = controlledCells[i];
                int count = GetEmptyNeighbours(cell.Item1, cell.Item2, worldempires, world, maxx, maxy).Count;
                if (count > maxNeighbours)
                {
                    maxNeighbours = count;
                    candidates.Clear();
                    candidates.Add(i);
                }
                else if (count == maxNeighbours)
                {
                    candidates.Add(i);
                }
            }
            var random = new Random();
            return candidates[random.Next(candidates.Count)];
        }

        public List<(int, int)> GetEmptyNeighbours(int x, int y, int[,] worldempires, bool[,] world, int maxx, int maxy)
        {
            List<(int, int)> neighbours = new();
            if (x > 0 && worldempires[x - 1, y] == 0 && world[x - 1, y]) neighbours.Add((x - 1, y));
            if (x < maxx - 1 && worldempires[x + 1, y] == 0 && world[x + 1, y]) neighbours.Add((x + 1, y));
            if (y > 0 && worldempires[x, y - 1] == 0 && world[x, y - 1]) neighbours.Add((x, y - 1));
            if (y < maxy - 1 && worldempires[x, y + 1] == 0 && world[x, y + 1]) neighbours.Add((x, y + 1));
            return neighbours;
        }
    }
}

