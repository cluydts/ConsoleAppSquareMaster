using ConsoleAppSquareMaster.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster.Model
{
    public class Conquer1Strategy : IConquerStrategy
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

            // Simuleer verovering gedurende `turns`
            for (int i = 0; i < turns; i++)
            {
                if (controlledCells.Count == 0) break; // Als het rijk geen cellen meer heeft, stop
                int index = random.Next(controlledCells.Count); // Kies een willekeurige gecontroleerde cel
                var cell = controlledCells[index];
                x = cell.Item1;
                y = cell.Item2;

                // Kies een willekeurige richting
                int direction = random.Next(4);
                switch (direction)
                {
                    case 0: // Rechts
                        if (x < maxx - 1 && worldempires[x + 1, y] == 0 && world[x + 1, y])
                        {
                            worldempires[x + 1, y] = empireId;
                            controlledCells.Add((x + 1, y));
                        }
                        break;
                    case 1: // Links
                        if (x > 0 && worldempires[x - 1, y] == 0 && world[x - 1, y])
                        {
                            worldempires[x - 1, y] = empireId;
                            controlledCells.Add((x - 1, y));
                        }
                        break;
                    case 2: // Boven
                        if (y < maxy - 1 && worldempires[x, y + 1] == 0 && world[x, y + 1])
                        {
                            worldempires[x, y + 1] = empireId;
                            controlledCells.Add((x, y + 1));
                        }
                        break;
                    case 3: // Onder
                        if (y > 0 && worldempires[x, y - 1] == 0 && world[x, y - 1])
                        {
                            worldempires[x, y - 1] = empireId;
                            controlledCells.Add((x, y - 1));
                        }
                        break;
                }

            }
        }
    }
}
