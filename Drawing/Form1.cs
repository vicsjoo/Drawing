using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace MathDraw
{
	public partial class MainForm : Form
	{
		// Initializes the randomizer.
		Random rng = new Random();
		public MainForm()
		{
			InitializeComponent();
		}
		private void MainForm_Load(object sender, EventArgs e)
		{
			keepdrawing(10);

		}
		public void Drawtest_Colorful()
		{
			// Creating a new thread to not crash the form.
			new Thread(() =>
			{
				// Defining the maximum amount of points to be generated per tick.
				// The maximum amount of points can increase along with the form width, the density looks more proportional.
				Point[] pontos = new Point[100+this.Width/10];

				// Defining a new color using RNG
				Color rainbow = Color.FromArgb(255, rng.Next(0, 255), rng.Next(0, 255), rng.Next(0, 255));

				Thread.CurrentThread.IsBackground = true;

				// Initializes the bitmap.
				Bitmap bitmap1 = new Bitmap(this.Width, this.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

				// Manually calling Garbage Collector since calling the dispose of the bitmap1 object are causing multiple issues when resizing the form.
				System.GC.Collect();
				
				Graphics graphics1 = Graphics.FromImage(bitmap1);
				Pen pen1 = new Pen(rainbow, 1);
				pen1.Color = rainbow;
				
				// Here we randomly generate the X and Y values for half of our points, since we want the other half to have a different color.
				for (int i = 0; i < pontos.Length / 2; i++)
				{
					pontos[i].X = rng.Next(0, pictureBox1.Width);
					pontos[i].Y = rng.Next(0, pictureBox1.Height);
					if (i % 100 == 0)
					{
						rainbow = Color.FromArgb(255, rng.Next(0, 255), rng.Next(0, 255), rng.Next(0, 255));
						pen1.Color = rainbow;
					}
				}

				// Then we draw the above generated points with our rainbow pencil.
				graphics1.DrawPolygon(pen1, pontos);

				// The same thing again...
				for (int i = 0; i < pontos.Length / 2; i++)
				{
					pontos[i].X = rng.Next(0, pictureBox1.Width);
					pontos[i].Y = rng.Next(0, pictureBox1.Height);
					if (i % 100 == 0)
					{
						rainbow = Color.FromArgb(255, rng.Next(0, 255), rng.Next(0, 255), rng.Next(0, 255));
						pen1.Color = rainbow;
					}
				}

				graphics1.DrawPolygon(pen1, pontos);

				// Now we need to Invoke the pictureBox1 from our different thread and update the image.
				_ = pictureBox1.Invoke(new MethodInvoker(
			  delegate ()
			  {
				  pictureBox1.Image = bitmap1;
				  pictureBox1.Update();
                  //      bitmap1.Dispose();
                  // graphics1.Dispose();
				  Thread.Sleep(100);
			  }));
			}).Start();

		}

		private void keepdrawing(int times)
		{

			for (int i = 0; i < times; i++)
			{

				if (pictureBox1 != null && pictureBox1.Image != null)
				{
					Image img = pictureBox1.Image;
					pictureBox1.Image = null;
					img.Dispose();
				}

				Drawtest_Colorful();

			}

		}
		private void pictureBox1_Click(object sender, EventArgs e)
		{
			keepdrawing(10);
		}
	}

}
