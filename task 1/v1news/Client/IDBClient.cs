using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmltest
{
    internal interface IDBClient
    {
        IMongoCollection<News> GetNewsCollection();
    }
}
