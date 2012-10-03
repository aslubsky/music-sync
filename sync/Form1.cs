using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace sync
{
    public partial class Form1 : Form
    {
        Sync _sync;
        public Form1()
        {
            InitializeComponent();
                        
            string src = @"E:\Музика\Саша\";
            string dst = @"H:\Music\";

            _sync = new Sync(src, dst);
            _sync.OnFileCopied += new Sync.dOnFileCopied(sync_OnFileCopied);
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
    }
}
