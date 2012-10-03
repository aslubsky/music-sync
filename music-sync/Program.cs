using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mysic_sync
{
    class Program
    {
        static int[] indexes;

        static void Main(string[] args)
        {
            string src = @"E:\Музика\Саша\";
            string dst = @"H:\Music\";

            var folder = new DirectoryInfo(src);
            var files = folder.GetFiles("*.mp3", SearchOption.AllDirectories);
            var length = files.Length;
            for (var i = 0; i < length; i++)
            {
                /*try 
                {*/
                    var indx = index(length);
                    var fileName = files[indx];
                    File.Copy(src + fileName, dst + fileName);
                /*}
                catch (Exception ex) 
                { 

                }*/
            }
        }

        static int index(int max)
        {
            var rnd = new Random();
            var cur = (int)rnd.Next(max);
            if (indexes.Contains(cur)) 
            {
                return index(max);
            }
            indexes[indexes.Length] = cur;
            return cur;
        }
    }
}
