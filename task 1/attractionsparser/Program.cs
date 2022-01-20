using HtmlAgilityPack;

string url = "https://avolgograd.com/sights?obl=vgg";

var web = new HtmlWeb();
var doc = web.Load(url);

var nodes = doc.DocumentNode.SelectNodes("//div[@id='afisha-content']//a[@href]");

List<string> attractions = new List<string>();

foreach (var node in nodes)
{
    if(!string.IsNullOrEmpty(node.InnerText))
        attractions.Add(node.InnerText);
}

List<string> lines = new();

foreach (var item in attractions)
{
    string[] line =
    {
        $"PersonsNames \"{item.Replace(" ", "_")}\"",
        "{",
        $"\tkey = \"{item}\"",
        $"\tlemma = \"{item}\"",
        "}",
        " "
    };
    foreach (var items in line)
    {
        lines.Add(items);
    }
}

File.WriteAllLines("WriteLines.txt", lines.ToArray());