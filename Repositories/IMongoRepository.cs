using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ReloadingBench
{
    public interface IMongoRepository<T>
    {
        T GetItem(FilterDefinition<T> filter);
        List<T> GetItems(FilterDefinition<T> filter);
        ObjectId SaveItem(T item);
        bool DeleteItem(ObjectId id);
    }
}