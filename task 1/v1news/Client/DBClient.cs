using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmltest
{
    internal class DBClient : IDBClient
    {
        private readonly IMongoCollection<News> _news;
        public DBClient(IOptions<NewsDBConfig> newsDbConfig)
        {
            var client = new MongoClient("mongodb+srv://Katsu:ZV2xcd2vhCeiT7XL@cluster0.bgl18.mongodb.net/NewsDB?retryWrites=true&w=majority");
            var database = client.GetDatabase("NewsDB");
            _news = database.GetCollection<News>("News");
        }
        public IMongoCollection<News> GetNewsCollection() => _news;
    }
}
