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
        private string key;

        public string Key
        {
            get
            {
                return key;
            }

            set
            {
                key = value;
            }
        }

        public async Task CheckKey()
        {
            try
            {
                Tinify.Key = "Wud9BHyk6PGEALoQJ94O00HHrkfFkAky";
                await Tinify.Validate();
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                // Validation of API key failed.
            }
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
