using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CodenameHorror
{
    public class TileMap
    {
        public byte[][][] data;

        public const int MapHeight = 20;
        public const int MapWidth = 30;
        public const int MapDepth = 3;
        public static int tileHeight = 48;
        public static int tileWidth = 48;

        public int xLoc;
        public int yLoc;

        public enum Dimension
        {
            Overworld,
            Hell
        }

        public static Texture2D spriteSheet;

        public static TileMap LoadMap(int xLocation, int yLocation, Dimension dimension)
        {
            TileMap t = new TileMap();
            t.xLoc = xLocation;
            t.yLoc = yLocation;
            t.data = new byte[MapWidth][][];
            for (int x = 0; x < MapWidth; x++)
            {
                t.data[x] = new byte[MapHeight][];
                for (int y = 0; y < MapHeight; y++)
                {
                    t.data[x][y] = new byte[MapDepth];
                }
            }
            FileStream fStream = new FileStream("Maps\\ovr" + ((yLocation * 16) + xLocation) + ".map", FileMode.Open);
            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    for (int z = 0; z < MapDepth; z++)
                    {
                        t.data[x][y][z] = (byte)fStream.ReadByte();
                    }
                }
            }
            fStream.Close();
            return t;
        }

        public static void ChangeResolution(int width, int height)
        {
            tileWidth = (int)((float)width / MapWidth);
            tileHeight = (int)((float)height / MapHeight);
        }

        public void Draw(SpriteBatch sb)
        {
            int spX, spY;
            for (int z = 0; z < MapDepth; z++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    for (int y = 0; y < MapHeight; y++)
                    {
                        spX = data[x][y][z] % 16;
                        spY = data[x][y][z] / 16;
                        sb.Draw(spriteSheet, new Rectangle(x * tileWidth,
                                                           y * tileHeight,
                                                           tileWidth, tileHeight),
                                new Rectangle(spX * 18, spY * 18, 17, 17), Color.White);
                    }
                }
            }
        }
    }
}
