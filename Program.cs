using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Flurl.Http;
using HtmlAgilityPack;
namespace gal_dl
{
    class Program
    {
        static bool OSisWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        static async Task Main(string[] args)
        {
        start:
            Console.Title = "Gallery Downloader";
            Console.BackgroundColor = ConsoleColor.Black;

            Logo();

            System.Console.WriteLine(OSisWindows);

            bool ValidInput = false;
            int input = 0;

            while (!ValidInput)
            {
                Console.WriteLine("[1] Single Download");
                Console.WriteLine("[2] Multi Download");
                Console.WriteLine("[3] Exit");
                Console.Write("==> ");
                int temp = int.Parse(Console.ReadLine());
                if (temp > 3)
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
                    Console.WriteLine($"Again?(y/n)");
                    if (Console.ReadLine() == "y")
                    {
                        goto start;
                    }
                    else
                    {

                    }
                    break;

                case 2:
                    Console.WriteLine("[+] Multi Download Mode");
                    Console.Write("Type in URLs seperated by '+' => ");
                    string URLs = Console.ReadLine();
                    foreach (string item in URLs.Split("+"))
                    {
                        await DownloadAsync(item);
                    }
                    Console.WriteLine($"Again?(y/n)");
                    if (Console.ReadLine() == "y")
                    {
                        goto start;
                    }
                    else
                    {

                    }
                    break;

                case 3:
                    Logo();

                    break;

                default:
                    Logo();
                    break;
            }
            Logo();

        }

        public static async Task DownloadAsync(string URL)
        {
            Logo();
            string folderName = URL.Remove(0, 35).Replace("/", "");
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(URL);

            string path;

            if (OSisWindows)
            {
                path = $"C:\\Users\\{Environment.UserName}\\gal-dl\\{folderName}";
            }
            else
            {
                path = "/home/gal-dl";
            }

            if (!Directory.Exists(path))
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

        public static void Logo()
        {
            Console.Clear();
            Console.WriteLine(@"

  _    _                              _____          _           
 | |  | |                            / ____|        | |          
 | |__| | ___  _ __ _ __   ___ _   _| |     ___   __| | ___ _ __ 
 |  __  |/ _ \| '__| '_ \ / _ \ | | | |    / _ \ / _` |/ _ \ '__|
 | |  | | (_) | |  | | | |  __/ |_| | |___| (_) | (_| |  __/ |   
 |_|  |_|\___/|_|  |_| |_|\___|\__, |\_____\___/ \__,_|\___|_|   
                                __/ |                            
                               |___/                             

");
        }
    }
}
