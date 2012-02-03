using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GGJEditor
{
    public partial class ToolboxForm : Form
    {
        private byte selectedTool;
        private Bitmap field;
        private Bitmap spriteSheet = new Bitmap(@"Content\\tilesheet.png");
        private Form1 parent;
        public ToolboxForm(byte selectedTool, Form1 f1)
        {
            parent = f1;
            this.selectedTool = selectedTool;
            InitializeComponent();
            SetupField();
            this.ClientSize = field.Size;
            this.Paint += new PaintEventHandler(ToolboxForm_Paint);
            this.Click += new EventHandler(ToolboxForm_Click);
        }

        void ToolboxForm_Click(object sender, EventArgs e)
        {
            MouseEventArgs m = (MouseEventArgs)e;
            SetSelectedTool((byte)((m.X / Form1.tileSize) + (16 * (m.Y / Form1.tileSize))));
            parent.SetSelectedTool(selectedTool);
        }

        void SetupField()
        {
            field = new Bitmap(spriteSheet);
            Graphics g = Graphics.FromImage(field);
            g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(new Point(0), field.Size));
            g.DrawImage(spriteSheet, new Rectangle(new Point(0), field.Size),
                new Rectangle(new Point(0), field.Size), GraphicsUnit.Pixel);
            Color drawColor;
            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    drawColor = Color.DarkGray;
                    if(selectedTool == (y * 16) + x) drawColor = Color.Yellow;
                    g.DrawRectangle(new Pen(new SolidBrush(drawColor)), new Rectangle(x * Form1.tileSize,
                        y * Form1.tileSize, Form1.tileSize, Form1.tileSize));
                    g.DrawString(String.Format("{0:x}", y*16 + x), 
                        new Font("Small Fonts", 8.0f), new SolidBrush(drawColor),
                        new PointF(x * Form1.tileSize, y * Form1.tileSize));
                }
            }
        }

        public void SetSelectedTool(byte toolNum)
        {
            selectedTool = toolNum;
            SetupField();
            this.Invalidate();
        }

        void ToolboxForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(field, new Rectangle(new Point(0), field.Size),
                new Rectangle(new Point(0), field.Size), GraphicsUnit.Pixel);
        }


    }
}
