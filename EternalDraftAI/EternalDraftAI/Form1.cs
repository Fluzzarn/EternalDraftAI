using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using ScreenShots;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EternalDraftAI
{
    public partial class Form1 : Form
    {

        IntPtr EternalHandle;

        public Form1()
        {
            this.Cursor = new Cursor(Cursor.Current.Handle);
            InitializeComponent();
            var timer1 = new Timer();
            timer1.Interval = 16;
            timer1.Tick += timer1_Tick;
            timer1.Start();


        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (EternalHandle != IntPtr.Zero)
            {


                ScreenCapture sc = new ScreenCapture();
                System.Drawing.Image im = sc.CaptureWindow(EternalHandle);
                if (pictureBox1.Image != null)
                {
                    var old = pictureBox1.Image;
                    old.Dispose();

                }
                
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = im;
                Bitmap bp = new Bitmap(im);


                // set center colol and radius

                if (redTextBox.Text == "")
                
                    redTextBox.Text = "0";
                
                if (greenTextBox.Text == "")
                
                    greenTextBox.Text = "0";
                
                if (blueTextBox.Text == "")
                
                    blueTextBox.Text = "0";
                

                int r = Convert.ToInt32(redTextBox.Text);
                int green = Convert.ToInt32(greenTextBox.Text);
                int b = Convert.ToInt32(blueTextBox.Text);

                // create filter
                var temp = (Bitmap)System.Drawing.Image.FromFile("card-mask.png");
                var mask = new Bitmap(temp,new Size(bp.Width,bp.Height));
                MaskedFilter filter = new MaskedFilter(new ColorFiltering(new IntRange(255,255), new IntRange(255, 255), new IntRange(255, 255)), mask);

                filter.ApplyInPlace(bp);


                pictureBox1.Image = bp;


            }
        }

        private PointF[] ToPointsArray(List<IntPoint> points)
        {
            PointF[] array = new PointF[points.Count];

            for (int i = 0, n = points.Count; i < n; i++)
            {
                array[i] = new PointF(points[i].X, points[i].Y);
            }

            return array;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var processes = Process.GetProcesses();
            var eternalProcess = processes.Where((x) => x.ProcessName == "Eternal").FirstOrDefault();

            if (eternalProcess != null)
            {
                EternalHandle = eternalProcess.MainWindowHandle;
            }
        }

        private void redTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
