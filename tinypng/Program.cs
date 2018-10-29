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
            //path = "E:/ShanHe1.0.1/Assets";
            path = "E:/ShanHe1.0.1/Assets/Textures/UI/BuildName/tips.png";
            //path = "E:/ShanHe1.0.1/Assets/Textures/UI/Dynamic/Common";

            compression pngco = new compression();
            Task<bool> kje =compression.CheckKey();
            kje.GetAwaiter();
            int k = pngco.GeNumberOfCompressions();
            List< FileInfo > keList= FileGet.getFile(path, ".png|.jpg");
            int ke = 0;
            int index = -1;
            for (int i = 0; i < keList.Count; i++)
            {
                //Thread threadB = new Thread(
                //() =>
                //{
                //    index++;
                //    if (index < keList.Count)
                //    {
                //        compression.compressionPng(keList[index].FullName, delegate ()
                //        {
                //            ke++;
                //            Console.WriteLine("已经压缩完成" + ke);
                //        }).Wait();
                //    }
                //}
                //);
                //threadB.Start();
                compression.compressionPng(keList[i].FullName, delegate ()
                {
                    ke++;
                    Console.WriteLine("已经压缩完成" + ke);
                }).Wait();
            }
            while (ke < keList.Count - 1)
            {
                Console.ReadKey();
            }
        }
    }
}
