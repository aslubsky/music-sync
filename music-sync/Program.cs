using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace mysic_sync
{
    class Program
    {
        static List<int> indexes;

        static void Main(string[] args)
        {
            string src = @"E:\Музика\Саша\";
            string dst = @"H:\Music\";

            var folder = new DirectoryInfo(src);
            var files = folder.GetFiles("*.mp3", SearchOption.AllDirectories);
            var length = files.Length;
            indexes = new List<int>();
            for (var i = 0; i < length; i++)
            {
                try 
                {
                    var indx = index(length);
                    var curFile = files[indx];
                    File.Copy(curFile.FullName, dst + curFile.Name);
                }
                catch (Exception ex) 
                {
                    Console.Out.WriteLine(ex.Message);
                    break;
                }
            }
            Console.Out.WriteLine("Done");
            Console.In.ReadLine();
        }

        static int index(int max)
        {
            var rnd = new Random();
            var cur = (int)rnd.Next(max);
            if (indexes.Contains(cur)) 
            {
                return index(max);
            }
            indexes.Add(cur);
            return cur;
        }
    }
}
