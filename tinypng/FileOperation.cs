using System;
using System.Collections.Generic;
using System.IO;

public partial class FileGet
{
    /// <summary>
    /// 私有变量
    /// </summary>
    private static List<FileInfo> lst = new List<FileInfo>();
    /// <summary>
    /// 获得目录下所有文件或指定文件类型文件(包含所有子文件夹)
    /// </summary>
    /// <param name="path">文件夹路径</param>
    /// <param name="extName">扩展名可以多个 例如 .mp3.wma.rm</param>
    /// <returns>List<FileInfo></returns>
    public static List<FileInfo> getFile(string path, string extName)
    {
        lst.Clear();
        getdir(path, extName);
        return lst;
    }
    /// <summary>
    /// 私有方法,递归获取指定类型文件,包含子文件夹
    /// </summary>
    /// <param name="path"></param>
    /// <param name="extName"></param>
    private static void getdir(string path, string extName)
    {
        try
        {
            if (File.Exists(path))
            {
                string finame = Path.GetFileName(path);
                if (File.Exists(path))
                {
                    DirectoryInfo fdir = new DirectoryInfo(path + "/../");
                    FileInfo[] file = fdir.GetFiles();
                    for (int i = 0; i < file.Length; i++)
                    {
                        if (finame == file[i].Name)
                        {
                            lst.Add(file[i]);
                        }
                    }
                }
            }
            else
            {
                string[] dir = Directory.GetDirectories(path); //文件夹列表   
                DirectoryInfo fdir = new DirectoryInfo(path);
                FileInfo[] file = fdir.GetFiles();
                //FileInfo[] file = Directory.GetFiles(path); //文件列表   
                if (file.Length != 0 || dir.Length != 0) //当前目录文件或文件夹不为空                   
                {
                    foreach (FileInfo f in file) //显示当前目录所有文件   
                    {
                        string[] exstrshuz = extName.Split('|');
                        for (int i = 0; i < exstrshuz.Length; i++)
                        {
                            if (f.Name.EndsWith(exstrshuz[i]))
                            {
                                if (System.Math.Ceiling(f.Length / 1024.0) > 20)
                                {
                                    lst.Add(f);
                                }
                            }
                        }
                    }
                    foreach (string d in dir)
                    {
                        getdir(d, extName); //递归   
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw ex;
        }
    }
}


public partial class FileGet1
{
    /// <summary>
    /// 获得目录下所有文件或指定文件类型文件(包含所有子文件夹)
    /// </summary>
    /// <param name="path">文件夹路径</param>
    /// <param name="extName">扩展名可以多个 例如 .mp3.wma.rm</param>
    /// <returns>List<FileInfo></returns>
    public static List<FileInfo> getFile(string path, string extName)
    {
        try
        {
            List<FileInfo> lst = new List<FileInfo>();
            string[] dir = Directory.GetDirectories(path); //文件夹列表   
            DirectoryInfo fdir = new DirectoryInfo(path);
            FileInfo[] file = fdir.GetFiles();
            //FileInfo[] file = Directory.GetFiles(path); //文件列表   
            if (file.Length != 0 || dir.Length != 0) //当前目录文件或文件夹不为空                   
            {
                foreach (FileInfo f in file) //显示当前目录所有文件   
                {
                    if (extName.ToLower().IndexOf(f.Extension.ToLower()) >= 0)
                    {
                        lst.Add(f);
                    }
                }
                foreach (string d in dir)
                {
                    getFile(d, extName);//递归   
                }
            }
            return lst;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw ex;
        }
    }

}