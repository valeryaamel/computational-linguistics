using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace htmltest
{
    internal class NewsServices : INewsServices
    {
        private readonly IMongoCollection<News> _news;
        public NewsServices(IDBClient dBClient)
        {
            _news = dBClient.GetNewsCollection();
        }

        public News AddNews(News news)
        {
            _news.InsertOne(news);
            return news;
        }

        public bool Delete(string id)
        {
            _news.DeleteOne(x => x.Id == id);
            return true;
        }

        public List<News> GetNews() => _news.Find(news => true).SortByDescending(x => x.Date).Limit(100).ToList();

        public News Getone() => _news.Find(news => true).SortByDescending(x => x.Date).Limit(1).First();
    }
}
