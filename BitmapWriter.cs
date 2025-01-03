﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster
{
    internal class BitmapWriter
    {
        private const int drawingFactor = 8;
        private string path= @"C:\temp\PG2\Refactor";
        public void DrawWorld(int[,] world)
        {
            Color[] cvalues = new Color[] {Color.Green, Color.Red, Color.Yellow, Color.Blue, Color.Cyan, Color.GreenYellow,Color.Gold,Color.Ivory,Color.NavajoWhite };
            Bitmap bm = new Bitmap(world.GetLength(0) * drawingFactor, world.GetLength(1)* drawingFactor);

            Graphics g = Graphics.FromImage(bm);

            int delta = drawingFactor / 2;
            Color color;
            for (int x = 0; x < world.GetLength(0); x++)
                for (int y = 0; y < world.GetLength(1); y++)
                {
                    int p1x = x * drawingFactor + delta;
                    int p1y = y * drawingFactor + delta;
                    if (world[x, y] >= 0)
                    {
                        color = cvalues[world[x, y] % cvalues.Length];
                        Pen pen = new Pen(color, 1);
                        g.DrawRectangle(pen, p1x - delta, p1y - delta, drawingFactor, drawingFactor);
                        if (world[x, y] != 0)
                        {
                            g.FillRectangle(new SolidBrush(color), p1x - delta, p1y - delta, drawingFactor, drawingFactor);
                        }
                    }
                }
            //var x = Path.Combine(path, woodID.ToString() + "_escapeRoutes.jpg");
            bm.Save(Path.Combine(path, "world.jpg"), ImageFormat.Jpeg);
        }
    }
}
