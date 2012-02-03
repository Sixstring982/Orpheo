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
    public partial class WorldPicker : Form
    {
        private Bitmap field;

        public const int worldWidth = 16;
        public const int worldHeight = 16;
        private int worldNum;

        public Point PickedRoom = Point.Empty;
        public WorldPicker(int worldNum)
        {
            this.worldNum = worldNum;
            InitializeComponent();
            SetupField();
            this.ClientSize = field.Size;
            this.MaximizeBox = false;
            this.SizeGripStyle = SizeGripStyle.Hide;
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            this.Paint += new PaintEventHandler(WorldPicker_Paint);
        }

        private void SetupField()
        {
            field = new Bitmap(Form1.mapWidth * worldWidth + 1, Form1.mapHeight * worldWidth + 1);
            Graphics g = Graphics.FromImage(field);
            Color drawColor = Color.DarkGray;
            string prefix = "ovr";
            if (worldNum == 1) prefix = "hll";
            for (int x = 0; x < worldWidth; x++)
            {
                for (int y = 0; y < worldHeight; y++)
                {
                    if(File.Exists("Maps\\" + prefix + (y * worldWidth + x) + ".map"))
                        drawColor = Color.Yellow;
                    else
                        drawColor = Color.Black;
                    g.FillRectangle(new SolidBrush(drawColor),
                        new Rectangle(x * Form1.mapWidth, y * Form1.mapHeight,
                                      Form1.mapWidth, Form1.mapHeight));
                    g.DrawRectangle(new Pen(new SolidBrush(Color.DarkGray)),
                        new Rectangle(x * Form1.mapWidth, y * Form1.mapHeight,
                                      Form1.mapWidth, Form1.mapHeight));
                }
            }
        }

        void WorldPicker_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(field, new Point(0));
        }

        private void WorldPicker_Click(object sender, EventArgs e)
        {
            MouseEventArgs m = (MouseEventArgs)e;
            PickedRoom = new Point(m.X / Form1.mapWidth, m.Y / Form1.mapHeight);
            this.Close();
        }
    }
}
