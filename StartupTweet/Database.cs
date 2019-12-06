using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartupTweet
{
    public class Database
    {
        private IMongoDatabase db;
        public Database(string database)
        {
            var client = new MongoClient("mongodb+srv://admin:admin@ehr-roxao.mongodb.net/test?retryWrites=true&w=majority");
            db = client.GetDatabase(database);
        }
        public void InsertRecord<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOneAsync(record);
        }
        public List<T> LoadRecords<T>(string table)
        {
            var collection = db.GetCollection<T>(table);

            return collection.Find(new BsonDocument()).ToList();
        }
    }
}
//var client = new MongoClient("mongodb+srv://admin:admin@ehr-roxao.mongodb.net/test?retryWrites=true&w=majority");
//var database = client.GetDatabase("Twitter");