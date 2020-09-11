using System;
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
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Clear();

            string URL = "https://www.pornpics.com/galleries/puba-network-anna-bell-peaks-jenevieve-hexxx-74787898/";
            HtmlWeb web = new HtmlWeb();
            FlurlRequest f = new FlurlRequest(URL);
            var htmlDoc = web.Load(URL);

            foreach (HtmlNode node in htmlDoc.DocumentNode.SelectNodes("//a[@class='rel-link']"))
            {
                string img = node.GetAttributeValue("href", null).ToString();
                await img.DownloadFileAsync(@"C:\\Users\\Amir\\Reddit");
                Console.WriteLine(img);
            }
        }
    }
}
