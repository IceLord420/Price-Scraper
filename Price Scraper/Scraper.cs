using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Price_Scraper
{
    class Scraper
    {
        public static void ScrapePrices(string[] urls)
        {
            for (int j = 0; j < urls.Length; j++)
            {
                Thread t = new Thread(new ParameterizedThreadStart(ScraperTask));
                t.Start(urls[j]);
            }

            Thread.Sleep(1000);
        }

        private static void ScraperTask(object value)
        {
            string url = (string)value;

            var web = new HtmlWeb();
            var doc = web.Load(url);

            var name = doc.DocumentNode.SelectSingleNode("/html/body/div[3]/div[2]/div/section[1]/div/div[2]/div/div[1]/h1").InnerText;
            var price = doc.DocumentNode.SelectSingleNode("/html/body/div[3]/div[2]/div/section[1]/div/div[2]/div/div[2]/div[2]/div/div/div[2]/form/div[1]/div[1]/div/div/p[2]").InnerText;

            Console.WriteLine("| {0} | is with Price: {1}", name.Trim(), price.Trim());
        }
    }
}
