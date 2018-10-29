using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinifyAPI;

namespace tinypng
{
    class compression
    {
        private static Dictionary<string,bool> key;

        static compression()
        {
            key = new Dictionary<string, bool>();
            key["Wud9BHyk6PGEALoQJ94O00HHrkfFkAky"] = false;
            key["EkgWF0hqmlEyr36s1Ni7OgpS5KO3KFUs"] = false;
            key["Wsp60anj6d9IWUapUeDBdmHvCAd9fuzd"] = false;
            key["WcKzY1a1ncYnxlOEfJ5cnAW1n32EIKKG"] = false;
            key["FbOyNDJBlQFeDMJJkynoxEHFQ3llleyq"] = false;
            key["GHxAWm9E10nuq84RTEIdiAIIbQz4XWza"] = false;
        }

        public static string GetUnusedKeys()
        {
            string str = string.Empty;
            foreach (var VARIABLE in key)
            {
                if (!VARIABLE.Value)
                {
                    str = VARIABLE.Key;
                }
            }
            return str;
        }
        public static Task<bool> CheckKey()
        {
            string zcf = GetUnusedKeys();
            if (zcf != String.Empty)
            {
                Tinify.Key = zcf;
            }
            else
            {
                Console.WriteLine("keys已经使用完成请添加可用的key");
                Console.ReadKey();
                ;
            }
            return Tinify.Validate();
        }
        public int GeNumberOfCompressions()
        {
            var compressionsThisMonth = Tinify.CompressionCount;
            if (compressionsThisMonth == null)
            {
                return 0;
            }
            return (int) compressionsThisMonth.Value;
        }
        public static async Task compressionPng(string path,Action claa)
        {
            try
            {
                var source = Tinify.FromFile(path);
                await source.ToFile(path);
                if (claa != null)
                {
                    claa.Invoke();
                }
            }
            catch (AccountException e)
            {
                System.Console.WriteLine("The error message is:key压缩数量已达到上限 " + e.Message);
                key[Tinify.Key] = true;
                CheckKey();
                compressionPng(path, claa).Wait();
                // Verify your API key and account limit.
            }
            catch (ClientException e)
            {
                // Check your source image and request options.
                Console.WriteLine("由于提交的数据存在问题，无法完成请求。异常消息将包含更多信息。您不应该重试该请求。");
            }
            catch (ServerException e)
            {
                // Temporary issue with the Tinify API.
                Console.WriteLine("由于Tinify API存在临时问题，无法完成请求。几分钟后重试请求是安全的。如果您在较长时间内反复看到此错误，请 与我们联系。");
            }
            catch (ConnectionException e)
            {
                Console.WriteLine("无法发送请求，因为连接到Tinify API时出现问题。您应该验证您的网络连接。重试请求是安全的。");

                compressionPng(path, claa).Wait();
                // A network connection error occurred.
            }
            catch (System.Exception e)
            {
                // Something else went wrong, unrelated to the Tinify API.
                Console.WriteLine(e.Message);
            }
        }
    }
}
