using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace sync
{
    class Sync
    {

        private List<int> _indexes;

        private List<string[]> _files;

        private DirectoryInfo _srcFolder;
        private DirectoryInfo _dstFolder;

        public delegate void dOnFileCopied(int count);
        public event dOnFileCopied OnFileCopied;

        private Exception _lastEx;

        public Sync(string src, string dst)
        {
            _srcFolder = new DirectoryInfo(src);
            _dstFolder = new DirectoryInfo(dst);
        }

        private void _estimate()
        {
            var hasSize = (new DriveInfo(_dstFolder.Root.ToString())).AvailableFreeSpace;
            _files = new List<string[]>();
            long sumSize = 0;
            
            var files = _srcFolder.GetFiles("*.mp3", SearchOption.AllDirectories);
            var length = files.Length;
            _indexes = new List<int>();
            for (var i = 0; i < length; i++)
            {
                var indx = index(length);
                var curFile = files[indx];
                sumSize += curFile.Length;
                if (sumSize > hasSize)
                {
                    return;
                }
                _files.Add(new string[] { curFile.FullName, _dstFolder.FullName + "/" + curFile.Name });
            }
        }

        public void run()
        {
            _estimate();
            long totalCount = _files.Count;
            long i = 1;
            foreach (string[] fileInfo in _files)
            {
                Application.DoEvents();

                File.Copy(fileInfo[0], fileInfo[1]);

                if (OnFileCopied != null)
                    OnFileCopied(Convert.ToInt32(Math.Round((double)(i * 100 / totalCount), 1)));

                Application.DoEvents();
                i++;
            }
        }

        int index(int max)
        {
            var rnd = new Random();
            var cur = (int)rnd.Next(max);
            if (_indexes.Contains(cur))
            {
                return index(max);
            }
            _indexes.Add(cur);
            return cur;
        }
    }
}
