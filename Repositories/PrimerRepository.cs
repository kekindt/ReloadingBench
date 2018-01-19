using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ReloadingBench
{
    public class PrimerRepository : MongoBaseRepository<Primer>, IMongoRepository<Primer>, IPrimerRepository
    {
        protected const string collectionName = "primers";

        public PrimerRepository() : base(collectionName)
        {

        }

        public List<Primer> GetItems(Dictionary<string, object> filterValues)
        {
            FilterDefinitionBuilder<Primer> builder = Builders<Primer>.Filter;
            foreach (KeyValuePair<string, object> pair in filterValues)
            {
                if (pair.Value != null)
                {
                    switch (pair.Key)
                    {
                        case "Size":
                            builder.Eq(r => r.Size, pair.Value.ToString());
                            break;
                        case "Brand":
                            builder.Eq(r => r.Brand, pair.Value.ToString());
                            break;
                    }
                }
            }
            return base.GetItems(builder.ToBsonDocument());
        }
    }
}