using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmltest
{
    public class News
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Link { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public DateTime? Date { get; set; }
        public int? Views { get; set; }
        public int? Comments { get; set; }
    }
}
