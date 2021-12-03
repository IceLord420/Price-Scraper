using HtmlAgilityPack;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Price_Scraper
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "https://www.emag.bg/igri-konzola-kompiutyr/c?ref=hp_menu_quick-nav_3096_10&type=category";
            string selector = "//div[@class='card-item card-standard js-product-data']/div/div/div[3]/a";
            Console.OutputEncoding = Encoding.UTF8;

            GenerateCSV(url, selector);

            string[] urls = File.ReadAllLines("./links.csv");

            Scraper.ScrapePrices(urls);

        }


        public static void GenerateCSV(string mainUrl, string linkSelector)
        {
            Console.WriteLine("Creating csv file");

            if (File.Exists("./links.csv"))
            {
                Console.WriteLine("CSV already created. Skipping.");
                return;
            }

            var web = new HtmlWeb();
            var doc = web.Load(mainUrl);

            var links = doc.DocumentNode.SelectNodes(linkSelector)
                .Select(node => node.Attributes["href"].Value);

            var output = string.Join(Environment.NewLine, links);

            File.WriteAllText("./links.csv", output);
            Console.WriteLine("csv file created.");
        }
    }
}

