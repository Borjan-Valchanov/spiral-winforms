using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Spiral {
	class SpiralForm : Form {
		// Initialise the graphics object for drawing the spiral
		private Graphics graphics;

		// Constructor
		public SpiralForm() {
			// Make the size fixed. Here is also where you can change the size of the window.
			// Note that ClientSize is the inner size of the window where things are actually displayed,
			// while Size is the size of the entire window including the title bar.
			ClientSize = new Size(800, 600);
			MinimumSize = Size;
			MaximumSize = Size;
			MaximizeBox = false;
			// Prepare the Graphics object for drawing the spiral 
			graphics = CreateGraphics();
			graphics.SmoothingMode = SmoothingMode.AntiAlias;
			Shown += (sender, eventArgs) => drawSpiral();
		}

		// Method for drawing the spiral
		private void drawSpiral() {
			// Initialising variables for spiral
			double rad = 0;
			double rot = 0;
			// The loop running the calculations and drawing the lines until hitting the border
			while (true) {
				// Calculate the current and next point
				// Before calculating the next point, increase rot and rad
				// Note: the denominators behind rot and rad are for controlling the shape and quality of the spiral;
				// for example if the ratio between the denominators stayed the same while decreasing their values, the shape would stay the same
				// but the quality would decrease. But if for example you increased rad's denominator while leaving rot's as it is, you would make the spiral tigher.
				// Also: NEVER set the denominator of one of them in one equation to a different one than in the other one,
				// because that makes cPoint and nPoint belong to different Spirals and thus can lead to some trippy results.
				Point cPoint = ddToPoint(calPoint(rot/10, rad/2, ClientSize.Width/2, ClientSize.Height/2));
				Point nPoint = ddToPoint(calPoint(++rot/10, ++rad/2, ClientSize.Width/2, ClientSize.Height/2));

				// Only continue if the current point still is inside the form
				if (cPoint.X < 0
					|| cPoint.X > ClientSize.Width
					|| cPoint.Y < 0
					|| cPoint.Y > ClientSize.Height) break;

				// Draw a line between the current and next Point
				graphics.DrawLine(Pens.Black, cPoint, nPoint);
			}
		}

		// Method for calculating a point on a circle
		private (double, double) calPoint(double rot, double rad = 1, double offX = 0, double offY = 0) {
			// Initialise variables for storing calculated values
			double x;
			double y;
			// Calculate the coordinates using cosine for X and sine for Y 
			x = offX + (rad * Math.Cos(rot));
			y = offY + (rad * Math.Sin(rot));
			// Return the values
			return (x, y);
		}

		// Method for converting from (double, double) to Point
		private Point ddToPoint((double, double) ddouble) {
			// Return a point with the X coordinate being the first of the two doubles converted to an int32,
			// and the Y coordinate being the second of the two doubles converted to an int32
			return new Point(Convert.ToInt32(ddouble.Item1), Convert.ToInt32(ddouble.Item2));
		}
	}
}
