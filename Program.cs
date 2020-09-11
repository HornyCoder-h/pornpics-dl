using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Flurl.Http;
using HtmlAgilityPack;
namespace gal_dl
{
    class Program
    {
        static async Task Main(string[] args)
        {

            Console.Title = "Gallery Downloader";
            Console.BackgroundColor = ConsoleColor.Black;
            // Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Clear();
            
            bool ValidInput = false;
            int input = 0;

            while(!ValidInput)
            {
                Console.WriteLine("[1] Single Download");
                Console.WriteLine("[2] Multi Download");
                Console.WriteLine("[3] Exit!");
                Console.Write("==> ");
                int temp = int.Parse(Console.ReadLine());
                if(temp > 3)
                {
                    Console.WriteLine("[-] Invalid Input!");
                }
                else
                {
                    input = temp;
                    ValidInput = true;
                }
            }

            GC.Collect();



            switch (input)
            {
                case 1:
                    Console.WriteLine("[+] Single Download Mode");
                    Console.Write("Type in a URL => ");
                    string url = Console.ReadLine();
                    await DownloadAsync(url);
                break;

                case 2:
                    Console.WriteLine("[+] Multi Download Mode");
                    Console.Write("Type in URL seperated by '+' => ");
                    string URLs = Console.ReadLine();
                    List<string> url_list = new List<string>();
                    foreach (string item in URLs.Split("+"))
                    {
                        await DownloadAsync(item);
                    }
                break;

                case 3:
                break;

                default:
                break;
            }


        }

        public static async Task DownloadAsync(string URL)
        {
            // https://www.pornpics.com/galleries/puba-network-anna-bell-peaks-jenevieve-hexxx-74787898/
            Console.Clear();
            string folderName = URL.Remove(0, 35).Replace("/", "");
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(URL);

            string path = $"C:\\Users\\{Environment.UserName}\\gal-dl\\{folderName}";
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Console.WriteLine($">>>>>Downloading {folderName}<<<<<");
            foreach (HtmlNode node in htmlDoc.DocumentNode.SelectNodes("//a[@class='rel-link']"))
            {
                string img = node.GetAttributeValue("href", null).ToString();
                Console.WriteLine($"Downloading => {img}");
                await img.DownloadFileAsync(path);
            }
            Console.WriteLine("Finished Downlaoding!");
            GC.Collect();
        }
    }
}
