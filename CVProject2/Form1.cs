using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Collections;

using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
using OpenCvSharp.Blob;
using OpenCvSharp.Extensions;
using OpenCvSharp.UserInterface;
namespace CVProject2
{
    public partial class Form1 : Form
    {
        String gpath = System.IO.Path.GetFullPath(@"..\..\Good");
        String bpath = System.IO.Path.GetFullPath(@"..\..\Bad");

        public Form1()
        {
            InitializeComponent();
        }              
        
        private void run(String filepath)
        {
            string[] pics = Directory.GetFiles(filepath);

            int n = 0;
            foreach (string p in pics)
            {
                //create hough line image
                Mat imageIn = Cv2.ImRead(p, LoadMode.GrayScale);//.Resize(new OpenCvSharp.CPlusPlus.Size(150, 100));
                Mat edges = new Mat();
                Cv2.Canny(imageIn, edges, 95, 100);
                CvLineSegmentPoint[] segHoughP = Cv2.HoughLinesP(edges, 1, Math.PI / 180, 100, 100, 10);
                Mat imageOutP = imageIn.EmptyClone();
                foreach (CvLineSegmentPoint s in segHoughP)
                    imageOutP.Line(s.P1, s.P2, Scalar.White, 1, LineType.AntiAlias, 0);

                //draw original image
                var img = Image.FromFile(p);
                var picture = new PictureBox
                {
                    Name = "pictureBox" + n.ToString() + "1",
                    Size = new System.Drawing.Size(400, 300),
                    Location = new System.Drawing.Point(0, 25 + 300 * n),
                    Image = (Image)new Bitmap(img, new System.Drawing.Size(400, 300))
                };
                this.Controls.Add(picture);

                //draw hough lines image next to it
                var picture2 = new PictureBox
                {
                    Name = "pictureBox" + n.ToString() + "2",
                    Size = new System.Drawing.Size(400, 300),
                    Location = new System.Drawing.Point(400, 25 + 300 * n),
                    Image = imageOutP.Resize(new OpenCvSharp.CPlusPlus.Size(400, 300)).ToBitmap()
                };
                this.Controls.Add(picture2);

                n++;
            }
        }

        private void goodExamplesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int ix = this.Controls.Count - 1; ix >= 0; ix--)            
                if (this.Controls[ix] is PictureBox) this.Controls[ix].Dispose();
            
            run(gpath);
        }

        private void badExamplesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int ix = this.Controls.Count - 1; ix >= 0; ix--)            
                if (this.Controls[ix] is PictureBox) this.Controls[ix].Dispose();
            
            run(bpath);
        }
    }
}
