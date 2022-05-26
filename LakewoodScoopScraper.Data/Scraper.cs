using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LakewoodScoopScraper.Scraper
{
    public static class Scraping
    {
        public static List<LakewoodScoopResult> ScrapeLakewoodScoop()
        {
            var results = new List<LakewoodScoopResult>();
            var html = GetLakewoodScoopHtml();
            var parser = new HtmlParser();
            var document = parser.ParseDocument(html);
            IHtmlCollection<IElement> postElement = document.QuerySelectorAll("div.post");

            foreach (var result in postElement)
            {
                var lakewoodScoopResult = new LakewoodScoopResult();

                var img = result.QuerySelector("img");
                if (img != null)
                {
                    lakewoodScoopResult.ImageUrl = img.Attributes["src"].Value;
                }


                var comments = result.QuerySelector("div.backtotop");
                lakewoodScoopResult.AmountOfComments = comments.TextContent;

                var titleUrl = result.QuerySelector("h2");
                if (titleUrl == null)
                {
                    continue;
                }
                lakewoodScoopResult.Title = titleUrl.TextContent;

                var link = titleUrl.QuerySelector("a");
                lakewoodScoopResult.LinkUrl = link.Attributes["href"].Value;

                var blurbs = result.QuerySelectorAll("p");
                foreach (var blurb in blurbs)
                {
                    if (blurb == null)
                    {
                        continue;
                    }

                    lakewoodScoopResult.BlurbOfText += blurb.TextContent.Replace("Read more ›", String.Empty);
                }

                results.Add(lakewoodScoopResult);

            }



            return results;

        }
        private static string GetLakewoodScoopHtml()
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            string url = "https://www.thelakewoodscoop.com";
            var client = new HttpClient(handler);
            return client.GetStringAsync(url).Result;
        }
    }
}
