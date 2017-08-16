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
                Image im = sc.CaptureWindow(EternalHandle);
                if (pictureBox1.Image != null)
                {
                    var old = pictureBox1.Image;
                    old.Dispose();

                }
                
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Image = im;

            }
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
    }
}
