using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puppetmaster
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //Log Information show
            Sys.box = rtb1;
            Plugins.box = rtb1;
            SysMain.box = rtb2;
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) //Exit the Form
        {
            if (Sys._isRunning) Sys.Stop();
            if (SysMain._isRunning) SysMain.Stop();
            Environment.Exit(0);
        }



        private void startToolStripMenuItem_Click(object sender, EventArgs e)  //Start Button
        {
            //Start the Engine
           Sys.Start();
           SysMain.Start();

        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e) //Stop Button
        {
            //Stop the Engine
            Sys.Stop();
            SysMain.Stop();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) //Exit Button
        {
            if (Sys._isRunning) Sys.Stop();
            if (SysMain._isRunning) SysMain.Stop();
            Environment.Exit(0);
        }
    }
}
