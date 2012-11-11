using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace sync
{
    public partial class Form1 : Form
    {
        Sync _sync;
        public Form1()
        {
            InitializeComponent();
                        
           /* string src = @"E:\Музика\Саша\";
            string dst = @"H:\Music\";*/

            var sourceLocation = sync.Properties.Settings.Default["sourceLocation"].ToString();
            if (sourceLocation.Length != 0)
            {
                this.label2.Text = sourceLocation;
            }

            var detstLocation = sync.Properties.Settings.Default["detstLocation"].ToString();
            if (detstLocation.Length != 0)
            {
                this.label4.Text = detstLocation;
            }

            checkReady();
        }

        private void checkReady()
        {
            var sourceLocation = sync.Properties.Settings.Default["sourceLocation"].ToString();
            var detstLocation = sync.Properties.Settings.Default["detstLocation"].ToString();
            if (sourceLocation.Length != 0 && detstLocation.Length != 0)
            {
                _sync = new Sync(sourceLocation, detstLocation);
                _sync.OnFileCopied += new Sync.dOnFileCopied(sync_OnFileCopied);

                button1.Enabled = true;
                button2.Enabled = true;
            }
        }

        void sync_OnFileCopied(int count)
        {
            progressBar1.Value = count;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            _sync.run();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                sync.Properties.Settings.Default["sourceLocation"] = this.label2.Text = folderBrowserDialog1.SelectedPath;
                sync.Properties.Settings.Default.Save();
                checkReady();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                sync.Properties.Settings.Default["detstLocation"] = this.label4.Text = folderBrowserDialog1.SelectedPath;
                sync.Properties.Settings.Default.Save();
                checkReady();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var dest = sync.Properties.Settings.Default["detstLocation"].ToString();
            if (dest.Length > 0 && Directory.Exists(dest)) 
            {
                Directory.Delete(dest, true);
                Directory.CreateDirectory(dest);
            }
        }
    }
}
