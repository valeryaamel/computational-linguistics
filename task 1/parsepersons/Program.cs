// See https://aka.ms/new-console-template for more information
using HtmlAgilityPack;
using System.Globalization;

string url = "https://global-volgograd.ru/person";
string postfix = "";
int offset = 20;

var web = new HtmlWeb();
var doc = web.Load(url);

List<string> persons = new List<string>();
List<Person> _persons = new List<Person>(); 

bool flag = true;

while (flag)
{
    doc = web.Load(url + postfix);
    var nodes = doc.DocumentNode.SelectNodes("//div[@class='person-text']//div[@class='title']//a[@href]");
    if (nodes == null)
    {
        flag = false;
        break;
    }
    foreach (var node in nodes)
    {
        if (node == null)
        {
            flag = false;
            break;
        }
        persons.Add(node.InnerText);
    }
    postfix = $"?offset={offset}";
    offset += 20;
}

string[] str = new string[3];
TextInfo textInfo = new CultureInfo("ru-RU", false).TextInfo;

for (int i = 0; i < persons.Count; i++)
{
    str = persons[i].Trim().Split(' ');
    str[0] = str[0].ToLower();
    if (str.Count() == 2)
    {
        List<string> list = new List<string>(str.ToList());
        list.Add(null);
        str = list.ToArray();
    }
    Person person = new Person
    {
        Key = textInfo.ToTitleCase(str[0]) + "_" + str[1] + "_" + (i+1).ToString(),
        Name = str[1],
        Surname = textInfo.ToTitleCase(str[0]),
        Patronymic = str[2]
    };
    _persons.Add(person);
}

List<string> lines = new(); 

for (int i = 0; i < _persons.Count; i++)
{
    string[] line =
    {
        $"PersonsNames '{_persons[i].Key}'",
        "{",
        $"key = '{_persons[i].Surname} {_persons[i].Name} {_persons[i].Patronymic}' | '{_persons[i].Name} {_persons[i].Patronymic} {_persons[i].Surname}' | '{_persons[i].Surname} {_persons[i].Name}' | '{_persons[i].Name} {_persons[i].Surname}'",
        $"lemma = '{_persons[i].Name} {_persons[i].Surname}'",
        "}",
        " "
    };
    foreach (var item in line)
    {
        lines.Add(item);
    }
}

File.WriteAllLines("WriteLines.txt", lines.ToArray());

class Person
{
    public string Key { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
}