using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace tinypng
{
    class Program
    {
        private static string path;
        static void Main(string[] args)
        {
            path = "E:/ShanHe1.0.1/Assets/Models";
            compression pngco = new compression();
            pngco.CheckKey().Wait();

            int k = pngco.GeNumberOfCompressions();
            List< FileInfo > keList= FileGet.getFile(path, ".png.jpg");
            int ke = 0;
            int index = 0;
            for (int i = 0; i < keList.Count; i++)
            {
                Task tes=Task.Run(() =>
                {
                    FileInfo tanseInfo = keList[index];
                    compression.compressionPng(tanseInfo.FullName, delegate ()
                    {
                        ke++;
                        Console.WriteLine("已经压缩完成" + ke);
                    });
                    index++;
                    Console.WriteLine("启动Task=" + tanseInfo.FullName);
                });
            }
            while (ke < keList.Count - 1)
            {
                Console.ReadKey();
            }
        }
    }
}
