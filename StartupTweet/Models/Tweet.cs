using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartupTweet.Models
{
    public class Tweet
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string text { get; set; }
    }
}
