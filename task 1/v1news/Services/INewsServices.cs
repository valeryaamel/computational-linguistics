using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmltest
{
    public interface INewsServices
    {
        News Getone();
        List<News> GetNews();
        News AddNews(News news);
        bool Delete(string id);
    }
}
