using System;
using System.Net;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AutoUpdater
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string url = Console.ReadLine();
            await DownloaderAsync(url);
            Console.ReadKey();
        }

        static async Task DownloaderAsync(string url)
        {
            using (WebClient web = new WebClient())
            {
                Uri uri = new Uri(url);
                Task.Run(() =>
                {
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFileAsync(uri, $@"{Environment.GetFolderPath(Environment.SpecialFolder.InternetCache)}/load.exe");
                        client.DownloadFileCompleted += (s, a) =>
                        {
                            Console.WriteLine("\nDownload is finished");
                            Console.WriteLine($@"Directory: {Environment.GetFolderPath(Environment.SpecialFolder.InternetCache)}");
                            Console.WriteLine("Start downloaded file...");
                            Process.Start($@"{Environment.GetFolderPath(Environment.SpecialFolder.InternetCache)}/load.exe");
                        };
                        client.DownloadProgressChanged += (s, a) =>
                        {
                            Console.WriteLine($"Downloaded: {a.ProgressPercentage}%");
                        };
                    }
                }).Wait();
                await Task.Delay(10);
            }
            //ZipFile.ExtractToDirectory($@"{Environment.GetFolderPath(Environment.SpecialFolder.InternetCache)}", $@"{Environment.GetFolderPath(Environment.SpecialFolder.InternetCache)}");
        }
    }
}
