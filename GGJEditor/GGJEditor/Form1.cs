using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GGJEditor
{
    public partial class Form1 : Form
    {
        private Bitmap field;
        private byte[][][] tileMap;
        private Bitmap[] tiles;
        private Bitmap[] darkTiles;
        private byte selectedTool = 0;
        private byte selectedWorld = 0;
        private byte selectedLayer = 0;
        private bool ctrlHeld = false, shiftHeld = false;
        private bool drawingGrid = true;
        private bool layerHilighting = true;
        private Point markPoint = Point.Empty;
        private Point currentRoom = new Point(5, 5);
        private ToolboxForm toolboxForm = null;

        public const int mapHeight = 20;
        public const int mapWidth = 30;
        public const int tileSize = 18;
        private const int resHeight = mapHeight * tileSize;
        private const int resWidth = mapWidth * tileSize;
        private void SetupScreen()
        {
            field = new Bitmap(resWidth + 1, resHeight + 1);
            fieldBox.Size = field.Size;
            Graphics g = Graphics.FromImage(field);

            FlipBuffers();
        }

        private void FlipBuffers()
        {
            Graphics g = Graphics.FromImage(field);
            g.FillRectangle(new SolidBrush(Color.Magenta), new Rectangle(new Point(0), field.Size));
            for (int x = 0; x < mapWidth; x++)
            {
                for (int y = 0; y < mapHeight; y++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        byte b = tileMap[x][y][z];
                        if (layerHilighting)
                        {
                            if (z == selectedLayer)
                                g.DrawImage(tiles[b], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize),
                                    new Rectangle(0, 0, tileSize, tileSize), GraphicsUnit.Pixel);
                            else
                                g.DrawImage(darkTiles[b], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize),
                                    new Rectangle(0, 0, tileSize, tileSize), GraphicsUnit.Pixel);
                        }
                        else
                            g.DrawImage(tiles[b], new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize),
                                new Rectangle(0, 0, tileSize, tileSize), GraphicsUnit.Pixel);
                    }
                    if (drawingGrid)
                    {
                        g.DrawRectangle(new Pen(new SolidBrush(Color.Black)),
                        new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize));
                    }
                }
            }
            if (markPoint != Point.Empty)
            g.DrawRectangle(new Pen(new SolidBrush(Color.Yellow)),
                new Rectangle(markPoint.X * tileSize, markPoint.Y * tileSize, tileSize, tileSize));
            fieldBox.Image = field;
        }

        private void InitTileMap()
        {
            tileMap = new byte[mapWidth][][];
            for (int x = 0; x < tileMap.Length; x++)
            {
                tileMap[x] = new byte[mapHeight][];
                for (int y = 0; y < tileMap[x].Length; y++)
                {
                    tileMap[x][y] = new byte[3];
                    for (int z = 0; z < 3; z++)
                    {
                        tileMap[x][y][z] = 0xff;
                    }
                }
            }
        }

        private void LoadTiles()
        {
            tiles = new Bitmap[tileSize * tileSize];
            darkTiles = new Bitmap[tileSize * tileSize];
            Bitmap tileSheet = new Bitmap("Content\\TileSheet.png");
            for (int y = 0; y < 16; y++)
            {
                for(int x = 0; x < 16; x++)
                {
                    tiles[y * 16 + x] = new Bitmap(tileSize, tileSize);
                    darkTiles[y * 16 + x] = new Bitmap(tileSize, tileSize);
                    Graphics g = Graphics.FromImage(tiles[y * 16 + x]);
                    g.DrawImage(tileSheet, new Rectangle(0, 0, tileSize, tileSize),
                        new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize),
                        GraphicsUnit.Pixel);
                    g = Graphics.FromImage(darkTiles[y * 16 + x]);
                    g.DrawImage(tileSheet, new Rectangle(0, 0, tileSize, tileSize),
                        new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize),
                        GraphicsUnit.Pixel);
                    BitmapData bmpdata = darkTiles[y * 16 + x].LockBits(new Rectangle(new Point(0), darkTiles[y * 16 + x].Size),
                        System.Drawing.Imaging.ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                    unsafe
                    {
                        byte* start = (byte*)bmpdata.Scan0;
                        int times = darkTiles[y * 16 + x].Width * darkTiles[y * 16 + x].Height;
                        for (int i = 0; i < times * 4; i += 4)
                        {
                            *(start + i) = (byte)(*(start + i) / 2);
                            *(start + i + 1) = (byte)(*(start + i + 1) / 2);
                            *(start + i + 2) = (byte)(*(start + i + 2) / 2);
                        }
                    }
                    darkTiles[y * 16 + x].UnlockBits(bmpdata);
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            InitTileMap();
            LoadTiles();
            SetupScreen();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ShiftKey:
                    shiftHeld = true;
                    selectedTool += 16;
                    toolLabel.Text = String.Format("Tool: 0x{0:x}", selectedTool);
                    if(toolboxForm != null)
                        toolboxForm.SetSelectedTool(selectedTool);
                    break;
                case Keys.LButton | Keys.ShiftKey:
                    selectedTool -= 16;
                    ctrlHeld = true;
                    toolLabel.Text = String.Format("Tool: 0x{0:x}", selectedTool);
                    if(toolboxForm != null)
                    toolboxForm.SetSelectedTool(selectedTool);
                    break;
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                    selectedTool = (byte)((e.KeyCode - Keys.D0) + 
                                   (selectedTool - (selectedTool % 0x10)));
                    toolLabel.Text = String.Format("Tool: 0x{0:x}", selectedTool);
                    if(toolboxForm != null)
                    toolboxForm.SetSelectedTool(selectedTool);
                    break;
                case Keys.A:
                case Keys.B:
                case Keys.C:
                case Keys.D:
                case Keys.E:
                case Keys.F:
                    selectedTool = (byte)((e.KeyCode - Keys.A + 0xa) +
                                   (selectedTool - (selectedTool % 0x10)));
                    toolLabel.Text = String.Format("Tool: 0x{0:x}", selectedTool);
                    if(toolboxForm != null)
                    toolboxForm.SetSelectedTool(selectedTool);
                    break;
                case Keys.G:
                    drawingGrid = !drawingGrid;
                    break;
                case Keys.H:
                    layerHilighting = !layerHilighting;
                    break;
                case Keys.W:
                    selectedLayer++;
                    if (selectedLayer > 2) selectedLayer = 0;
                    layerLabel.Text = "Layer: " + selectedLayer;
                    break;
                case Keys.S:
                    selectedLayer--;
                    if (selectedLayer > 2) selectedLayer = 2;
                    layerLabel.Text = "Layer: " + selectedLayer;
                    break;
            }
            FlipBuffers();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ShiftKey:
                    shiftHeld = false;
                    break;
                case Keys.LButton | Keys.ShiftKey:
                    ctrlHeld = false;
                    break;
            }
        }

        private void fieldBox_Click(object sender, EventArgs e)
        {
            MouseEventArgs m = (MouseEventArgs)e;
            if (m.Button == MouseButtons.Right)
            {
                if (markPoint == Point.Empty)
                {
                    markPoint = new Point(m.X / tileSize, m.Y / tileSize);
                }
                else
                {
                    Point endMkPt = new Point(m.X / tileSize, m.Y / tileSize);
                    int startPtx = Math.Min(endMkPt.X, markPoint.X);
                    int endPtx = Math.Max(endMkPt.X, markPoint.X);
                    int startPty = Math.Min(endMkPt.Y, markPoint.Y);
                    int endPty = Math.Max(endMkPt.Y, markPoint.Y);
                    for (int x = startPtx; x < endPtx + 1; x++)
                    {
                        for (int y = startPty; y < endPty + 1; y++)
                        {
                            tileMap[x][y][selectedLayer] = selectedTool;
                        }
                    }
                    markPoint = Point.Empty;
                }
            }
            else if (m.Button == MouseButtons.Left)
            {
                tileMap[m.X / tileSize][m.Y / tileSize][selectedLayer] = (byte)selectedTool;
            }
            FlipBuffers();
        }

        private void changeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WorldPicker w = new WorldPicker(selectedWorld);
            w.ShowDialog();
            currentRoom = w.PickedRoom;
            string path = "Maps\\ovr" + (currentRoom.Y * WorldPicker.worldWidth + currentRoom.X) + ".map";
            if (File.Exists(path))
            {
                FileStream fStream = new FileStream(path, FileMode.OpenOrCreate);
                for (int y = 0; y < mapHeight; y++)
                {
                    for (int x = 0; x < mapWidth; x++)
                    {
                        for (int z = 0; z < 3; z++)
                        {
                            tileMap[x][y][z] = (byte)fStream.ReadByte();
                        }
                    }
                }
                fStream.Close();
            }
            else
            {
                InitTileMap();
            }
            FlipBuffers();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = "Maps\\ovr" + (currentRoom.Y * WorldPicker.worldWidth + currentRoom.X) + ".map";
            FileStream fStream = new FileStream(path, FileMode.OpenOrCreate);
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    for (int z = 0; z < 3; z++)
                    {
                        fStream.WriteByte(tileMap[x][y][z]);
                    }
                }
            }
            fStream.Close();
        }

        private void toolboxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(toolboxForm == null)
            {
                toolboxForm = new ToolboxForm(selectedTool, this);
            }
            else if (toolboxForm.IsDisposed)
            {
                toolboxForm = new ToolboxForm(selectedTool, this);
            }
            toolboxForm.Show();
        }

        public void SetSelectedTool(byte tool)
        {
            selectedTool = tool;
        }
    }
}
