using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathDraw
{
    public partial class MainForm : Form
    {
        Random rng = new Random();
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            drawtest();
            
        }

        public void drawtest()
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Bitmap bitmap1 = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                Graphics graphics1 = Graphics.FromImage(bitmap1);
                Pen pen1 = new Pen(Color.Black, 1);
                Point[] pontos = new Point[1200];
                int limit = 900;
                for (int i = 0; i < pontos.Length - limit; i++)
                {
                    pontos[i].X = rng.Next(0, pictureBox1.Width);
                    pontos[i].Y = rng.Next(0, pictureBox1.Height);

                }
                graphics1.DrawPolygon(pen1, pontos);

                _ = pictureBox1.Invoke(new MethodInvoker(
              delegate ()
              {
                  pictureBox1.Image = bitmap1;
                  pictureBox1.Update();
                  bitmap1.Dispose();
                  graphics1.Dispose();
                  Thread.Sleep(10);
              }));
            }).Start();
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            keepdrawing();
        }
        private void keepdrawing()
        {
          
            for (int i = 0; i < 100; i++)
            {

                drawtest();

            }
         

           
        }

    }
    
}
