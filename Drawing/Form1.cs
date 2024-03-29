﻿using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using static System.Math;

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
			  }));
			}).Start();

		}

		public void Drawtest_Waves()
		{
			// Creating a new thread to not crash the form.
			new Thread(() =>
			{
				// Defining the maximum amount of points to be generated per tick.
				// The maximum amount of points can increase along with the form width, the density looks more proportional.
				Point[] pontos = new Point[pictureBox1.Width + pictureBox1.Height];

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

                for (int i = 0; i < 1; i++)
                {
					// The same thing again...
					{
						for (int ii = 0; ii < pontos.Length  ; ii++)
						{

							pontos[ii].X = (rng.Next(0, pictureBox1.Width));
							pontos[ii].Y = (rng.Next(0, pictureBox1.Height));
							if (ii % 1 == 0)
							{
								rainbow = Color.FromArgb(255, rng.Next(0, 255), rng.Next(0, 255), rng.Next(0, 255));
								pen1.Color = rainbow;
					         	graphics1.DrawEllipse(pen1, new Rectangle(pontos[ii], new System.Drawing.Size(pontos[ii].X, pontos[ii].Y)));

							}
							
								
						}
						// Then we draw the above generated points with our rainbow pencil.'
						// graphics1.DrawPolygon(pen1, pontos);

					}
					
				}

				// Now we need to Invoke the pictureBox1 from our different thread and update the image.
				_ = pictureBox1.Invoke(new MethodInvoker(
			  delegate ()
			  {
				  pictureBox1.Image = bitmap1;
				  pictureBox1.Update();
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
					Thread.Sleep(100);
				}

				//Drawtest_Colorful();
				Drawtest_Waves();

			}

		}
		private void pictureBox1_Click(object sender, EventArgs e)
		{
			// keepdrawing(10);
		}

        private void refresh(object sender, EventArgs e)
        {
			keepdrawing(1);
        }

        private void save_file(object sender, EventArgs e)
        {
			using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = @"PNG|*.png" })
			{
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					pictureBox1.Image.Save(saveFileDialog.FileName);
				}
			}
		}

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
				case (Keys.F5):
					refresh(null,null);
					break;
				case (Keys.S):
					if (e.Control)
					{
						save_file(null, null);
					}
					break;

            }
        }
    }

}
