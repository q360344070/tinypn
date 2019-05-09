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
            path = "C:\\Users\\Administrator\\Desktop\\友宝小游戏文档\\友宝便利店\\切图";
            //path = "E:/ShanHe1.0.1/Assets/Textures/UI/BuildName/tips.png";
            //path = "E:/ShanHe1.0.1/Assets/Textures/UI/Dynamic/Common";

            compression pngco = new compression();
            Task<bool> kje =compression.CheckKey();
            kje.GetAwaiter();
            int k = pngco.GeNumberOfCompressions();
            List< FileInfo > keList= FileGet.getFile(path, ".png|.jpg");
            List<Task> tasklist = new List<Task>();
            int ke = 0;
            bool isCamp = false;
            bool flag = true;
            while (ke < keList.Count - 1)
            {
                flag = !(ke < keList.Count - 1);
                //Sleep
                if (!isCamp)
                {
                    isCamp = true;

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
                        string namefile = keList[i].FullName;
                        Task t2 = new Task(() => {
                            compression.compressionPng(namefile, delegate ()
                            {
                                ke++;
                                Console.WriteLine("已经压缩完成" + ke);
                            }).Wait();
                        });
                        tasklist.Add(t2);
                    }
                    int counout = 0;
                    List<Task> tasklist1 = new List<Task>();
                    for (int j = tasklist.Count-1; j >-1; j--)
                    {
                        tasklist[j].Start();
                        tasklist1.Add(tasklist[j]);
                        tasklist.RemoveAt(j);
                        counout++;
                        if (counout > 18)
                        {
                            Task.WaitAll(tasklist1.ToArray());
                            tasklist1.Clear();
                            counout = 0;
                        }
                    }
                    Task.WaitAll(tasklist1.ToArray());
                }
                Thread.Sleep(1);
            }
        }
    }
}
