﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster.Model
{
    public class WorldModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // Column-based or Seed-based
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public double Coverage { get; set; } // Alleen voor seed-based
        public bool[,] Data { get; set; } // Wereldgrid
                                          // Methode om werelden op te slaan

    }


}