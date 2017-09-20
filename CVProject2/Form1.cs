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
        

        private void goodExamplesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] pics = Directory.GetFiles(gpath);

            int n = 0, m = 0;
            foreach (string p in pics)
            {
                var img = Image.FromFile(p);
                
                img = (Image) new Bitmap(img, new System.Drawing.Size(150,100));                

                var picture = new PictureBox
                {
                    Name = "pictureBox"+n.ToString(),
                    Size = new System.Drawing.Size(150, 100),
                    Location = new System.Drawing.Point(150*n, 25+100*m),
                    Image = img
                };
                this.Controls.Add(picture);

                n++;
                if(n%5==0 && n!=0) { m++; n = 0; }
                    
            }
        }
    }
}
