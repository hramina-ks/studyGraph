using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace studyGraph
{
    public partial class FormMain : Form
    {
        bool isPressed = false;
        int x1, y1, x2, y2; //координаты мыши
        Bitmap snapshot, tempDraw; //снимки экрана
        Color foreColor; //цвет
        int lineWidth; //ширина линий
        string selectedTool; 

        public FormMain()
        {
            InitializeComponent();
            snapshot = new Bitmap(pictureBox.ClientRectangle.Width, pictureBox.ClientRectangle.Height);
            tempDraw = (Bitmap)snapshot.Clone();
            ForeColor = Color.Black;
            lineWidth = 2;
            Pencil.Checked = true;
            selectedTool = "Pencil";
        }

        private void Tool_Click(object sender, EventArgs e)
        {
            Line.Checked = false;
            Rectangle.Checked = false;
            Pencil.Checked = false;
            ToolStripButton btnClicked = sender as ToolStripButton;
            btnClicked.Checked = true;
            selectedTool = btnClicked.Name;
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            isPressed = true;
            x1 = e.X;
            y1 = e.Y;
            tempDraw = (Bitmap)snapshot.Clone();
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isPressed = false;
            snapshot = (Bitmap)tempDraw.Clone();
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPressed)
            {
                x2 = e.X;
                y2 = e.Y;
                pictureBox.Invalidate();
                pictureBox.Update();
            }
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (selectedTool != "Pencil")
                tempDraw = (Bitmap)snapshot.Clone();
            Graphics g = Graphics.FromImage(tempDraw);
            Pen myPen = new Pen(foreColor, lineWidth);
            switch (selectedTool)
            {
                case "Line":
                    if (tempDraw != null)
                        g.DrawLine(myPen, x1, y1, x2, y2);
                    break;
                case "Rectangle":
                    if (tempDraw != null)
                        g.DrawRectangle(myPen, x1, y1, x2 - x1, y2 - y1);
                    break;
                case "Pencil":
                    if (tempDraw != null)
                    {
                        g.DrawLine(myPen, x1, y1, x2, y2);
                        x1 = x2;
                        y1 = y2;
                    }
                    break;
                default: break;
            }
            myPen.Dispose();
            e.Graphics.DrawImageUnscaled(tempDraw, 0, 0);
            g.Dispose();
        }
    }
}
