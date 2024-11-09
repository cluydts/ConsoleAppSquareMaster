using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster
{
    public class WorldConquer
    {
        private bool[,] world;
        private int[,] worldempires;
        private int maxx, maxy;
        private Random random = new Random();

        public WorldConquer(bool[,] world)
        {
            this.world = world;
            maxx = world.GetLength(0);
            maxy = world.GetLength(1);
            worldempires = new int[maxx,maxy];
            for (int i = 0; i < world.GetLength(0); i++) for (int j = 0; j < world.GetLength(1); j++) if (world[i, j]) worldempires[i, j] = 0; else worldempires[i, j] = -1;
        }
        public int[,] Conquer(int nEmpires,int turns)
        {
            Dictionary<int, List<(int, int)>> empires = new();
            //zoek start
            int x, y;
            for (int i = 0; i < nEmpires; i++)
            {
                bool ok = false;
                while (!ok)
                {
                    x = random.Next(maxx); y = random.Next(maxy);
                    if (world[x, y])
                    {
                        ok = true;
                        worldempires[x, y] = i + 1;
                        empires.Add(i + 1, new List<(int, int)>() { (x, y) });
                    }
                }
            }
            //aantal beurten
            int index;
            int richting;//0 -rechts,1-links,2-boven,3-onder
            for (int i = 0; i < turns; i++)
            {
                for(int e = 1; e <= nEmpires; e++)
                {
                    index = random.Next(empires[e].Count);
                    richting = random.Next(4);
                    x = empires[e][index].Item1;
                    y= empires[e][index].Item2;
                    switch (richting)
                    {
                        case 0:
                            if (x<maxx-1 && worldempires[x+1,y]==0)
                            {
                                worldempires[x + 1, y] = e;
                                empires[e].Add((x+1, y));
                            }
                            break;
                        case 1:
                            if (x>1 && worldempires[x - 1, y] == 0)
                            {
                                worldempires[x - 1, y] = e;
                                empires[e].Add((x - 1, y));
                            }
                            break;
                        case 2:
                            if (y < maxy - 1 && worldempires[x, y+1] == 0)
                            {
                                worldempires[x, y+1] = e;
                                empires[e].Add((x, y+1));
                            }
                            break;
                        case 3:
                            if (y >1 && worldempires[x, y-1] == 0)
                            {
                                worldempires[x, y-1] = e;
                                empires[e].Add((x, y-1));
                            }
                            break;
                    }
                }
            }
            return worldempires;
        }
    }
}
