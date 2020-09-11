using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http;
using HtmlAgilityPack;
namespace gal_dl
{
    class Program
    {
        static void Main(string[] args)
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
                    SingleDownloadAsync();
                break;

                case 2:
                    MultiDownloadAsync();
                break;

                case 3:
                break;

                default:
                break;
            }


        }

        public static async void SingleDownloadAsync()
        {
            Console.Clear();
            Console.WriteLine("[+] Single Download Mode");
            Console.Write("Type in a URL to get started => ");
            string URL = Console.ReadLine();
            string folderName = URL.Remove(0, 35).Replace("/", "");
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(URL);
            string path = $@"C:\\Users\\{Environment.UserName}\\gal-dl\\{folderName}";
            Console.WriteLine(path);
            foreach (HtmlNode node in htmlDoc.DocumentNode.SelectNodes("//a[@class='rel-link']"))
            {
                string img = node.GetAttributeValue("href", null).ToString();
                Console.WriteLine($"Downloading => {img}");
                await img.DownloadFileAsync(path);
            }
            Console.WriteLine("Finished Downlaoding!");
        }

        public static async void MultiDownloadAsync()
        {
            Console.Clear();
            Console.WriteLine("[+] Multi Download Mode");
            Console.Write("Type in URL seperated by '+' => ");
            string URLs = Console.ReadLine();
            List<string> url_list = new List<string>();
            foreach (string url in URLs.Split("+"))
            {
                url_list.Add(url.Remove(0, 35).Replace("/", ""));
            }
            
            // HtmlWeb web = new HtmlWeb();
            // var htmlDoc = web.Load(URL);

            // foreach (HtmlNode node in htmlDoc.DocumentNode.SelectNodes("//a[@class='rel-link']"))
            // {
            //     string img = node.GetAttributeValue("href", null).ToString();
            //     Console.WriteLine($"Downloading => {img}");
            //     await img.DownloadFileAsync(@"C:\\Users\\Amir\\gal-dl");
            // }
            // Console.WriteLine("Finished Downlaoding!");
        } 
    }
}
