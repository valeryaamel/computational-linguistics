using HtmlAgilityPack;
using System.Globalization;
using System.Linq;

namespace htmltest
{
    internal class Repository
    {
        public static bool upToDate = false;
        public static List<string> GetLinks(string url, string page)
        {
            List<string> links = new List<string>();

            try
            {
                var web = new HtmlWeb();
                var doc = web.Load(url + "/text/" + page);


                var nodes = doc.DocumentNode.SelectNodes("//article[@data-test='archive-record-item']//a[@data-test='archive-record-header']");
                if (nodes != null)
                {
                    foreach (var item in nodes)
                    {

                        string str = item.Attributes["href"].Value;
                        links.Add(url + str);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return links;
        }

        public static Task<List<string>> GetLinksAsync(string url, string page)
        {
            return Task.Run(() => GetLinks(url, page));
        }

        public static List<News> GetNews(List<string> links)
        {

            List<News> news = new List<News>();
            foreach (var link in links)
            {
                try
                {
                    Console.WriteLine(link);

                    News n = new News();

                    n.Link = link;

                    var web = new HtmlWeb();

                    HtmlDocument doc;

                    doc = web.Load(link);

                    var node = doc.DocumentNode.SelectSingleNode("//div[@itemprop='datePublished']//a[@href]");

                    n.Date = (node != null) ? Convert.ToDateTime(node.InnerText) : null;

                    node = doc.DocumentNode.SelectSingleNode("//h1[@itemprop='headline']");

                    n.Title = (node != null) ? node.InnerText : null;

                    var nodes = doc.DocumentNode.SelectNodes("//div[@itemprop='articleBody']//div[@class]//div//p");


                    if (nodes != null)
                    {
                        n.Text = "";
                        foreach (var item in nodes)
                        {
                            if (item.InnerText != "Поделиться")
                                n.Text += item.InnerText + "\n";
                        }
                    }
                    else n.Text = null;


                    node = doc.DocumentNode.SelectSingleNode("//div[@id='record-header']//div[@data-test='record-stats-view']//span");

                    n.Views = (node != null) ? int.Parse(node.InnerText, NumberStyles.AllowThousands) : null;


                    node = doc.DocumentNode.SelectSingleNode("//div[@id='record-header']//span[@data-test='record-stats-comments-count']");

                    n.Comments = (node != null) ? int.Parse(node.InnerText, NumberStyles.AllowThousands) : null;

                    news.Add(n);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            return news;
        }

        public static List<News> GetNewNews(List<string> links, DateTime currentDate)
        {
            List<News> news = new List<News>();
            foreach (var link in links)
            {
                try
                {
                    News n = new News();

                    n.Link = link;

                    var web = new HtmlWeb();

                    HtmlDocument doc;

                    doc = web.Load(link);

                    var node = doc.DocumentNode.SelectSingleNode("//div[@itemprop='datePublished']//a[@href]");

                    n.Date = (node != null) ? Convert.ToDateTime(node.InnerText) : null;

                    if (n.Date <= currentDate)
                    {
                        upToDate = true;
                        break;
                    }

                    n.Date = n.Date.Value.AddHours(3);

                    node = doc.DocumentNode.SelectSingleNode("//h1[@itemprop='headline']");

                    n.Title = (node != null) ? node.InnerText : null;

                    var nodes = doc.DocumentNode.SelectNodes("//div[@itemprop='articleBody']//div[@class]//div//p");


                    if (nodes != null)
                    {
                        n.Text = "";
                        foreach (var item in nodes)
                        {
                            if (item.InnerText != "Поделиться")
                                n.Text += item.InnerText + "\n";
                        }
                    }
                    else n.Text = null;


                    node = doc.DocumentNode.SelectSingleNode("//div[@id='record-header']//div[@data-test='record-stats-view']//span");

                    n.Views = (node != null) ? int.Parse(node.InnerText, NumberStyles.AllowThousands) : null;


                    node = doc.DocumentNode.SelectSingleNode("//div[@id='record-header']//span[@data-test='record-stats-comments-count']");

                    n.Comments = (node != null) ? int.Parse(node.InnerText, NumberStyles.AllowThousands) : null;

                    news.Add(n);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            return news;
        }

        public static Task<List<News>> GetNewsAsync(List<string> links, DateTime currentDate)
        {
            return Task.Run(() => GetNewNews(links, currentDate));
        }
        
        public static async Task<List<List<News>>> Parser(DateTime currentDate)
        {
            var myStopwatch = new System.Diagnostics.Stopwatch();
            myStopwatch.Start();

            List<List<News>> News = new();
            var url = "https://v1.ru";
            var page = "";

            List<string> links = await GetLinksAsync(url, page);

            List<News> news = await GetNewsAsync(links, currentDate);

            News.Add(news);

            for (int i = 2; i <= 50; i++)
            {
                if (upToDate) break;
                try
                {
                    page = $"?page={i}";

                    links = await GetLinksAsync(url, page);

                    news = await GetNewsAsync(links, currentDate);

                    News.Add(news);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            myStopwatch.Stop();
            Console.WriteLine("Время = {0}", myStopwatch.Elapsed);

            return News;
        }

        public static async Task<List<News>> Resrtuct(DateTime currentDate)
        {
            List<List<News>> news = await Parser(currentDate);
            List<News> n = new List<News>();
            foreach (var item in news)
            {
                n = n.Concat(item).ToList();
            }
            return n;
        }
    }
}
