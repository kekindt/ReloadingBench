using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ReloadingBench
{
    public abstract class MongoBaseRepository<T> where T : BsonBase
    {
        private MongoClient client;

        public List<string> databases;
        IMongoCollection<T> collection;

        public MongoBaseRepository(string collectionName)
        {
            var connectionString = "mongodb://localhost:27017";
            client = new MongoClient(connectionString);
            //var handle = client.StartSession();

            var database = client.GetDatabase("reloadingbench");
            collection = database.GetCollection<T>(collectionName, null);


        }

        public T GetItem(FilterDefinition<T> filter)
        {
            return collection.Find(filter).FirstOrDefault();
        }

        public List<T> GetItems(FilterDefinition<T> filter)
        {
            return collection.Find(filter).ToList();
        }

        public ObjectId SaveItem(T item)
        {
            if (item.ID == null || item.ID == ObjectId.Empty)
            {
                collection.InsertOne(item);
            }
            else
            {
                collection.FindOneAndReplace(Builders<T>.Filter.Eq(r => r.ID, item.ID), item);
            }
            return item.ID;
        }

        public bool DeleteItem(ObjectId id)
        {
            if(id != null && id != ObjectId.Empty)
            {
                collection.DeleteOne(Builders<T>.Filter.Eq(r => r.ID, id));
            }
            return true;
        }
    }
}